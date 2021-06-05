using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using static Swe2_tour_planer.Models.MapQuestJson;
using Swe2_tour_planer.CustomExceptions;

namespace Swe2_tour_planer.Services
{
    public class MapQuestApi : IMapQuestApiHelper
    {
        static readonly HttpClient client = new HttpClient();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static readonly IConfiguration config = new ConfigurationBuilder().AddJsonFile("Appsettings.json", false, true).Build();
        private readonly IFileSystemAccess _fileSystemAccess;

        public MapQuestApi(IFileSystemAccess fileSystem)
        {
            _fileSystemAccess = fileSystem;
        }

        //https://www.mapquestapi.com/staticmap/v5/map?start=New+York,NY&end=Washington,DC&size=600,400@2x&key=KEY
        public async Task<string> GetMapImageAsync(string from, string too, string x_pixel = "500", string y_pixel = "500")
        {
            try
            {
                string uri = $@"https://www.mapquestapi.com/staticmap/v5/map?start={from}&end={too}&size={x_pixel},{y_pixel}@2x&key={config["MapQuest:Key"]}";
                uri = Uri.EscapeUriString(uri);
                var response = await client.GetAsync(uri);

                string id = Guid.NewGuid().GetHashCode().ToString();
                id += Guid.NewGuid().GetHashCode().ToString();
                id += ".jpg";
                using (var stream = await response.Content.ReadAsStreamAsync())
                {
                    await _fileSystemAccess.SaveToFileSystemFromStreamAsync(config["MapQuest:Location"] + id, stream);
                }
                log.Info("got image and stored to disk");
                return id;
            }
            catch (Exception e)
            {
                log.Error("could not get image to route");
                log.Debug(e.StackTrace); 
                throw;
            }
        }
        public async Task<List<CustomManeuvers>> GetRouteAsync(string from, string too)
        {
            try
            {
                string uri = $@"https://www.mapquestapi.com/directions/v2/route?key={config["MapQuest:Key"]}&from={from}&to={too}";
                uri = Uri.EscapeUriString(uri);
                var response = await client.GetAsync(uri);

                Root Routedescription = JsonConvert.DeserializeObject<Root>(await response.Content.ReadAsStringAsync());

                List<CustomManeuvers> customMan = new List<CustomManeuvers>();

                if (Routedescription.Info.Statuscode != 0)
                {
                    log.Error($"route not possible statuscode of mapquest api was: {Routedescription.Info.Statuscode.ToString()} with message: \"{Routedescription.Info.Messages}\"");
                    log.Debug(from);
                    log.Debug(too);
                    throw new MapQuestException($"route not possible statuscode of mapquest api was: {Routedescription.Info.Statuscode.ToString()} with message: \"{Routedescription.Info.Messages}\"");
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
                log.Info("successful Route");
                return customMan;
            }
            catch (Exception e)
            {
                log.Error("Exception occured in get Route\n");
                log.Debug(e.StackTrace); 
                throw;
            }
        }
    }
}
