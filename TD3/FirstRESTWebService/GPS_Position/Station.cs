using System;
using System.Collections.Generic;
using System.Text;

namespace GPS_Position
{
    class Station
    {
        public int number { get; set; }
        public string contractName{ get; set; }
        public string name { get; set; }
        public string status { get; set; }
        public string address { get; set; }
        public bool banking { get; set; }
        public bool bonus { get; set; }
        public bool overflow { get; set; }
        public Position position { get; set; }

        public string ToStringRecap()
        {
            return "[" + status + "] " + name + " Pos=" + position;
        }

        public override string ToString()
        {
            return "[" + status + "] " + name + "\nAddress :" + address + "\nBanking : " + banking + "\nBonus : " + bonus + "\nOverflow : " + overflow;
        }
    }
}
