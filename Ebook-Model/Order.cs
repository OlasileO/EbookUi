using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook_Model
{
    public  class Order
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        [Required]
        public string? UserId { get; set; }  
        public int OrderStatusId { get; set; }
        [Display(Name = "Order Status")]
        public OrderStatus OrderStatus { get; set; }
       
        public ICollection<OrderDetails> OrderDetails { get; set; }
    }
}
