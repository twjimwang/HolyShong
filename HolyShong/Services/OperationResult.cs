using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HolyShong.Services
{
    public class OperationResult
    {
        public bool IsSuccessful { get; set; }
        public Exception Exception { get; set; }
    }
}