using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreServices.Models
{
    public class Sp_GetAllPost
    {
        public int PostId { get; set; }
        public string Title { get; set; }
        public int? CategoryId { get; set; }
        public DateTime? CreatedDate { get; set; }
        public string CategoryName { get; set; }
    }
}
