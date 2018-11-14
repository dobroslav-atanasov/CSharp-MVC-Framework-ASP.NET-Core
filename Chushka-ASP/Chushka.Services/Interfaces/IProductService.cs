namespace Chushka.Services.Contracts
{
    using System.Collections.Generic;
    using Models;
    using Models.Enums;

    public interface IProductService
    {
        void CreateProduct(string name, decimal price, string description, ProductType type);

        List<Product> GetAllProducts();

        Product GetProductById(int id);

        void ProductOrder(int productId, string userId);

        void EditProduct(int productId, string name, decimal price, string description, ProductType type);

        void DeleteProduct(int productId);

        List<Order> GetAllOrders();
    }
}