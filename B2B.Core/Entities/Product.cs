namespace B2B.Core.Entities;

public class Product
{
    public int Id { get; set; }
    public string ProductCode { get; set; } = string.Empty; // Ürün kodu (görsel için)
    public string Name { get; set; } = string.Empty; // Ürün adı
    public int PackageQuantity { get; set; } // Paket içi adet
    public decimal Price { get; set; } // Fiyat
    public string CurrencyCode { get; set; } = "USD"; // Para birimi (USD, EUR, TRY, vb.)
    public string ImageUrl { get; set; } = string.Empty; // Görsel URL
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    // Çoklu dil desteği için
    public ICollection<ProductTranslation> Translations { get; set; } = new List<ProductTranslation>();
}

public class ProductTranslation
{
    public int Id { get; set; }
    public int ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public string LanguageCode { get; set; } = string.Empty; // tr, en, de, vb.
    public string Name { get; set; } = string.Empty; // Çevrilmiş ürün adı
}
