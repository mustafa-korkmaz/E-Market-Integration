using Api.Common.DataExport;
using System.Collections.Generic;
using Api.BL.Helper;

namespace Api.BL.DataExport
{
    public class ProductExporter : IDataExportable
    {
        private ProductRoot _productRoot { get; set; }

        public ProductExporter(ProductRoot productRoot)
        {
            _productRoot = productRoot;
        }

        public string ExportData()
        {
            string xml = string.Empty;

            var extendedProductList = new List<ExtendedProduct>();

            foreach (var product in _productRoot.Products)
            {
                ExtendedProduct extendedProduct = new ExtendedProduct() { Product = product };

                extendedProduct.SetProductAttributes(_productRoot.ProductAttributes);
                extendedProduct.SetProductImages(_productRoot.ProductImages);
                extendedProduct.SetProductOptions(_productRoot.ProductOptions);
                extendedProduct.SetProductShippingMethods(_productRoot.ProductShippingMethods);

                extendedProductList.Add(extendedProduct);
            }

            return extendedProductList.Serialize();
        }
    }
}