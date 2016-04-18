using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models.Partial_Class
{
    [MetadataType(typeof(ProductMetaData))]
    public partial class ProductPartial
    {
    }

    public class ProductMetaData
    {
        public int ProductId { get; set; }
        [至少須包含兩個空白]
        public string ProductName { get; set; }
        public Nullable<decimal> Price { get; set; }
        public Nullable<bool> Active { get; set; }
        public Nullable<decimal> Stock { get; set; }
    }
}