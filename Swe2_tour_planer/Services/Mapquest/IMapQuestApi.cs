using System.Collections.Generic;
using System.Threading.Tasks;
using static Swe2_tour_planer.Models.MapQuestJson;

namespace Swe2_tour_planer.Services
{
    public interface IMapQuestApiHelper
    {
        public Task<string> GetMapImageAsync(string from, string too, string x_pixel = "500", string y_pixel = "500");
        public Task<List<CustomManeuvers>> GetRouteAsync(string from, string too);
    }
}
