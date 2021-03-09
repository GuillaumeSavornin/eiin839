using System;
using System.Collections.Generic;
using System.Text;

namespace FirstRESTWebService
{
    class Park
    {
        public int number { get; set; }
        public string contractName { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string status { get; set; }
        public string accessType{ get; set; }
        public string lockerType { get; set; }
        public bool hasSurveillance { get; set; }
        public bool isFree { get; set; }
        public bool overflow { get; set; }
    }
}
