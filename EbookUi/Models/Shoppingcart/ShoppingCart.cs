using Ebook_Model;
using Ebook_Service.Implementation;
using Microsoft.EntityFrameworkCore;
using System;

namespace EbookUi.Models.Shoppingcart
{
    public class ShoppingCart
    { 
        private readonly EbookDbContext _dbContext;
        public string ShoppingCartId { get; set; }
        public List<ShoppingCartItem> shoppingCartItems { get; set; }
        public ShoppingCart(EbookDbContext dbContext)
        {
            _dbContext= dbContext;
        }
        public static ShoppingCart GetShoppingCart(IServiceProvider serviceProvider)
        {
            ISession session = serviceProvider.GetRequiredService<IHttpContextAccessor>()?.HttpContext.Session;
            var context = serviceProvider.GetService<EbookDbContext>();

            string cartId = session.GetString("cartId") ?? Guid.NewGuid().ToString();
            session.SetString("cartId", cartId);

            return new ShoppingCart(context) { ShoppingCartId = cartId };
        }

        public void AddItemtocart(Book book)
        {
            var shoppingCarts = _dbContext.ShoppingCartItems.FirstOrDefault(b => b.Book.Id == book.Id
                                                    && b.ShoppingCardId == ShoppingCartId);
            if (shoppingCarts == null)
            {
                shoppingCarts = new ShoppingCartItem
                {
                    ShoppingCardId= ShoppingCartId,
                    Book = book,
                    Quatity = 1

                };
                _dbContext.ShoppingCartItems.Add(shoppingCarts);
            }
            else
            {
                shoppingCarts.Quatity++;
            }
            _dbContext.SaveChanges();
        }

        public void RemoveItemFrmCart(Book book)
        {
            var shoppingcarts = _dbContext.ShoppingCartItems.FirstOrDefault(b => b.Book.Id == book.Id
                                                            && b.ShoppingCardId == ShoppingCartId);
            if (shoppingcarts != null)
            {
                if (shoppingcarts.Quatity > 1)
                {
                    shoppingcarts.Quatity--;
                }
                else
                {
                    _dbContext.ShoppingCartItems.Remove(shoppingcarts);
                }
                _dbContext.SaveChanges();
            }
        }
        public List<ShoppingCartItem> GetshoppingCartItems()
        {
            return shoppingCartItems ?? (shoppingCartItems = _dbContext.ShoppingCartItems.Where
                (s => s.ShoppingCardId == ShoppingCartId).Include(b => b.Book).ToList());
        }

        public double GetshoppingCartTotal() => _dbContext.ShoppingCartItems.Where
            (s => s.ShoppingCardId == ShoppingCartId).Select(b => b.Book.price*b.Quatity).Sum();

        public async Task clearShoppingCartAsync()
        {
            var item = await _dbContext.ShoppingCartItems.Where(s => s.ShoppingCardId == ShoppingCartId).ToListAsync();
            _dbContext.ShoppingCartItems.RemoveRange(item);
            await _dbContext.SaveChangesAsync();
        }
    }
}
