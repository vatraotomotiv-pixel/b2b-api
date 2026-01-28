using Microsoft.EntityFrameworkCore;
using B2B.Core.Entities;
using B2B.Core.Services;
using B2B.Infrastructure.Data;
using OfficeOpenXml;
using Microsoft.Extensions.Configuration;

namespace B2B.Infrastructure.Services;

public class ProductService : IProductService
{
    private readonly B2BDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly string _webRootPath;

    public ProductService(B2BDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
        _webRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
        ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
    }

    public async Task<List<Product>> GetAllProductsAsync(string? languageCode = null, string? currencyCode = null)
    {
        var query = _context.Products
            .Include(p => p.Translations)
            .Where(p => p.IsActive)
            .AsQueryable();

        var products = await query.ToListAsync();

        // Dil ve para birimi dönüşümü
        foreach (var product in products)
        {
            if (!string.IsNullOrEmpty(languageCode))
            {
                var translation = product.Translations
                    .FirstOrDefault(t => t.LanguageCode == languageCode);
                if (translation != null)
                {
                    product.Name = translation.Name;
                }
            }

            // Para birimi dönüşümü (basit - gerçek projede API kullanılmalı)
            if (!string.IsNullOrEmpty(currencyCode) && product.CurrencyCode != currencyCode)
            {
                // Basit dönüşüm oranları (gerçek projede exchange rate API kullan)
                product.Price = ConvertCurrency(product.Price, product.CurrencyCode, currencyCode);
                product.CurrencyCode = currencyCode;
            }
        }

        return products;
    }

    public async Task<Product?> GetProductByIdAsync(int id, string? languageCode = null)
    {
        var product = await _context.Products
            .Include(p => p.Translations)
            .FirstOrDefaultAsync(p => p.Id == id && p.IsActive);

        if (product != null && !string.IsNullOrEmpty(languageCode))
        {
            var translation = product.Translations
                .FirstOrDefault(t => t.LanguageCode == languageCode);
            if (translation != null)
            {
                product.Name = translation.Name;
            }
        }

        return product;
    }

    public async Task<Product> CreateProductAsync(Product product)
    {
        product.CreatedAt = DateTime.UtcNow;
        product.UpdatedAt = DateTime.UtcNow;
        product.ImageUrl = await GetProductImageUrlAsync(product.ProductCode);

        _context.Products.Add(product);
        await _context.SaveChangesAsync();
        return product;
    }

    public async Task<bool> ImportFromExcelAsync(Stream excelStream)
    {
        try
        {
            using var package = new ExcelPackage(excelStream);
            var worksheet = package.Workbook.Worksheets[0];

            var rowCount = worksheet.Dimension?.Rows ?? 0;
            if (rowCount < 2) return false; // Header + en az 1 satır

            var products = new List<Product>();

            for (int row = 2; row <= rowCount; row++)
            {
                var productCode = worksheet.Cells[row, 1].Text?.Trim();
                if (string.IsNullOrEmpty(productCode)) continue;

                var product = new Product
                {
                    ProductCode = productCode,
                    Name = worksheet.Cells[row, 2].Text?.Trim() ?? "",
                    PackageQuantity = int.TryParse(worksheet.Cells[row, 3].Text, out var qty) ? qty : 1,
                    Price = decimal.TryParse(worksheet.Cells[row, 4].Text, out var price) ? price : 0,
                    CurrencyCode = worksheet.Cells[row, 5].Text?.Trim() ?? "USD",
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                };

                // Görsel URL'ini oluştur
                product.ImageUrl = await GetProductImageUrlAsync(product.ProductCode);

                products.Add(product);
            }

            // Mevcut ürünleri güncelle veya yeni ekle
            foreach (var product in products)
            {
                var existing = await _context.Products
                    .FirstOrDefaultAsync(p => p.ProductCode == product.ProductCode);

                if (existing != null)
                {
                    existing.Name = product.Name;
                    existing.PackageQuantity = product.PackageQuantity;
                    existing.Price = product.Price;
                    existing.CurrencyCode = product.CurrencyCode;
                    existing.ImageUrl = product.ImageUrl;
                    existing.UpdatedAt = DateTime.UtcNow;
                }
                else
                {
                    _context.Products.Add(product);
                }
            }

            await _context.SaveChangesAsync();
            return true;
        }
        catch
        {
            return false;
        }
    }

    public async Task<string> GetProductImageUrlAsync(string productCode)
    {
        var imagePath = _configuration["ImageSettings:ProductImagePath"] ?? "wwwroot/images/products/";
        var extension = _configuration["ImageSettings:DefaultImageExtension"] ?? ".jpg";

        // Görsel dosyasını kontrol et
        var imageFile = Path.Combine(_webRootPath, "images", "products", $"{productCode}{extension}");
        
        if (File.Exists(imageFile))
        {
            return $"/images/products/{productCode}{extension}";
        }

        // Alternatif uzantıları dene
        var extensions = new[] { ".jpg", ".jpeg", ".png", ".webp" };
        foreach (var ext in extensions)
        {
            var altFile = Path.Combine(_webRootPath, "images", "products", $"{productCode}{ext}");
            if (File.Exists(altFile))
            {
                return $"/images/products/{productCode}{ext}";
            }
        }

        // Varsayılan görsel
        return "/images/products/default.jpg";
    }

    private decimal ConvertCurrency(decimal amount, string fromCurrency, string toCurrency)
    {
        // Basit dönüşüm oranları (gerçek projede exchange rate API kullan)
        var rates = new Dictionary<string, decimal>
        {
            { "USD", 1.0m },
            { "EUR", 0.92m },
            { "TRY", 32.0m },
            { "GBP", 0.79m }
        };

        var fromRate = rates.GetValueOrDefault(fromCurrency, 1.0m);
        var toRate = rates.GetValueOrDefault(toCurrency, 1.0m);

        return amount * (toRate / fromRate);
    }
}
