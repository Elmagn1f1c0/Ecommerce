using API.Data;
using API.Data.DTO;
using API.Entities;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
	[Route("api/[controller]")]
	public class BasketController : BaseApiController
	{
		private readonly StoreContext _context;

		public BasketController(StoreContext context)
		{
			_context = context;
		}

		[HttpGet(Name = "GetBasket")]
		public async Task<ActionResult<BasketDto>> GetBasket()
        {
            var basket = await RetrieveBasket();

            if (basket == null) return NotFound();

            return MapBasketToDto(basket);
        }

       

        [HttpPost]
		public async Task<ActionResult<BasketDto>> AddItemToBasket(int productId, int quantity)
		{
			//get basket || create the basket
			var basket = await RetrieveBasket();
			if (basket == null) basket = CreateBasket();

			//get product
			var product = await _context.Products.FindAsync(productId);
			if (product == null) return NotFound();

			// add item
			basket.AddItem(product, quantity);

			// save the changes
			var result = await _context.SaveChangesAsync() > 0;
			if (result) return CreatedAtRoute("GetBasket", MapBasketToDto(basket));

			return BadRequest(new ProblemDetails { Title = "Problem saving Item to basket" });
		}

		[HttpDelete]
		public async Task<ActionResult> RemoveBasketItem(int productId, int quantity)
		{
			// Get basket
			var basket = await RetrieveBasket();
			if (basket == null) return NotFound();

			// Check if the item exists in the basket
			var basketItem = basket.Items.FirstOrDefault(item => item.ProductId == productId);
			if (basketItem == null)
			{
				return NotFound("Item not found in the basket");
			}

			// Check if the quantity to remove is greater than the quantity in the basket
			if (quantity > basketItem.Quantity)
			{
				return BadRequest("Quantity to remove exceeds the available quantity in the basket");
			}

			// Remove the item or reduce quantity
			basket.RemoveItem(productId, quantity);

			// Save changes
			var result = await _context.SaveChangesAsync() > 0;
			if (result) return Ok();

			return BadRequest(new ProblemDetails { Title = "Problem deleting item from the basket" });
		}


		private async Task<Basket> RetrieveBasket()
		{
			return await _context.Baskets
				.Include(i => i.Items)
				.ThenInclude(p => p.Product)
				.FirstOrDefaultAsync(x => x.BuyerId == Request.Cookies["buyerId"]);
		}
		private Basket CreateBasket()
		{
			var buyerId = Guid.NewGuid().ToString();
			var cookieOptions = new CookieOptions { IsEssential = true, Expires = DateTime.Now.AddDays(30) };
			Response.Cookies.Append("buyerId", buyerId, cookieOptions);
			var basket = new Basket { BuyerId = buyerId };
			_context.Baskets.Add(basket);
			return basket;
		}
         private BasketDto MapBasketToDto(Basket basket)
        {
            return new BasketDto
            {
                Id = basket.Id,
                BuyerId = basket.BuyerId,
                Items = basket.Items.Select(item => new BasketItemDto
                {
                    ProductId = item.ProductId,
                    Name = item.Product.Name,
                    Price = item.Product.Price,
                    PictureUrl = item.Product.PictureUrl,
                    Type = item.Product.Type,
                    Brand = item.Product.Brand,
                    Quantity = item.Quantity
                }).ToList()
            };
        }
	}
}