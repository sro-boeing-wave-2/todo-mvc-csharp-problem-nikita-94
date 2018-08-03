using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Controllers.Models;

namespace WebApplication1.Controllers
{
    [Produces("application/xml")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Products> p = new List<Products>(new[]
            {
                new Products()
                {
                    ID=1,Name="hot mayo"
                },
                new Products()
                {
                    ID=2,Name="cheese"
                },
                new Products()
                {
                    ID=3,Name="butter"
                },
            });
        public List<Products> GET(){
            //=> "Hello API";
            return p;
        }
        [HttpGet("{id}")]
        public IActionResult Get( int id)
        {
            var product= p.SingleOrDefault(p => p.ID == id);
            if (product == null)
            {
                return NotFound();
            }
            return Ok(product);
        }
        [HttpPost]
        public IActionResult Post ([FromBody]Products product)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            p.Add(product);
            return CreatedAtAction(nameof(Get),
            new { id = product.ID }, product);
        }
    }
}