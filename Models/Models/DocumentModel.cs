using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class DocumentModel : IId
    {
        public int Id { get; set; }
        public int AdminId { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Year { get; set; }

        public string Content { get; set; }

        public string CloudLink { get; set; }
        public AdminModel? Admin { get; set; }
    }
}
