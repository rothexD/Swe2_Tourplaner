namespace Swe2_tour_planer.Models
{
    static public class MapQuestJsonExtentions
    {
        static public string DistanceInKm(this MapQuestJson.CustomManeuvers single)
        {
            float inMiles = float.Parse(single.DistanceInMiles);
            float withMath = (inMiles * 1.609344f);
            return withMath.ToString("0.00"); ;
        }
    }
}
