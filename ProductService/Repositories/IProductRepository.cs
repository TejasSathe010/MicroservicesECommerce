using ProductService.Models;

public interface IProductRepository
{
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product?> GetProductById(Guid id); 
    Task AddProduct(Product product);
    Task UpdateProduct(Product product);
    Task DeleteProduct(Guid id); 
    Task<bool> ProductExists(Guid id); 
}
