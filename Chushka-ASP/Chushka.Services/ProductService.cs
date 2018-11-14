namespace Chushka.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using Contracts;
    using Data;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using Models.Enums;

    public class ProductService : IProductService
    {
        private readonly ChushkaDbContext context;

        public ProductService(ChushkaDbContext context)
        {
            this.context = context;
        }

        public void CreateProduct(string name, decimal price, string description, ProductType type)
        {
            var product = new Product
            {
                Name = name,
                Price = price,
                Description = description
            };

            product.Type = type;

            this.context.Products.Add(product);
            this.context.SaveChanges();
        }

        public List<Product> GetAllProducts()
        {
            return this.context.Products.Where(p => p.IsDeleted == false).ToList();
        }

        public Product GetProductById(int id)
        {
            var product = this.context.Products.FirstOrDefault(p => p.Id == id);

            return product;
        }
        
        public void ProductOrder(int productId, string userId)
        {
            var order = new Order
            {
                ProductId = productId,
                ClientId = userId
            };

            this.context.Orders.Add(order);
            this.context.SaveChanges();
        }

        public void EditProduct(int productId, string name, decimal price, string description, ProductType type)
        {
            var product = this.context.Products.FirstOrDefault(p => p.Id == productId);

            product.Name = name;
            product.Description = description;
            product.Price = price;
            product.Type = type;

            this.context.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            var product = this.context.Products.FirstOrDefault(p => p.Id == productId);

            product.IsDeleted = true;

            this.context.SaveChanges();
        }

        public List<Order> GetAllOrders()
        {
            var orders = this.context.Orders
                .Where(o => o.IsDeleted == false)
                .Include(o => o.Product)
                .Include(o => o.Client)
                .ToList();

            return orders;
        }
    }
}