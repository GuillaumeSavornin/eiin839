using System;
using System.Collections.Generic;
using System.Text;

namespace GPS_Position
{
    class Position
    {
        public double lat {get; set;}
        public double lng { get; set; }

        public override string ToString()
        {
            return "[" + lat + ", " + lng + "]";
        }
    }
}
