using CouchbaseSampleApp.Controllers;
using CouchbaseSampleApp.Data.Entities;

namespace CouchbaseSampleApp.Data;

public interface IProductRepository
{
    Task<Product> GetProductByKey(string key);
    Task<List<Product>> GetProductsByCategory(ProductCategoryType category);
    Task<string> CreateProduct(ProductDto product);
    Task UpdateProduct(string key, ProductDto product);
}