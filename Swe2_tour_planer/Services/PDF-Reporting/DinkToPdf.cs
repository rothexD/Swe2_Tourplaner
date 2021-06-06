using DinkToPdf;
using Microsoft.Extensions.Configuration;
using Swe2_tour_planer.Models;
using System.Collections.Generic;

namespace Swe2_tour_planer.Services
{
    public class DinkToPdfClass : IDinkToPdfClass
    {
        private static readonly IConfiguration config = new ConfigurationBuilder().AddJsonFile("Appsettings.json", false, true).Build();
        public string TourAndLogToHtml(TourEntry Tour, List<LogEntry> Logs)
        {
            string Html = "";
            Html += TourHtml(Tour);
            Html += LogEntry(Logs);
            return Html;
        }
        public string TourHtml(TourEntry Tour)
        {
            string TourHtml = "";
            TourHtml += $"<h1>Tour from {Tour.Title}</h1>";
            TourHtml += HeadingHtml("2", "Location");
            TourHtml += $"<img width=500 height=500 src=\"{config["MapQuest:Location"]+Tour.ImgSource}\">";
            TourHtml += ParagraphHtml($"From: {Tour.From}<br>Too: {Tour.Too}");
            TourHtml += ParagraphHtml($"Distance: {Tour.TourDistanceInKm}");
            TourHtml += HeadingHtml("2", "Description");
            TourHtml += ParagraphHtml(Tour.Description);

            TourHtml += HeadingHtml("2", "Maneuvers");
            foreach (var item in Tour.Maneuvers)
            {
                TourHtml += ParagraphHtml($"Direction: {item.DirectionName},Distance: {item.DistanceInKm()}km,narrative: {item.Narrative}<br>");
            }
            return TourHtml;
        }
        public string LogEntry(List<LogEntry> Log)
        {
            string LogHtml = HeadingHtml("2", "TourLogs");
            float Sumduration = 0;
            float Sumdistance = 0;
            int z = 0;
            Log.ForEach(x =>
            {
                LogHtml += "<br>";
                LogHtml += HeadingHtml("3", $"Log: {++z}");
                LogHtml += ParagraphHtml($"Date: {x.Date}");
                LogHtml += ParagraphHtml($"Duration: {x.Duration}");
                LogHtml += ParagraphHtml($"Distance: {x.Distance}");
                LogHtml += ParagraphHtml($"Rating: {x.Rating}");
                LogHtml += ParagraphHtml($"Report: {x.Report}");
                LogHtml += ParagraphHtml($"Averagespeed: {x.AverageSpeed}");
                LogHtml += ParagraphHtml($"Energyused: {x.EnergyUsed}");
                LogHtml += ParagraphHtml($"Wheater: {x.Wheater}");
                LogHtml += ParagraphHtml($"Traffic: {x.Traffic}");
                LogHtml += ParagraphHtml($"Nicenessoflocals: {x.NicenessOfLocals}");
                LogHtml += "<br>";
                Sumduration += float.Parse(x.Duration);
                Sumdistance += float.Parse(x.Distance);
            });
            LogHtml += HeadingHtml("2", "Statistics");
            LogHtml += ParagraphHtml($"Sum Distance: {Sumdistance}");
            LogHtml += ParagraphHtml($"Sum Duration: {Sumduration}");
            LogHtml += "<br>";
            return LogHtml;
        }
        public string HeadingHtml(string headingLvl, string a)
        {
            return $"<h{headingLvl }>{a}</h{headingLvl}>";
        }
        public string ParagraphHtml(string a)
        {
            return $"<p>{a}</p>";
        }
        public void CreatePDFFromHtml(string htmlPage, string @out)
        {
            // https://github.com/rdvojmoc/DinkToPdf
            // https://github.com/rdvojmoc/DinkToPdf/tree/master/v0.12.4/64%20bit
            var converter = new BasicConverter(new PdfTools());
            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    ColorMode = ColorMode.Color,
                    Orientation = Orientation.Landscape,
                    PaperSize = PaperKind.A4Plus,
                    Out = @out,
                },
                Objects = {
                    new ObjectSettings() {
                        PagesCount = true,
                        HtmlContent = htmlPage,
                        WebSettings = { DefaultEncoding = "utf-8" },
                        HeaderSettings = { FontSize = 9, Right = "Page [page] of [toPage]", Line = true, Spacing = 2.812 },

                    }
                }
            };
            converter.Convert(doc);
        }
    }
}

