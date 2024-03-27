using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApi.Data;
using ProductApi.Models;
using System;

namespace ProductApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductDbContext _dbContext;

        public ProductsController(ProductDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var product = _dbContext.Products.ToList();
            return Ok(product);
        }

        [HttpGet("{id}")]

        public IActionResult GetById(int id)
        {
            var product = _dbContext.Products.Find(id);

            if (product == null)
            {
                return BadRequest();
            }
            return Ok(product);
        }

        [HttpPost]
        public IActionResult Post(Product product)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Products.Add(product);
                _dbContext.SaveChanges();
                return Ok(product);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]

        public IActionResult DeleteById(int id)
        {
            var product = _dbContext.Products.Find(id);

            if (product == null)
            {
                return NotFound();
            }

            _dbContext.Products.Remove(product);
            _dbContext.SaveChanges();

            return NoContent();
        }

        [HttpPut("{id}")]

        public IActionResult UpdateProduct(int id, Product updatedProduct)
        {

            var existingProduct = _dbContext.Products.Find(id);

            if (existingProduct == null)
            {
                return NotFound();
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;
            
            _dbContext.Update(existingProduct);
            _dbContext.SaveChanges();
            return NoContent();
        }

    }

}
