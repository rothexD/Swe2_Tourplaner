using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swe2_tour_planer.Model;
using Newtonsoft.Json;
using System.IO;
using Swe2_tour_planer.Logik;

namespace Swe2_tour_planer.helpers
{
    public class ImportExporthelper : IImportExporthelper
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        
        public async Task<bool> SaveToFile(string path,string output)
        {
            try
            {
                await File.WriteAllTextAsync(path , output);
                return true;
            }
            catch (Exception e)
            {
                log.Error("could not save file");
                throw e;
            }
}
        public string ImportFromJsonFile(string fullpath)
        {
            try
            {
                return File.ReadAllText(fullpath);
            }
            catch(Exception e)
            {
                log.Error("could not deserialize file");
                throw e;
            }      
        }
    }
}
