using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApiData_Extractor
{
    public class Response
    {
        public string Statuscode { get; set; }
        public string Error { get; set; }
        public string Message { get; set; }
        public string Result { get; set; }
    }
}