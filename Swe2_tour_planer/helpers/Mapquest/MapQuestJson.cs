using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Swe2_tour_planer.helpers
{
    public class MapQuestJson
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Lr
        {
            [JsonProperty("lng")]
            public double Lng { get; set; }

            [JsonProperty("lat")]
            public double Lat { get; set; }
        }

        public class Ul
        {
            [JsonProperty("lng")]
            public double Lng { get; set; }

            [JsonProperty("lat")]
            public double Lat { get; set; }
        }

        public class BoundingBox
        {
            [JsonProperty("lr")]
            public Lr Lr { get; set; }

            [JsonProperty("ul")]
            public Ul Ul { get; set; }
        }

        public class RouteError
        {
            [JsonProperty("errorCode")]
            public int ErrorCode { get; set; }

            [JsonProperty("message")]
            public string Message { get; set; }
        }

        public class StartPoint
        {
            [JsonProperty("lng")]
            public double Lng { get; set; }

            [JsonProperty("lat")]
            public double Lat { get; set; }
        }

        public class Sign
        {
            [JsonProperty("extraText")]
            public string ExtraText { get; set; }

            [JsonProperty("text")]
            public string Text { get; set; }

            [JsonProperty("type")]
            public int Type { get; set; }

            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("direction")]
            public int Direction { get; set; }
        }

        public class Maneuver
        {
            [JsonProperty("distance")]
            public double Distance { get; set; }

            [JsonProperty("streets")]
            public List<string> Streets { get; set; }

            [JsonProperty("narrative")]
            public string Narrative { get; set; }

            [JsonProperty("turnType")]
            public int TurnType { get; set; }

            [JsonProperty("startPoint")]
            public StartPoint StartPoint { get; set; }

            [JsonProperty("index")]
            public int Index { get; set; }

            [JsonProperty("formattedTime")]
            public string FormattedTime { get; set; }

            [JsonProperty("directionName")]
            public string DirectionName { get; set; }

            [JsonProperty("maneuverNotes")]
            public List<object> ManeuverNotes { get; set; }

            [JsonProperty("linkIds")]
            public List<object> LinkIds { get; set; }

            [JsonProperty("signs")]
            public List<Sign> Signs { get; set; }

            [JsonProperty("mapUrl")]
            public string MapUrl { get; set; }

            [JsonProperty("transportMode")]
            public string TransportMode { get; set; }

            [JsonProperty("attributes")]
            public int Attributes { get; set; }

            [JsonProperty("time")]
            public int Time { get; set; }

            [JsonProperty("iconUrl")]
            public string IconUrl { get; set; }

            [JsonProperty("direction")]
            public int Direction { get; set; }
        }

        public class Leg
        {
            [JsonProperty("hasTollRoad")]
            public bool HasTollRoad { get; set; }

            [JsonProperty("hasBridge")]
            public bool HasBridge { get; set; }

            [JsonProperty("destNarrative")]
            public string DestNarrative { get; set; }

            [JsonProperty("distance")]
            public double Distance { get; set; }

            [JsonProperty("hasTimedRestriction")]
            public bool HasTimedRestriction { get; set; }

            [JsonProperty("hasTunnel")]
            public bool HasTunnel { get; set; }

            [JsonProperty("hasHighway")]
            public bool HasHighway { get; set; }

            [JsonProperty("index")]
            public int Index { get; set; }

            [JsonProperty("formattedTime")]
            public string FormattedTime { get; set; }

            [JsonProperty("origIndex")]
            public int OrigIndex { get; set; }

            [JsonProperty("hasAccessRestriction")]
            public bool HasAccessRestriction { get; set; }

            [JsonProperty("hasSeasonalClosure")]
            public bool HasSeasonalClosure { get; set; }

            [JsonProperty("hasCountryCross")]
            public bool HasCountryCross { get; set; }

            [JsonProperty("roadGradeStrategy")]
            public List<List<object>> RoadGradeStrategy { get; set; }

            [JsonProperty("destIndex")]
            public int DestIndex { get; set; }

            [JsonProperty("time")]
            public int Time { get; set; }

            [JsonProperty("hasUnpaved")]
            public bool HasUnpaved { get; set; }

            [JsonProperty("origNarrative")]
            public string OrigNarrative { get; set; }

            [JsonProperty("maneuvers")]
            public List<Maneuver> Maneuvers { get; set; }

            [JsonProperty("hasFerry")]
            public bool HasFerry { get; set; }
        }

        public class Options
        {
            [JsonProperty("arteryWeights")]
            public List<object> ArteryWeights { get; set; }

            [JsonProperty("cyclingRoadFactor")]
            public int CyclingRoadFactor { get; set; }

            [JsonProperty("timeType")]
            public int TimeType { get; set; }

            [JsonProperty("useTraffic")]
            public bool UseTraffic { get; set; }

            [JsonProperty("returnLinkDirections")]
            public bool ReturnLinkDirections { get; set; }

            [JsonProperty("countryBoundaryDisplay")]
            public bool CountryBoundaryDisplay { get; set; }

            [JsonProperty("enhancedNarrative")]
            public bool EnhancedNarrative { get; set; }

            [JsonProperty("locale")]
            public string Locale { get; set; }

            [JsonProperty("tryAvoidLinkIds")]
            public List<object> TryAvoidLinkIds { get; set; }

            [JsonProperty("drivingStyle")]
            public int DrivingStyle { get; set; }

            [JsonProperty("doReverseGeocode")]
            public bool DoReverseGeocode { get; set; }

            [JsonProperty("generalize")]
            public int Generalize { get; set; }

            [JsonProperty("mustAvoidLinkIds")]
            public List<object> MustAvoidLinkIds { get; set; }

            [JsonProperty("sideOfStreetDisplay")]
            public bool SideOfStreetDisplay { get; set; }

            [JsonProperty("routeType")]
            public string RouteType { get; set; }

            [JsonProperty("avoidTimedConditions")]
            public bool AvoidTimedConditions { get; set; }

            [JsonProperty("routeNumber")]
            public int RouteNumber { get; set; }

            [JsonProperty("shapeFormat")]
            public string ShapeFormat { get; set; }

            [JsonProperty("maxWalkingDistance")]
            public int MaxWalkingDistance { get; set; }

            [JsonProperty("destinationManeuverDisplay")]
            public bool DestinationManeuverDisplay { get; set; }

            [JsonProperty("transferPenalty")]
            public int TransferPenalty { get; set; }

            [JsonProperty("narrativeType")]
            public string NarrativeType { get; set; }

            [JsonProperty("walkingSpeed")]
            public int WalkingSpeed { get; set; }

            [JsonProperty("urbanAvoidFactor")]
            public int UrbanAvoidFactor { get; set; }

            [JsonProperty("stateBoundaryDisplay")]
            public bool StateBoundaryDisplay { get; set; }

            [JsonProperty("unit")]
            public string Unit { get; set; }

            [JsonProperty("highwayEfficiency")]
            public int HighwayEfficiency { get; set; }

            [JsonProperty("maxLinkId")]
            public int MaxLinkId { get; set; }

            [JsonProperty("maneuverPenalty")]
            public int ManeuverPenalty { get; set; }

            [JsonProperty("avoidTripIds")]
            public List<object> AvoidTripIds { get; set; }

            [JsonProperty("filterZoneFactor")]
            public int FilterZoneFactor { get; set; }

            [JsonProperty("manmaps")]
            public string Manmaps { get; set; }
        }

        public class DisplayLatLng
        {
            [JsonProperty("lng")]
            public double Lng { get; set; }

            [JsonProperty("lat")]
            public double Lat { get; set; }
        }

        public class LatLng
        {
            [JsonProperty("lng")]
            public double Lng { get; set; }

            [JsonProperty("lat")]
            public double Lat { get; set; }
        }

        public class Location
        {
            [JsonProperty("dragPoint")]
            public bool DragPoint { get; set; }

            [JsonProperty("displayLatLng")]
            public DisplayLatLng DisplayLatLng { get; set; }

            [JsonProperty("adminArea4")]
            public string AdminArea4 { get; set; }

            [JsonProperty("adminArea5")]
            public string AdminArea5 { get; set; }

            [JsonProperty("postalCode")]
            public string PostalCode { get; set; }

            [JsonProperty("adminArea1")]
            public string AdminArea1 { get; set; }

            [JsonProperty("adminArea3")]
            public string AdminArea3 { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("sideOfStreet")]
            public string SideOfStreet { get; set; }

            [JsonProperty("geocodeQualityCode")]
            public string GeocodeQualityCode { get; set; }

            [JsonProperty("adminArea4Type")]
            public string AdminArea4Type { get; set; }

            [JsonProperty("linkId")]
            public int LinkId { get; set; }

            [JsonProperty("street")]
            public string Street { get; set; }

            [JsonProperty("adminArea5Type")]
            public string AdminArea5Type { get; set; }

            [JsonProperty("geocodeQuality")]
            public string GeocodeQuality { get; set; }

            [JsonProperty("adminArea1Type")]
            public string AdminArea1Type { get; set; }

            [JsonProperty("adminArea3Type")]
            public string AdminArea3Type { get; set; }

            [JsonProperty("latLng")]
            public LatLng LatLng { get; set; }
        }

        public class Route
        {
            [JsonProperty("hasTollRoad")]
            public bool HasTollRoad { get; set; }

            [JsonProperty("hasBridge")]
            public bool HasBridge { get; set; }

            [JsonProperty("boundingBox")]
            public BoundingBox BoundingBox { get; set; }

            [JsonProperty("distance")]
            public double Distance { get; set; }

            [JsonProperty("hasTimedRestriction")]
            public bool HasTimedRestriction { get; set; }

            [JsonProperty("hasTunnel")]
            public bool HasTunnel { get; set; }

            [JsonProperty("hasHighway")]
            public bool HasHighway { get; set; }

            [JsonProperty("computedWaypoints")]
            public List<object> ComputedWaypoints { get; set; }

            [JsonProperty("routeError")]
            public RouteError RouteError { get; set; }

            [JsonProperty("formattedTime")]
            public string FormattedTime { get; set; }

            [JsonProperty("sessionId")]
            public string SessionId { get; set; }

            [JsonProperty("hasAccessRestriction")]
            public bool HasAccessRestriction { get; set; }

            [JsonProperty("realTime")]
            public int RealTime { get; set; }

            [JsonProperty("hasSeasonalClosure")]
            public bool HasSeasonalClosure { get; set; }

            [JsonProperty("hasCountryCross")]
            public bool HasCountryCross { get; set; }

            [JsonProperty("fuelUsed")]
            public double FuelUsed { get; set; }

            [JsonProperty("legs")]
            public List<Leg> Legs { get; set; }

            [JsonProperty("options")]
            public Options Options { get; set; }

            [JsonProperty("locations")]
            public List<Location> Locations { get; set; }

            [JsonProperty("time")]
            public int Time { get; set; }

            [JsonProperty("hasUnpaved")]
            public bool HasUnpaved { get; set; }

            [JsonProperty("locationSequence")]
            public List<int> LocationSequence { get; set; }

            [JsonProperty("hasFerry")]
            public bool HasFerry { get; set; }
        }

        public class Copyright
        {
            [JsonProperty("imageAltText")]
            public string ImageAltText { get; set; }

            [JsonProperty("imageUrl")]
            public string ImageUrl { get; set; }

            [JsonProperty("text")]
            public string Text { get; set; }
        }

        public class Info
        {
            [JsonProperty("statuscode")]
            public int Statuscode { get; set; }

            [JsonProperty("copyright")]
            public Copyright Copyright { get; set; }

            [JsonProperty("messages")]
            public List<object> Messages { get; set; }
        }

        public class Root
        {
            [JsonProperty("route")]
            public Route Route { get; set; }

            [JsonProperty("info")]
            public Info Info { get; set; }
        }

        public class CustomManeuvers
        {
            [JsonProperty("narrative")]
            public string Narrative { get; set; }
            [JsonProperty("streets")]
            public List<string> Streets { get; set; }
            [JsonProperty("directionname")]
            public string DirectionName { get; set; }
            [JsonProperty("route")]
            public string Index { get; set; }
            [JsonProperty("distance")]
            public string DistanceInMiles { get; set; }
        }
    }
}
