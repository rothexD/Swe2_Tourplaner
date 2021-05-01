using System;
using System.Collections.Generic;
using System.Text;

namespace Swe2_tour_planer.helpers
{
    static public class mapQuestJsonExtentions
    {
        static public string distanceInKm(this MapQuestJson.CustomManeuvers single)
        {
            float inMiles = float.Parse(single.DistanceInMiles);
            return (inMiles * 1.609344).ToString();
        }

    }
}
