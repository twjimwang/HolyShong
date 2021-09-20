using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.Repositories
{
    public class ProductOptionRepository
    {
        public int ProductOptionId { get; set; }
        public int ProductId { get; set; }
        public string Name { get; set; }
        public bool IsNecessary { get; set; }
    }
}