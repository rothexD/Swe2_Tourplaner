using Microsoft.Extensions.Configuration;
using Npgsql;
using Swe2_tour_planer.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Swe2_tour_planer.Services
{
    public class Database : IDatabase
    {
        private static readonly IConfiguration config = new ConfigurationBuilder().AddJsonFile("Appsettings.json", false, true).Build();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        public readonly ILogEntryDatabase _logData = new LogEntryDatabase();
        public readonly ITourEntryDatabase _tourData = new TourEntryDatabase();

        public NpgsqlConnection Create()
        {
            var i = new NpgsqlConnection(config["Database:ConnectionString"]);
            i.Open();
            return i;
        }
        public ILogEntryDatabase LogData
        {
            get
            {
                return _logData;
            }
        }
        public ITourEntryDatabase TourData
        {
            get
            {
                return _tourData;
            }
        }
        public Database(bool ShouldDoTableExistsCheck = false)
        {
            if (ShouldDoTableExistsCheck)
            {
                this.InitialSetupTableExists();
            }
        }
        public async void InitialSetupTableExists()
        {
            try
            {
                var conn = Create();
                string querystring = @"Create table if not exists TourEntry(
                                        tourID serial primary key,
                                        title varchar,
                                        description varchar,
                                        imgSource varchar,
                                        fromL varchar,
                                        too varchar,
                                        maneuvers varchar
                                    );";
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    await command.ExecuteNonQueryAsync();
                }
                querystring = @"Create table if not exists LogEntry(
                                        logID serial primary key,
                                        tourID_fk int,
                                        date timestamp,
                                        duration varchar,
                                        distance varchar,
                                        rating varchar,
                                        report varchar,
                                        averagespeed varchar,
                                        energyused varchar,
                                        wheater varchar,
                                        traffic varchar,
                                        nicenessoflocals varchar,
                                        FOREIGN KEY(tourID_fk) REFERENCES TourEntry(tourID) ON DELETE CASCADE
                                    );";
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    await command.ExecuteNonQueryAsync();
                }
                conn.Close();
            }
            catch (Exception e)
            {
                log.Fatal("failure in initialSetupTableExists");
                log.Debug(e.StackTrace);
                Environment.Exit(1);
            }
        }


        public async Task<ObservableCollection<TourEntry>> GetListOfToursAsync()
        {
            try
            {
                string querystring = @$"Select * from TourEntry;";
                var conn = Create();
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    ObservableCollection<TourEntry> TourList = new ObservableCollection<TourEntry>();

                    if (reader.HasRows == false)
                    {
                        return TourList;
                    }

                    while (reader.Read())
                    {
                        var item = new TourEntry(Int32.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString(), reader[6].ToString());
                        TourList.Add(item);
                    }
                    log.Debug("get List of tours success");
                    conn.Close();
                    return TourList;

                }
            }
            catch (Exception e)
            {
                log.Error("get List of tours failed");
                log.Debug(e.StackTrace); 
                throw;
            }
        }
        public async Task<ObservableCollection<LogEntry>> GetListOfLogsAsync(int TourID)
        {
            try
            {
                string querystring = @$"Select * from LogEntry where tourID_fk = @tourID";
                var conn = Create();
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    command.Parameters.AddWithValue("tourID", TourID);
                    NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                    ObservableCollection<LogEntry> TourList = new ObservableCollection<LogEntry>();

                    if (reader.HasRows == false)
                    {
                        return TourList;
                    }

                    while (reader.Read())
                    {
                        var item = new LogEntry(Int32.Parse(reader[0].ToString()), Int32.Parse(reader[1].ToString()), Convert.ToDateTime(reader[2].ToString()), reader[3].ToString(),
                            reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString(), reader[10].ToString(), reader[11].ToString());
                        TourList.Add(item);
                    }
                    reader.Close();
                    conn.Close();
                    return TourList;
                }
            }
            catch (Exception e)
            {
                log.Error("get List of Logs failed");
                log.Debug(e.StackTrace); 
                throw;
            }
        }

        public async Task<int> RemoveTourAsync(int ID)
        {
            try
            {
                string querystring = @$"Delete from TourEntry where tourID = @ID ";
                var conn = Create();
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    command.Parameters.AddWithValue("ID", ID);
                    await command.ExecuteNonQueryAsync();
                }
                conn.Close();
                return 0;
            }
            catch (Exception e)
            {
                log.Error("Removing of Tour failed");
                log.Debug(e.StackTrace); 
                throw;
            }
        }

        public async Task<int> RemoveLogAsync(int ID)
        {
            try
            {
                string querystring = @$"Delete from LogEntry where logID = @ID ";
                var conn = Create();
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    command.Parameters.AddWithValue("ID", ID);
                    await command.ExecuteNonQueryAsync();

                }
                conn.Close();
                return 0;
            }
            catch (Exception e)
            {
                log.Error("Remove of Log failed");
                log.Debug(e.StackTrace);
                throw;
            }
        }
        public async Task<int> RemoveAllTourAsync()
        {
            try
            {
                string querystring = @$"Delete from TourEntry";
                var conn = Create();
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    await command.ExecuteNonQueryAsync();
                }
                conn.Close();
                return 0;
            }
            catch (Exception e)
            {
                log.Error("Removing of All Tour failed");
                log.Debug(e.StackTrace); 
                throw;
            }
        }

        public async Task<int> RemoveAllLogAsync()
        {
            try
            {
                string querystring = @$"Delete from LogEntry";
                var conn = Create();
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    await command.ExecuteNonQueryAsync();

                }
                conn.Close();
                return 0;
            }
            catch (Exception e)
            {
                log.Error("Remove of All Log failed");
                log.Debug(e.StackTrace); 
                throw;
            }
        }
    }
}
