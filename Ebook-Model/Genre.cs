﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook_Model
{
    public  class Genre
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [Display(Name = "Genre")]
        public string GenreName { get; set; }
    }
}
