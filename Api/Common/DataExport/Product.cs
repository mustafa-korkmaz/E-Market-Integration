using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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
        public int TaxIncluded { get; set; }
        public decimal SpecialPrice { get; set; }
        public string DefaultImage { get; set; }

        private string ReplaceHexadecimalSymbols(string txt)
        {
            string r = "[\x00-\x08\x0B\x0C\x0E-\x1F\x26]";
            return Regex.Replace(txt, r, "", RegexOptions.Compiled);
        }
    }

    public class ProductRoot
    {
        public List<Product> Products { get; set; }
        public List<ProductAttribute> ProductAttributes { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductOption> ProductOptions { get; set; }
        public List<ProductShippingMethod> ProductShippingMethods { get; set; }
    }

    public class ExtendedProduct
    {
        public Product Product { get; set; }

        public List<ProductAttribute> ProductAttributes { get; private set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ProductOption> ProductOptions { get; set; }
        public List<ProductShippingMethod> ProductShippingMethods { get; set; }

        public void SetProductAttributes(List<ProductAttribute> productAttributes)
        {
            ProductAttributes = productAttributes.Where(p => p.ProductId == Product.ProductId).ToList();
        }

        public void SetProductImages(List<ProductImage> productImages)
        {
            ProductImages = productImages.Where(p => p.ProductId == Product.ProductId).ToList();
        }

        public void SetProductOptions(List<ProductOption> productOptions)
        {
            ProductOptions = productOptions.Where(p => p.ProductId == Product.ProductId).ToList();
        }

        public void SetProductShippingMethods(List<ProductShippingMethod> productShippingMethods)
        {
            ProductShippingMethods = productShippingMethods;
        }

    }
}


