using Microsoft.EntityFrameworkCore;
using B2B.Infrastructure.Data;
using B2B.Core.Services;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Render / Docker: PORT env ile dinle
var port = Environment.GetEnvironmentVariable("PORT");
if (!string.IsNullOrEmpty(port))
    builder.WebHost.UseUrls($"http://0.0.0.0:{port}");

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();

// Database - MySQL (XAMPP)
builder.Services.AddDbContext<B2BDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 0))
    ));

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

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
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
<li><a href="/openapi/v1.json">/openapi/v1.json</a> - API dokumani</li>
</ul>
</body>
</html>
""", "text/html"));

app.Run();
