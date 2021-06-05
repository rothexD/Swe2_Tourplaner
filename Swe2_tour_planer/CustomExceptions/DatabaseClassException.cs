using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Swe2_tour_planer.CustomExceptions
{
    class DatabaseClassException : Exception
    {
        public DatabaseClassException()
        {
        }

        public DatabaseClassException(string message)
            : base(message)
        {
        }

        public DatabaseClassException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
