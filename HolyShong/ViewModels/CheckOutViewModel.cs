using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.ViewModels
{
    public class CheckOutViewModel
    {
        public string CustomerAddress { get; set; }
        public string CustomerNote { get; set; }
        public bool IsTablewares { get; set; }
        public bool IsPlasticbag { get; set; }
    }
}