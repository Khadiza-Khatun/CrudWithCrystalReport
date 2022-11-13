namespace WebSSLProject.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Product")]
    public partial class Product
    {
        public int ProductId { get; set; }

        [StringLength(50)]
        public string ProductName { get; set; }

        [Column(TypeName = "money")]
        public decimal Price { get; set; }

        public int Quantity { get; set; }

        [StringLength(50)]
        public string Stock { get; set; }

        [Column(TypeName = "date")]
        public DateTime SalesDate { get; set; }

        [StringLength(500)]
        public string ProductPhoto { get; set; }

        public int BrandId { get; set; }

        public virtual ProductBrand ProductBrand { get; set; }
    }
}
