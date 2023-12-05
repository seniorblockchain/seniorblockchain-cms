using powerpage.Entities;

namespace powerpage.Services.Contracts;

public interface IProductService
{
    void AddNewProduct(Product product);
    IList<Product> GetAllProducts();
}