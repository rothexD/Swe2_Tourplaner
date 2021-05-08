using NUnit.Framework;
using Swe2_tour_planer.Logik;
using Swe2_tour_planer.Model;
using Swe2_tour_planer.helpers;
using Moq;
using System.Collections.Generic;
using Swe2_tour_planer.ViewModels;
using System;
using static Swe2_tour_planer.helpers.MapQuestJson;
using Npgsql;
using System.Threading.Tasks;
using System.IO;

namespace Unit_Tests
{
    public class Tests
    {
        private Services _service = new Services(new DatabaseHelper(),new MapQuestApiHelper(),new ImportExporthelper(),new DinkToPdfClass());
        private List<LogsAndTours> listLogsAndTours;
        private TourEntry _tempstorageEntry;
        private LogEntry _tempstorageLog;
        private string _tempstringstorage;

        static private TourEntry tour = new TourEntry(0, "wien berlin", "eine coole reise...", "29919324174129278439.jpg", "vienna", "berlin", "[]");
        static private TourEntry tour2 = new TourEntry(1, "wien hamburg", "eine coole reise...", "29919324174129278439.jpg", "vienna", "hamburg", "[]");
        static private TourEntry tour3 = new TourEntry(2, "wien hamburg", "eine coole reise...", "29919324174129278439.jpg", "vienna", "hamburg", "[]");
        static private TourEntry tour8 = new TourEntry(7, "bla", "eine coole reise...", "29919324174129278439.jpg", "vienna", "hamburg", "[]");
        static private LogEntry log = new LogEntry(0, 0, "abc", "abc", "5", "a", "aaa", "5.5", "5.2", "sunny", "no traffic", "very nice");
        static private LogsAndTours logsAndTour1 = new LogsAndTours()
        {
            Tour = tour,
            Logs = new List<LogEntry>()
        };
        static private LogsAndTours logsAndTour2 = new LogsAndTours()
        {
            Tour = tour2,
            Logs = new List<LogEntry>() { log }
        };
        static private LogsAndTours logsAndTour3 = new LogsAndTours()
        {
            Tour = tour3,
            Logs = new List<LogEntry>()
        };

        [SetUp]
        public void Setup()
        {
            _service = new Services(new DatabaseHelper(), new MapQuestApiHelper(), new ImportExporthelper(), new DinkToPdfClass());

            listLogsAndTours = new List<LogsAndTours>();
            
            listLogsAndTours.Add(logsAndTour1);
            listLogsAndTours.Add(logsAndTour2);
            listLogsAndTours.Add(logsAndTour3);

        }
        private void MoreLogsAndTours()
        {
            TourEntry tour4 = new TourEntry(3, "wien berlin", "eine coole reise...", "29919324174129278439.jpg", "vienna", "berlin", "[]");
            TourEntry tour5 = new TourEntry(4, "wien hamburg", "eine coole reise...", "29919324174129278439.jpg", "vienna", "hamburg", "[]");
            TourEntry tour6 = new TourEntry(5, "wien hamburg", "eine coole reise...", "29919324174129278439.jpg", "vienna", "hamburg", "[]");
            TourEntry tour7 = new TourEntry(6, "wien hamburg", "eine coole reise...", "29919324174129278439.jpg", "vienna", "hamburg", "[]");
            LogEntry log2 = new LogEntry(1, 0, "abc", "abc", "5", "a", "aaa", "5.5", "5.2", "sunny", "no traffic", "very nice");
            LogEntry log3 = new LogEntry(2, 0, "abc", "abc", "5", "a", "aaa", "5.5", "5.2", "sunny", "no traffic", "very nice");
            LogEntry log4 = new LogEntry(3, 0, "abc", "abc", "5", "a", "aaa", "5.5", "5.2", "sunny", "no traffic", "very nice");
            LogEntry log5 = new LogEntry(4, 0, "abc", "abc", "5", "a", "aaa", "5.5", "5.2", "sunny", "no traffic", "very nice");
            LogEntry log6 = new LogEntry(5, 0, "abc", "abc", "5", "a", "aaa", "5.5", "5.2", "sunny", "no traffic", "very nice");
            listLogsAndTours.Add(new LogsAndTours()
            {
                Tour = tour5,
                Logs = new List<LogEntry>()
            });
            listLogsAndTours.Add(new LogsAndTours()
            {
                Tour = tour6,
                Logs = new List<LogEntry>()
            });
            listLogsAndTours.Add(new LogsAndTours()
            {
                Tour = tour7,
                Logs = new List<LogEntry>()
            });
            listLogsAndTours.Add(new LogsAndTours()
            {
                Tour = tour8,
                Logs = new List<LogEntry>()
                {
                    log2,log3,log4,log5,log6
                }
            });
        }
        private void SetupServicesDbMocBasic()
        {
            var DatabaseMoq = new Mock<IDatabaseHelper>();
            DatabaseMoq.Setup(x => x.TourData.AddTourToDatabase(It.IsAny<TourEntry>(), It.IsAny<NpgsqlConnection>())).Callback((TourEntry a, NpgsqlConnection b) => _tempstorageEntry = a);
            DatabaseMoq.Setup(x => x.TourData.UpdateTourInDatabase(It.IsAny<TourEntry>(), It.IsAny<NpgsqlConnection>())).Callback((TourEntry a, NpgsqlConnection b) => _tempstorageEntry = a);

            DatabaseMoq.Setup(x => x.LogData.AddLogToDatabase(It.IsAny<LogEntry>(), It.IsAny<NpgsqlConnection>())).Callback((LogEntry a, NpgsqlConnection b) => _tempstorageLog = a);
            DatabaseMoq.Setup(x => x.LogData.UpdateLogInDatabase(It.IsAny<LogEntry>(), It.IsAny<NpgsqlConnection>())).Callback((LogEntry a, NpgsqlConnection b) => _tempstorageLog = a);
            DatabaseMoq.Setup(x => x.LogData.AddLogToDatabase(It.IsAny<LogEntry>(),It.IsAny<int>(), It.IsAny<NpgsqlConnection>())).Callback((LogEntry a,int b, NpgsqlConnection c) => _tempstorageLog = a);


            var MapQuestMoq = new Mock<IMapQuestApiHelper>();
            
            MapQuestMoq.Setup(x => x.getMapImage(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult<string>("doesNotExist.jpg_123456789"));
            MapQuestMoq.Setup(x => x.getRoute(It.IsAny<string>(), It.IsAny<string>())).Returns(Task.FromResult<List<CustomManeuvers>>(new List<CustomManeuvers>()));

            
            var ImportExportHelper = new Mock<IImportExporthelper>();
            List<LogsAndTours> tempList = new List<LogsAndTours>() { new LogsAndTours() { Tour = tour, Logs = new List<LogEntry>() } };
            ImportExportHelper.Setup(x => x.SaveToFile(It.IsAny<string>(), It.IsAny<string>())).Callback((string a, string b) => _tempstringstorage = b);
            ImportExportHelper.Setup(x => x.ImportFromJsonFile(It.IsAny<string>())).Returns("[{\"Tour\":{\"Maneuvers\":[],\"Too\":\"berlin\",\"From\":\"vienna\",\"TourID\":0,\"Title\":\"wien berlin\",\"Description\":\"eine coole reise...\",\"ImgSource\":\"29919324174129278439.jpg\"},\"Logs\":[]}]");

            _service = new Services(DatabaseMoq.Object, MapQuestMoq.Object, ImportExportHelper.Object, new DinkToPdfClass());
        }
        [Test]
        public void SearchAndFindOneItemInTour()
        {
            List<LogsAndTours> result;

            result = _service.Search(listLogsAndTours, "berlin");

            Assert.Contains(logsAndTour1, result);
        }
        [Test]
        public void SearchAndFindOneItemInLog()
        {
            List<LogsAndTours> result;

            result = _service.Search(listLogsAndTours, "no traffic");

            Assert.Contains(logsAndTour2, result);
        }
        [Test]
        public void SearchAndFindNoItem()
        {
            List<LogsAndTours> result;

            result = _service.Search(listLogsAndTours, "hans");

            Assert.IsEmpty(result);
        }
        [Test]
        public void SearchAndFind2itemsOutof3()
        {
            List<LogsAndTours> result;

            result = _service.Search(listLogsAndTours, "hamburg");

            Assert.AreEqual(2,result.Count);
            Assert.Contains(logsAndTour2, result);
            Assert.Contains(logsAndTour3, result);
        }
        [Test]
        public void FindCurrentActiveLogs()
        {
            List<LogEntry> result;

            result = _service.CurrentActiveLogs(listLogsAndTours, tour2);

            CollectionAssert.AreEqual(new List<LogEntry>() { log }, result);
        }
        [Test]
        public void TourHasNoLogs()
        {
            List<LogEntry> result;

            result = _service.CurrentActiveLogs(listLogsAndTours, tour);

            CollectionAssert.AreEqual(new List<LogEntry>(), result);
        }
        [Test]
        public void TourDoesNotExistInList()
        {
            List<LogEntry> result;

            result = _service.CurrentActiveLogs(listLogsAndTours, tour8);

            CollectionAssert.AreEqual(new List<LogEntry>(), result);
        }
        [Test]
        public void SwitchViewModel()
        {

            MainViewModel mainViewModel = new MainViewModel();
            Type result;

            mainViewModel.RequestChangeViewModel("ReportView");
            result = mainViewModel.SelectedViewModel.GetType();

            Assert.AreEqual(typeof(ReportViewModel), result);
        }
        [Test]
        public void ViewModelToStringDoesNotexist()
        {


            MainViewModel mainViewModel = new MainViewModel();
            Type result;

            mainViewModel.RequestChangeViewModel("hansView");
            result = mainViewModel.SelectedViewModel.GetType();

            Assert.AreNotEqual(typeof(ReportViewModel), result);
            Assert.AreEqual(typeof(HomeViewModel), result);
        }

        [Test]
        public void ViewModelFromStringNull()
        {

            MainViewModel mainViewModel = new MainViewModel();
            Type result;

            mainViewModel.RequestChangeViewModel(null);
            result = mainViewModel.SelectedViewModel.GetType();

            Assert.AreEqual(typeof(HomeViewModel), result);
        }
        [Test]
        public void UmrechnungKmToMilesCustomManovers()
        {
            CustomManeuvers customManeuvers;
            string result;

            customManeuvers = new CustomManeuvers()
            {
                DistanceInMiles = "1"
            };

            result = customManeuvers.DistanceInKm();

            Assert.AreEqual("1,61", result);
        }

        [Test]
        public void TourUnchangedAfterBusinessLogikAdd()
        {
            SetupServicesDbMocBasic();
            _ = _service.AddNewTour("test", "wien", "berlin", "aaaaa").Result;


            Assert.AreEqual("test", _tempstorageEntry.Title);
            Assert.AreEqual("wien", _tempstorageEntry.From);
            Assert.AreEqual("berlin", _tempstorageEntry.Too);
            Assert.AreEqual("aaaaa", _tempstorageEntry.Description);

        }
        [Test]
        public void  TourUnchangedAfterBusinessLogikUpdate()
        {
            SetupServicesDbMocBasic();
            _ = _service.UpdateTour(0, "test", "aaaaa", "wien", "berlin", "abc").Result;


            Assert.AreEqual("test", _tempstorageEntry.Title);
            Assert.AreEqual("wien", _tempstorageEntry.From);
            Assert.AreEqual("berlin", _tempstorageEntry.Too);
            Assert.AreEqual("aaaaa", _tempstorageEntry.Description);
            Assert.AreEqual(0, _tempstorageEntry.TourID);
        }

        [Test]
        public void LogUnchangedAfterBusinessLogikAdd()
        {
            SetupServicesDbMocBasic();
            _ = _service.AddNewLog(log);

            Assert.AreEqual(0, _tempstorageLog.LogID);
            Assert.AreEqual(0, _tempstorageLog.TourID);
            Assert.AreEqual("abc", _tempstorageLog.Date);
            Assert.AreEqual("abc", _tempstorageLog.Duration);
            Assert.AreEqual("5", _tempstorageLog.Distance);
            Assert.AreEqual("a", _tempstorageLog.Rating);
            Assert.AreEqual("aaa", _tempstorageLog.Report);
            Assert.AreEqual("5.5", _tempstorageLog.AverageSpeed);
            Assert.AreEqual("5.2", _tempstorageLog.EnergyUsed);
            Assert.AreEqual("sunny", _tempstorageLog.Wheater);
            Assert.AreEqual("no traffic", _tempstorageLog.Traffic);
            Assert.AreEqual("very nice", _tempstorageLog.NicenessOfLocals);
        }
        [Test]
        public void LogUnchangedAfterBusinessLogikAddwithId()
        {
            SetupServicesDbMocBasic();
            _ = _service.UpdateLog(log);

            Assert.AreEqual(0, _tempstorageLog.LogID);
            Assert.AreEqual(0, _tempstorageLog.TourID);
            Assert.AreEqual("abc", _tempstorageLog.Date);
            Assert.AreEqual("abc", _tempstorageLog.Duration);
            Assert.AreEqual("5", _tempstorageLog.Distance);
            Assert.AreEqual("a", _tempstorageLog.Rating);
            Assert.AreEqual("aaa", _tempstorageLog.Report);
            Assert.AreEqual("5.5", _tempstorageLog.AverageSpeed);
            Assert.AreEqual("5.2", _tempstorageLog.EnergyUsed);
            Assert.AreEqual("sunny", _tempstorageLog.Wheater);
            Assert.AreEqual("no traffic", _tempstorageLog.Traffic);
            Assert.AreEqual("very nice", _tempstorageLog.NicenessOfLocals);
        }
        [Test]
        public void findDefaultIMage()
        {
            byte[] result;

            result = _service.ImageBytes("default.jpg");

            Assert.IsTrue(result.Length > 0);
        }
        [Test]
        public void findDefaultIMageNUll()
        {
            byte[] result;

            result = _service.ImageBytes(null);

            Assert.IsTrue(result.Length == 0);
        }
        [Test]
        public void findDefaultIMageUnknown()
        {
            byte[] result;

            result = _service.ImageBytes("aaaaaaaaaa.jpg.hz.ts.jpg.fz");

            Assert.IsTrue(result.Length > 0);
        }
        [Test]
        public void ImportFromFile()
        {
            SetupServicesDbMocBasic();

            _ = _service.ImportFile("a").Result;

            Assert.AreEqual("wien berlin", _tempstorageEntry.Title);
            Assert.AreEqual("vienna", _tempstorageEntry.From);
            Assert.AreEqual("berlin", _tempstorageEntry.Too);
            Assert.AreEqual("eine coole reise...", _tempstorageEntry.Description);
        }
        [Test]
        public void ExportToFile()
        {
            SetupServicesDbMocBasic();
            List<LogsAndTours> tempList2 = new List<LogsAndTours>() { new LogsAndTours() { Tour = tour, Logs = new List<LogEntry>() } };
            _ = _service.ExportFile("a", tempList2);
            Console.WriteLine(_tempstringstorage);
            Assert.AreEqual("[{\"Tour\":{\"Maneuvers\":[],\"Too\":\"berlin\",\"From\":\"vienna\",\"TourID\":0,\"Title\":\"wien berlin\",\"Description\":\"eine coole reise...\",\"ImgSource\":\"29919324174129278439.jpg\"},\"Logs\":[]}]", _tempstringstorage);
        }
    }
}