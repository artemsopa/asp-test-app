using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TestAPI.Logic.Services.Interfaces;
using TestAPI.WEB.Models.Product;

namespace TestAPI.WEB.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IProductService productService;

        public ProductController(IProductService _productService)
        {
            productService = _productService;
        }

        [HttpGet]
        [Authorize(Roles = "Admin, Guest")]
        [Route("[action]")]
        public async Task<IActionResult> GetAllProducts()
        {
            try
            {
                var result = await productService.GetAllProducts();
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("[action]")]
        public async Task<IActionResult> AddProduct(Product_PassObject product)
        {
            try
            {
                var result = await productService.AddProduct(product.Name, product.Price, product.Quantity, product.Description);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        [Route("[action]")]
        public async Task<IActionResult> UpdateProduct(Product_UpdateObject product)
        {
            try
            {
                var result = await productService.UpdateProduct(product.Id, product.Name, product.Price, product.Quantity, product.Description);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        [Route("[action]/{id}")]
        public async Task<IActionResult> DeleteProductById(long id)
        {
            try
            {
                var result = await productService.DeleteProductById(id);
                return Ok(result);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
