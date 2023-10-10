using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DTO;
using API.Entities;
using API.Repository;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
     [Route("api/[controller]")]
    [ApiController]
    public class ProductController  : ControllerBase
    {
        private readonly IProductRepository _repo;
        public ProductController(IProductRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _repo.GetProducts();
            return Ok(products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<Product>>> GetProductById(int id)
        {
            var response = await _repo.GetProductById(id);
            if (response.StatusCode == 200)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }
    }
}