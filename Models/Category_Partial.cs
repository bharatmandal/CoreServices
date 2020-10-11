using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CoreServices.Models
{
    public partial class Category_Partial
    {
        [Required]
        public string Name { get; set; }
    }
}
