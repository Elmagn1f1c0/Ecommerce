using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Data.DTO;
using API.Entities;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly StoreContext _db;
        public ProductRepository(StoreContext db)
        {
            _db = db;
        }
        public async Task<ResponseDto<Product>> GetProductById(int productId)
        {
            var response = new ResponseDto<Product>();
             var target = await _db.Products.FindAsync(productId);
            if(target != null)
            {
                response.StatusCode = 200;
                response.DisplayMessage = "Product found";
                response.Result = target;
            }
            else
            {
                response.StatusCode = 404;
                response.DisplayMessage = "Product Id does not exist";
            }
            return response;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
             var productList = await _db.Products.ToListAsync();
             if(productList.Any())
             {
                return productList;
             }
             return null;
        }
    }
}