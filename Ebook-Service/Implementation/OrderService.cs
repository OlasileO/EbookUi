using Ebook_Model;
using Ebook_Model.Constant;
using Ebook_Service.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook_Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly EbookDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<IdentityUser> _userManager;

        public OrderService(EbookDbContext dbContext, IHttpContextAccessor httpContextAccessor, UserManager<IdentityUser> userManager)
        {
            _dbContext=dbContext;
            _httpContextAccessor=httpContextAccessor;
            _userManager=userManager;
        }

        public async Task<List<Order>> GetOrderByUserIdAndRoleAsync( string userRole)
        {
            var userId = GetUserId();
            if (string.IsNullOrEmpty(userId))
                throw new Exception("User is not logged-in");
            var orders = await _dbContext.Order.Include(s=>s.OrderStatus)
                                                 .Include(n => n.OrderDetails)
                                                .ThenInclude(n => n.Book)
                                                .ToListAsync();

            if (userRole != "admin")
            {
                orders = orders.Where(n => n.UserId == userId).ToList();
            }
            return orders;

        }

        public async Task StoreOrderAysnc(List<ShoppingCartItem> items, string userId)
        {
            var order = new Order()
            {
                UserId = userId,
                CreatedDate= DateTime.Now,
                OrderStatusId = 1,
                
            };
              await _dbContext.Order.AddAsync(order);
            await _dbContext.SaveChangesAsync();
            
            

            foreach (var item in items)
            {
                var orderitem = new OrderDetails()
                {
                    Quatity = item.Quatity,
                    BookId = item.Book.Id,
                    OrderId = order.Id,
                    Price = item.Book.price,
                };
                await _dbContext.OrderDetail.AddAsync(orderitem);
            }
            await _dbContext.SaveChangesAsync();
        }
        private string GetUserId()
        {
            var principal = _httpContextAccessor.HttpContext.User;
            string userId = _userManager.GetUserId(principal);
            return userId;
        }
    }
    
}
