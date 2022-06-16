using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestAPI.DAL.Entities;
using TestAPI.DAL.Functions.Implementations;
using TestAPI.DAL.Functions.Interfaces;
using TestAPI.Logic.Models;
using TestAPI.Logic.Services.Interfaces;

namespace TestAPI.Logic.Services.Implementations
{
    public class ProductService : IProductService
    {
        private ICrud _crud = new Crud();

        public async Task<List<Product_ResultSet>> GetAllProducts()
        {
            try
            {
                List<Product> products = await _crud.ReadAll<Product>();

                List<Product_ResultSet> result = new List<Product_ResultSet>();
                products.ForEach(p =>
                {
                    result.Add(new Product_ResultSet
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Price = p.Price,
                        Quantity = p.Quantity,
                        Description = p.Description
                    });
                });

                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product_ResultSet> AddProduct(string name, decimal price, int quantity, string description)
        {
            try
            {
                Product product = new Product
                {
                    Name = name,
                    Price = price,
                    Quantity = quantity,
                    Description = description
                };

                product = await _crud.Create<Product>(product);

                Product_ResultSet productAdded = new Product_ResultSet
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Description = product.Description
                };

                return productAdded;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Product_ResultSet> UpdateProduct(long id, string name, decimal price, int quantity, string description)
        {
            try
            {
                Product product = new Product
                {
                    Id = id,
                    Name = name,
                    Price = price,
                    Quantity = quantity,
                    Description = description
                };

                product = await _crud.Update<Product>(product, id);

                Product_ResultSet productUpdated = new Product_ResultSet
                {
                    Id = product.Id,
                    Name = product.Name,
                    Price = product.Price,
                    Quantity = product.Quantity,
                    Description = product.Description
                };

                return productUpdated;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> DeleteProductById(long id)
        {
            try
            {
                bool isProductDeleted = await _crud.Delete<Product>(id);

                return isProductDeleted;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
