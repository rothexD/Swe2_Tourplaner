using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Swe2_tour_planer.Model;

namespace Swe2_tour_planer.helpers
{
    public interface IDinkToPdfClass
    {
        public string TourAndLogToHtml(TourEntry Tour, List<LogEntry> Logs);
        public string TourHtml(TourEntry Tour);
        public string LogEntry(List<LogEntry> Log);
        public string HeadingHtml(string headingLvl, string a);
        public string ParagraphHtml(string a);
        public void CreatePDFFromHtml(string htmlPage, string @out);
    }
}
