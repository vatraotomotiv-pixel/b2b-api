using Microsoft.EntityFrameworkCore;
using B2B.Infrastructure.Data;
using B2B.Core.Services;
using B2B.Core.Entities;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Render / Docker: PORT env ile dinle
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Database: Render'da (RENDER=true) veya connection string yoksa SQLite; yoksa MySQL
var connStr = builder.Configuration.GetConnectionString("DefaultConnection");
var useSqlite = string.Equals(Environment.GetEnvironmentVariable("RENDER"), "true", StringComparison.OrdinalIgnoreCase)
    || string.IsNullOrWhiteSpace(connStr)
    || !connStr.TrimStart().StartsWith("Server=", StringComparison.OrdinalIgnoreCase);
if (!useSqlite)
{
    builder.Services.AddDbContext<B2BDbContext>(options =>
        options.UseMySql(connStr, new MySqlServerVersion(new Version(8, 0, 0))));
}
else
{
    var dataDir = Path.Combine(Directory.GetCurrentDirectory(), "data");
    Directory.CreateDirectory(dataDir);
    var sqlitePath = Path.Combine(dataDir, "b2b.db");
    builder.Services.AddDbContext<B2BDbContext>(options =>
        options.UseSqlite($"Data Source={sqlitePath}"));
}

// CORS
builder.Services.AddCors(options =>
{
    if (builder.Environment.IsDevelopment())
    {
        // Development: Tüm origin'lere izin ver
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyMethod()
                  .AllowAnyHeader();
        });
    }
    else
    {
        // Production: Sadece domain'e izin ver
        options.AddPolicy("AllowAll", policy =>
        {
            policy.WithOrigins(
                    "https://b2b.vatraotomotiv.com.tr",
                    "http://b2b.vatraotomotiv.com.tr"
                  )
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
    }
});

// Register services
builder.Services.AddScoped<IProductService, B2B.Infrastructure.Services.ProductService>();

var app = builder.Build();

// SQLite kullaniliyorsa DB olustur ve ornek urunleri yukle (ilk acilista)
try
{
    using var scope = app.Services.CreateScope();
    var db = scope.ServiceProvider.GetRequiredService<B2BDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    db.Database.EnsureCreated();
    if (!db.Products.Any())
    {
        var now = DateTime.UtcNow;
        var p1 = new Product { ProductCode = "PROD001", Name = "Ürün 1", PackageQuantity = 12, Price = 99.99m, CurrencyCode = "USD", ImageUrl = "/images/products/PROD001.jpg", IsActive = true, CreatedAt = now, UpdatedAt = now };
        var p2 = new Product { ProductCode = "PROD002", Name = "Ürün 2", PackageQuantity = 24, Price = 149.50m, CurrencyCode = "EUR", ImageUrl = "/images/products/PROD002.jpg", IsActive = true, CreatedAt = now, UpdatedAt = now };
        var p3 = new Product { ProductCode = "PROD003", Name = "Ürün 3", PackageQuantity = 6, Price = 75.00m, CurrencyCode = "TRY", ImageUrl = "/images/products/PROD003.jpg", IsActive = true, CreatedAt = now, UpdatedAt = now };
        p1.Translations.Add(new ProductTranslation { LanguageCode = "tr", Name = "Ürün 1 - Türkçe" });
        p1.Translations.Add(new ProductTranslation { LanguageCode = "en", Name = "Product 1 - English" });
        p2.Translations.Add(new ProductTranslation { LanguageCode = "tr", Name = "Ürün 2 - Türkçe" });
        p2.Translations.Add(new ProductTranslation { LanguageCode = "en", Name = "Product 2 - English" });
        p3.Translations.Add(new ProductTranslation { LanguageCode = "tr", Name = "Ürün 3 - Türkçe" });
        p3.Translations.Add(new ProductTranslation { LanguageCode = "en", Name = "Product 3 - English" });
        db.Products.AddRange(p1, p2, p3);
        db.SaveChanges();
        logger.LogInformation("B2B seed: 3 urun eklendi.");
    }
}
catch (Exception)
{
    // Seed basarisiz - uygulama yine de calisir, urunler bos olur
}

// /health en basta yanitlansin (Render probe, uygulama tam acilmadan da calissin)
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/health", StringComparison.OrdinalIgnoreCase))
    {
        await context.Response.WriteAsJsonAsync(new { status = "healthy", timestamp = DateTime.UtcNow });
        return;
    }
    await next(context);
});

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// HTTPS redirection sadece production'da
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseCors("AllowAll");
app.UseDefaultFiles(); // index.html için
app.UseStaticFiles(); // Görseller ve frontend için (wwwroot)

app.MapControllers();

// Health check
app.MapGet("/health", () => Results.Ok(new { status = "healthy", timestamp = DateTime.UtcNow }));

// Ana sayfa - tarayicida http://localhost:5000 acilinca bu gorunur
app.MapGet("/", () => Results.Content("""
<!DOCTYPE html>
<html>
<head><meta charset="utf-8"><title>B2B API</title></head>
<body style="font-family: sans-serif; padding: 2rem;">
<h1>B2B API calisiyor</h1>
<p>Asagidaki linklere tikla:</p>
<ul>
<li><a href="/health">/health</a> - Saglik kontrolu</li>
<li><a href="/api/products">/api/products</a> - Urunler</li>
<li><a href="/swagger">/swagger</a> - API dokumani</li>
</ul>
</body>
</html>
""", "text/html"));

app.Run();
