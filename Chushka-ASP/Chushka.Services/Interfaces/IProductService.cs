namespace Chushka.Services.Contracts
{
    using System.Collections.Generic;
    using Models;
    using Models.Enums;

    public interface IProductService
    {
        void AddProduct(string name, decimal price, string description, Type type);

        List<Product> GetAllProducts();

        Product GetProductById(int productId);

        void ProductOrder(int productId, int userId);

        void EditProduct(int productId, string name, decimal price, string description, Type type);

        void DeleteProduct(int productId);
    }
}