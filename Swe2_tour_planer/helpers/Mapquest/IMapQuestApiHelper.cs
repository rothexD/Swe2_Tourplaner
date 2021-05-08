using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swe2_tour_planer.Model;
using static Swe2_tour_planer.helpers.MapQuestJson;

namespace Swe2_tour_planer.helpers
{
    public interface IMapQuestApiHelper
    {
        public Task<string> getMapImage(string from, string too, string x_pixel = "500", string y_pixel = "500");
        public Task<List<CustomManeuvers>> getRoute(string from, string too);
    }
}
