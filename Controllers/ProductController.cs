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

    /// <summary>
    /// Get product by id
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    [HttpGet("{key}")]
    public async Task<IActionResult> GetByKey(string key)
    {
        return Ok(await _productRepository.GetProductByKey(key));
    }

    /// <summary>
    /// Get products by category type
    /// </summary>
    /// <remarks>
    /// 0 => electronics, 1 => fashion, 2 = home
    /// </remarks>
    /// <param name="category"></param>
    /// <returns></returns>
    [HttpGet]
    public async Task<IActionResult> GetByCategory(ProductCategoryType type)
    {
        return Ok(await _productRepository.GetProductsByCategory(type));
    }

    /// <summary>
    /// Create product
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    [HttpPost]
    public async Task<IActionResult> Create(ProductDto product)
    {
        return Ok(await _productRepository.CreateProduct(product));
    }

    /// <summary>
    /// Update product
    /// </summary>
    /// <param name="key"></param>
    /// <param name="product"></param>
    [HttpPut("{key}")]
    public async Task<IActionResult> Update(string key, [FromBody] ProductDto product)
    {
        await _productRepository.UpdateProduct(key, product);
        return Ok();
    }
}