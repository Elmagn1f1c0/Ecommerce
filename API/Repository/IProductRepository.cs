using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data.DTO;
using API.Entities;

namespace API.Repository
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();
        Task<ResponseDto<Product>> GetProductById(int productId);
    }
}