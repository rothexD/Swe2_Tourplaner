using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swe2_tour_planer.Model;
using Newtonsoft.Json;
using System.IO;

namespace Swe2_tour_planer.helpers
{
    class ImportExporthelper
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public static async void ExportToJsonFile(string path,string filename, List<LogsAndTours> toExport)
        {
            try
            {
                await File.WriteAllTextAsync(path + filename, JsonConvert.SerializeObject(toExport));
            }
            catch (Exception e)
            {
                log.Error("could not serialize file");
                log.Debug(e.Message);
            }
}
        public static List<LogsAndTours> ImportFromJsonFile(string fullpath)
        {
            try
            {
                return JsonConvert.DeserializeObject<List<LogsAndTours>>(File.ReadAllText(fullpath));
            }
            catch(Exception e)
            {
                log.Error("could not deserialize file");
                log.Debug(e.Message);
                return new List<LogsAndTours>();
            }      
        }
    }
}
