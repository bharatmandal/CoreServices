using System;
using System.Collections.Generic;

namespace CoreServices.Models
{
    public partial class Address
    {
        public decimal? AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string Country { get; set; }
    }
}
