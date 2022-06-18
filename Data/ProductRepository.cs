using Couchbase;
using Couchbase.KeyValue;
using CouchbaseSampleApp.Controllers;
using CouchbaseSampleApp.Data.Entities;

namespace CouchbaseSampleApp.Data;

public class ProductRepository : IProductRepository
{
    private readonly IBucket _bucket;

    public ProductRepository(IProductBucketProvider bucketProvider)
    {
        _bucket = bucketProvider.GetBucketAsync().Result;
    }
    
    /// <summary>
    /// Get product by id
    /// </summary>
    /// <param name="key"></param>
    /// <returns></returns>
    public async Task<Product> GetProductByKey(string key)
    {
        var collection = await _bucket.CollectionAsync("_default");
        var product = (await collection.GetAsync(key)).ContentAs<Product>();

        return product;
    }

    /// <summary>
    /// Get products by category type
    /// </summary>
    /// <remarks>
    /// 0 => electronics, 1 => fashion, 2 = home
    /// </remarks>
    /// <param name="category"></param>
    /// <returns></returns>
    public async Task<List<Product>> GetProductsByCategory(ProductCategoryType category)
    {
        var queryResult = await _bucket.Cluster.QueryAsync<Product>(
            "select meta().id as id, name, description, modelCode, category from products.`_default`.`_default` where category = $category",
            options => options.Parameter("category", category.ToString()));

        return await queryResult.Rows.ToListAsync();
    }

    /// <summary>
    /// Create product
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    public async Task<string> CreateProduct(ProductDto product)
    {
        var collection = await _bucket.CollectionAsync("_default");
        var uniqueKey = Guid.NewGuid().ToString();
        await collection.InsertAsync(uniqueKey, new
        {
            Name = product.Name, 
            Description = product.Description, 
            ModelCode = product.ModelCode, 
            Category = product.Category.ToString()
        });

        return uniqueKey;
    }

    /// <summary>
    /// Update product
    /// </summary>
    /// <param name="key"></param>
    /// <param name="product"></param>
    public async Task UpdateProduct(string key, ProductDto product)
    {
        var collection = await _bucket.CollectionAsync("_default");
        await collection.UpsertAsync(key, product);
    }
}