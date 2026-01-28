namespace B2B.Core.Entities;

public class Customer
{
    public int Id { get; set; }
    public string CompanyName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string LanguageCode { get; set; } = "tr"; // Müşteri dili
    public string CurrencyCode { get; set; } = "USD"; // Müşteri para birimi
    public bool IsActive { get; set; } = true;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}
