using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swe2_tour_planer.CustomExceptions
{
    class MapQuestException : Exception
    {
        public MapQuestException()
        {
        }

        public MapQuestException(string message)
            : base(message)
        {
        }

        public MapQuestException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
