using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace Swe2_tour_planer.helpers
{
    static public class mapQuestJsonExtentions
    {
        static public string DistanceInKm(this MapQuestJson.CustomManeuvers single)
        {
            float inMiles = float.Parse(single.DistanceInMiles);
            float withMath = (inMiles * 1.609344f);
            return withMath.ToString("0.00"); ;
        }
    }
}
