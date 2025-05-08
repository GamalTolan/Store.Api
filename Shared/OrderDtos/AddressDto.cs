using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.OrderDtos
{
    public record AddressDto
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string country { get; set; }

    }
}
