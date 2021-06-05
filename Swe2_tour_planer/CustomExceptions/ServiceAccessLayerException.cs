using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swe2_tour_planer.CustomExceptions
{
    public class ServiceAccessLayerException : Exception
    {
        public ServiceAccessLayerException()
        {
        }
        public ServiceAccessLayerException(string message)
            : base(message)
        {
        }
        public ServiceAccessLayerException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
