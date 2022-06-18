using CouchbaseSampleApp.Data;
using Microsoft.AspNetCore.Mvc;

namespace CouchbaseSampleApp.Controllers;

[ApiController]
[Route("products")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet("{key}")]
    public async Task<IActionResult> GetByKey(string key)
    {
        return Ok(await _productRepository.GetProductByKey(key));
    }

    [HttpGet]
    public async Task<IActionResult> GetByCategory(ProductCategoryType type)
    {
        return Ok(await _productRepository.GetProductsByCategory(type));
    }

    [HttpPost]
    public async Task<IActionResult> Create(ProductDto product)
    {
        return Ok(await _productRepository.CreateProduct(product));
    }

    [HttpPut("{key}")]
    public async Task<IActionResult> Update(string key, [FromBody] ProductDto product)
    {
        await _productRepository.UpdateProduct(key, product);
        return Ok();
    }
}