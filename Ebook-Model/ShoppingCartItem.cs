using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook_Model
{
    public  class ShoppingCartItem
    {
        [Key]
        public int Id { get; set; } 
        public int Quatity { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }  
        public string ShoppingCardId { get; set; }
    }
}
