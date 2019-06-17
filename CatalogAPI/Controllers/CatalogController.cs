using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CatalogAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private static IList<Product> products = new List<Product>()
        {
            new Product{Id=1,Name="Apple",Price=100,Quantity=20},
            new Product{Id=2,Name="Orange",Price=150,Quantity=30}
        };

        [HttpGet("", Name = "GetAll")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public IEnumerable<Product> GetItems()
        {
            return products;
        }

        [HttpGet("{id}", Name ="GetById")]
        [ProducesResponseType((int) HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public ActionResult<Product> GetItemById([FromRoute] int id)
        {
            var item = products.Single(s => s.Id == id);
            if (item == null)
                return NotFound();
            else
                return item;
        }
        [HttpPost("", Name = "AddItem")]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public ActionResult<Product> AddItem([FromBody] Product product)
        {
            product.Id = products.Max(s => s.Id) + 1;
            products.Add(product);
            return Created("", product);
        }
    }
}