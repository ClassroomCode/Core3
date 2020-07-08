using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace EComm.Data.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public decimal? UnitPrice { get; set; }
        public string Package { get; set; }
        public bool IsDiscontinued { get; set; }

        public int SupplierId { get; set; }
        public Supplier Supplier { get; set; }

        public string SupplierName { get; set; }
    }
}
