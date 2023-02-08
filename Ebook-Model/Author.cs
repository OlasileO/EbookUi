using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook_Model
{
    public  class Author
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Author Name")]
        public string? AuthorName { get; set; }
    }
}
