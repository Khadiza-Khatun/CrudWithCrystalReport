using WebSSLProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebSSLProject.ViewModels
{
    public class VmProduct
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Stock { get; set; }
        public DateTime SalesDate { get; set; }
        public string ProductPhoto { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
    }
}