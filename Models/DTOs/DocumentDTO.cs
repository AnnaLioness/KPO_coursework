using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.DTOs
{
    public class DocumentDTO
    {
        public int Id { get; set; }
        public int AdminId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }
        public string CloudLink { get; set; }
        public string HighlightedContent { get; set; } // для подсветки найденного
        public string ContentForSearch { get; set; }
    }
}
