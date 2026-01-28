using B2B.Core.Entities;

namespace B2B.Core.Services;

public interface IProductService
{
    Task<List<Product>> GetAllProductsAsync(string? languageCode = null, string? currencyCode = null);
    Task<Product?> GetProductByIdAsync(int id, string? languageCode = null);
    Task<Product> CreateProductAsync(Product product);
    Task<bool> ImportFromExcelAsync(Stream excelStream);
    Task<string> GetProductImageUrlAsync(string productCode);
}
