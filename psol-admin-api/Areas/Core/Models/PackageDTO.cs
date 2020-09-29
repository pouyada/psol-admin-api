using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsolAdminApi.Areas.Core.Models
{
    public class PackageDTO
    {
        public long Id { get; set; }
        public string PackageName { get; set; }
        public string Description { get; set; }
        public string InternalDescription { get; set; }
        public Boolean Active { get; set; }
        public Boolean IsDefault { get; set; }
        public decimal Price { get; set; }
        public decimal Vat { get; set; }
    }

    
}
