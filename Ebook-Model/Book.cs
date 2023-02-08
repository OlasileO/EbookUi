using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook_Model
{
    public  class Book
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name ="Book Name")]
        public string?  BookName { get; set; }
        [Required]
        [Display(Name = "Price")]
        public double price { get; set; }
        [Required]
        public string? Isbn { get; set; }
        [Required]
        [Display(Name = "Book Image")]
        public string  Image { get; set; }
        [Display(Name = "Total Pages")]

        public  int TotalPages { get; set; }
        [Display(Name = "Genre")]
        public int GenreId { get; set; }
        public Genre Genre { get; set; }
        [Display(Name = "Author")]
        public int AuthorId { get; set; }
        public Author Author { get; set; }
        [Display(Name = "Publisher")]
        public int PublisherId{ get; set; } 
        public Publisher Publisher { get; set; }
       
       
    }
}
