﻿using Api.Common;
using Api.Common.DataExport;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Api.Models

{

    [Serializable]
    public class CategoryModel : DataExportModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategoryId { get; set; }
        public string ParentCategoryName { get; set; }
    }

    public class ProductModel : DataExportModel
    {

    }
    [Serializable]
    public class DataExportModel
    {
    }

}