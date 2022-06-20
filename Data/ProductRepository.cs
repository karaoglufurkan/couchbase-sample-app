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
    
    public async Task<Product> GetProductByKey(string key)
    {
        var collection = await _bucket.CollectionAsync("_default");
        var product = (await collection.GetAsync(key)).ContentAs<Product>();

        return product;
    }
    
    public async Task<List<Product>> GetProductsByCategory(ProductCategoryType category)
    {
        var queryResult = await _bucket.Cluster.QueryAsync<Product>(
            "select meta().id as id, name, description, modelCode, category from products.`_default`.`_default` where category = $category",
            options => options.Parameter("category", category.ToString()));

        return await queryResult.Rows.ToListAsync();
    }
    
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
    
    public async Task UpdateProduct(string key, ProductDto product)
    {
        var collection = await _bucket.CollectionAsync("_default");
        await collection.UpsertAsync(key, product);
    }
}