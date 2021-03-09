using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GPS_Location
{
    class StationFinder
    {

        public static Station FindClosestStationToPositon(Station[] stations, Position argPosition)
        {
            Station stationClosestToMe = (stations.Length > 0) ? stations[0] : null;
            double distanceToClosestStation = Double.MaxValue;

            // Find the closest station
            for (int i = 0; i < stations.Length; i++)
            {
                double distanceToStation = stations[i].position.DistanceTo(argPosition);
                Console.WriteLine(i + " - " + stations[i].ToStringRecap() + " | -> " + stations[i].position.DisplayDistanceTo(argPosition));

                if (distanceToStation < distanceToClosestStation)
                {
                    distanceToClosestStation = distanceToStation;
                    stationClosestToMe = stations[i];
                }
            }

            return stationClosestToMe;
        }
    }
}
