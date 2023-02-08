using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Ebook_Model
{
    public  class OrderStatus
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Status Name")]
        public string StatusName { get; set; }
        public int StatusId { get; set; }
    }
}
