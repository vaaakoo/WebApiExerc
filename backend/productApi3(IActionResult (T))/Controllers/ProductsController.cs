using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using productApi3_IActionResult__T__.Data;
using productApi3_IActionResult__T__.Models;

namespace productApi3_IActionResult__T__.Controllers
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
        public async Task<ActionResult<IEnumerable<Product>>> Get()
        {
            var products = await _dbContext.Products.ToListAsync();
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id)
        {
            Console.WriteLine($"Route Parameter ID: {id}");
            var product = await _dbContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            return Ok(product);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Product product)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Products.Add(product);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
                
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<Product>> Delete(int id)
        {
            var product = await _dbContext.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound(); 
            }

            _dbContext.Products.Remove(product);
            await _dbContext.SaveChangesAsync();

            return product; 
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product>> UpdateProduct(int id, Product updatedProduct)
        {

            var existingProduct = await _dbContext.Products.FindAsync(id);

            if (existingProduct == null)
            {
                return NotFound(); 
            }

            existingProduct.Name = updatedProduct.Name;
            existingProduct.Price = updatedProduct.Price;

            await _dbContext.SaveChangesAsync(); 

            return existingProduct;        
        }


    }
}
