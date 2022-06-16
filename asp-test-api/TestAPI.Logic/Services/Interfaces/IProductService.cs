using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TestAPI.Logic.Models;

namespace TestAPI.Logic.Services.Interfaces
{
    public interface IProductService
    {
        Task<List<Product_ResultSet>> GetAllProducts();

        Task<Product_ResultSet> AddProduct(string name, decimal price, int quantity, string description);

        Task<Product_ResultSet> UpdateProduct(long id, string name, decimal price, int quantity, string description);

        Task<bool> DeleteProductById(long id);
    }
}
