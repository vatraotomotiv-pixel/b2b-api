using Microsoft.AspNetCore.Mvc;
using B2B.Core.Services;
using B2B.Core.Entities;

namespace B2B.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductsController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Product>>> GetProducts(
        [FromQuery] string? language = null,
        [FromQuery] string? currency = null)
    {
        try
        {
            var products = await _productService.GetAllProductsAsync(language, currency);
            return Ok(products);
        }
        catch (Exception)
        {
            // MySQL baglantisi yoksa veya hata varsa bos liste don (500 verme)
            return Ok(new List<Product>());
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Product>> GetProduct(int id, [FromQuery] string? language = null)
    {
        var product = await _productService.GetProductByIdAsync(id, language);
        if (product == null) return NotFound();
        return Ok(product);
    }

    [HttpPost]
    public async Task<ActionResult<Product>> CreateProduct(Product product)
    {
        var created = await _productService.CreateProductAsync(product);
        return CreatedAtAction(nameof(GetProduct), new { id = created.Id }, created);
    }

    [HttpPost("import")]
    public async Task<IActionResult> ImportFromExcel(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Excel dosyası yüklenmedi");

        if (!file.FileName.EndsWith(".xlsx") && !file.FileName.EndsWith(".xls"))
            return BadRequest("Sadece Excel dosyası (.xlsx, .xls) yüklenebilir");

        using var stream = file.OpenReadStream();
        var success = await _productService.ImportFromExcelAsync(stream);

        if (success)
            return Ok(new { message = "Ürünler başarıyla import edildi" });
        else
            return BadRequest("Import başarısız");
    }
}
