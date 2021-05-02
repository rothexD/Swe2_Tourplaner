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
        public static async void ExportToJsonFile(string path,string filename, LogsAndTours toExport)
        {
            await File.WriteAllTextAsync("path" + filename, JsonConvert.SerializeObject(toExport));             
        }
        public static LogsAndTours ImportFromJsonFile(string fullpath)
        {
          return JsonConvert.DeserializeObject<LogsAndTours>(File.ReadAllText(fullpath));
        }
    }
}
