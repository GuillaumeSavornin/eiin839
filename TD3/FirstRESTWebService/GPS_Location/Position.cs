using System;
using System.Collections.Generic;
using System.Text;
using System.Device.Location;

namespace GPS_Location
{
    class Position
    {
        public double lat {get; set;}
        public double lng { get; set; }

        public override string ToString()
        {
            return "[" + lat + ", " + lng + "]";
        }

        public Position()
        {

        }

        public Position(double lat, double lng)
        {
            this.lat = lat;
            this.lng = lng;
        }

        public GeoCoordinate ToGeoCoordinate()
        {
            return new GeoCoordinate(lat, lng);
        }

        public double DistanceTo(Position position)
        {
            return this.ToGeoCoordinate().GetDistanceTo(position.ToGeoCoordinate());
        }

        public string DisplayDistanceTo(Position position)
        {
            return String.Format("{0:0.00}km", this.DistanceTo(position)/1000);
        }
    }
}
