using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook_Model.Model
{
    public class BookViewModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? BookName { get; set; }
        [Required]
        public double price { get; set; }
        [Required]
        public string? Isbn { get; set; }
        [Required]
        public string Image { get; set; }

        public int TotalPages { get; set; }
        [Required]
        public int GenreId { get; set; }
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int PublisherId { get; set; }
    }
}
