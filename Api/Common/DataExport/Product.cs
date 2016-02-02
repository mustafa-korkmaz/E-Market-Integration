using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Common.DataExport
{
    public class Product
    {
        public int ProductId { get; set; }
        public string ProductCode { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string Status { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TaxRate { get; set; }
        public bool TaxIncluded { get; set; }
        public decimal SpecialPrice { get; set; }
        public string DefaultImage { get; set; }

        public List<ProductAttribute> ProductAttributes { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductOption> ProductOptions { get; set; }
        public List<ProductShippingMethod> ProductShippingMethods { get; set; }
    }
}