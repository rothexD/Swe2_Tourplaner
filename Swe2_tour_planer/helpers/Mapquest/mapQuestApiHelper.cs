using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;
using static Swe2_tour_planer.helpers.MapQuestJson;

namespace Swe2_tour_planer.helpers
{
    class mapQuestApiHelper
    {
        static readonly HttpClient client = new HttpClient();
        static private string  mapquestKey = "";

        //https://www.mapquestapi.com/staticmap/v5/map?start=New+York,NY&end=Washington,DC&size=600,400@2x&key=KEY
        public static async Task<string> getMapImage(string from, string too, string x_pixel = "500", string y_pixel = "500")
        {
           string uri = $@"https://www.mapquestapi.com/staticmap/v5/map?start={from}&end={too}&size={x_pixel},{y_pixel}@2x&key={mapquestKey}";
           uri = Uri.EscapeUriString(uri);
           var response = await client.GetAsync(uri);

           string id = Guid.NewGuid().GetHashCode().ToString();
           id += DateTime.Now.ToString();

           using (var stream = await response.Content.ReadAsStreamAsync())
            {
                var fileInfo = new FileInfo($"{id}.jpg");
                using (var fileStream = fileInfo.OpenWrite())
                {
                    await stream.CopyToAsync(fileStream);
                }
            }
            return id;
        }
        public static async Task<List<CustomManeuvers>> getRoute(string from, string too)
        {
            string uri = $@"http://www.mapquestapi.com/directions/v2/route?key={mapquestKey}&from={from}&to={too}";
            uri = Uri.EscapeUriString(uri);
            var response = await client.GetAsync(uri);       

            Root Routedescription = JsonConvert.DeserializeObject<Root>(await response.Content.ReadAsStringAsync());

            List<CustomManeuvers> customMan = new List<CustomManeuvers>();

            if(Routedescription.Info.Statuscode != 0)
            {
                throw new Exception($"route not possible statuscode of mapquest api was: {Routedescription.Info.Statuscode.ToString()} with message: \"{Routedescription.Info.Messages}\"");
            }

            Routedescription.Route.Legs.ForEach(x =>
            {
                x.Maneuvers.ForEach(y =>
                {
                    customMan.Add(new CustomManeuvers
                    {
                        Index = y.Index.ToString(),
                        Narrative = y.Narrative,
                        Streets = y.Streets,
                        DirectionName = y.DirectionName,
                        DistanceInMiles = y.Distance.ToString()
                    }); ;
                });
            });
            return customMan;
        }
    }
}
