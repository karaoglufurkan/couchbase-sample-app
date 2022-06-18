namespace CouchbaseSampleApp.Controllers;

public class ProductDto
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ModelCode { get; set; }
    public ProductCategoryType Category { get; set; }
}