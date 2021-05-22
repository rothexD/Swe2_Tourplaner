using Swe2_tour_planer.Models;
using System.Collections.Generic;

namespace Swe2_tour_planer.Services
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
