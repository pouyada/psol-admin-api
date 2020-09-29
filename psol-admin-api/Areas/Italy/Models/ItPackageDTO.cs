using System;


namespace PsolAdminApi.Areas.Italy.Models
{
    public class ItPackageDTO
    {
        public long Id { get; set; }
        public string PackageName { get; set; }
        public string Description { get; set; }
        public string InternalDescription { get; set; }
        public Boolean Active { get; set; }
        public Boolean IsDefault { get; set; }
        public decimal Price { get; set; }
        public decimal IVA { get; set; }

    }
}
