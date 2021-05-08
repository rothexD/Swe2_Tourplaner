using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swe2_tour_planer.Model;

namespace Swe2_tour_planer.helpers
{
    public interface IImportExporthelper
    {
        public Task<bool> SaveToFile(string path, string output);
        public string ImportFromJsonFile(string fullpath);
    }
}
