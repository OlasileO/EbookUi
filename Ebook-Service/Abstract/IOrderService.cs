using Ebook_Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook_Service.Abstract
{
    public  interface IOrderService
    {
        Task StoreOrderAysnc(List<ShoppingCartItem> items, string userId);

        Task<List<Order>> GetOrderByUserIdAndRoleAsync( string userRole);
    }
}
