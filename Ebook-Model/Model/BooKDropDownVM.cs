using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ebook_Model.Model
{
    public class BooKDropDownVM
    {

        public BooKDropDownVM()
        {
            Authors= new List<Author>();
            Genres = new List<Genre>();
            Publisher= new List<Publisher>();
        }
        public List<Author> Authors { get; set; }
        public List<Genre> Genres { get; set; }
        public List<Publisher> Publisher { get; set; }
    }
}
