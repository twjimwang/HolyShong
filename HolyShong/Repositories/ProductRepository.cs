using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.Repositories
{
    public class ProductRepository
    {
        public int ProductId { get; set; }
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public bool IsPopular { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public string Img { get; set; }
        public bool IsEnable { get; set; }
        public bool IsDelete { get; set; }
    }
}