﻿using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Swe2_tour_planer.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Swe2_tour_planer.helpers
{
    public class Databasehelper
    {
        private static readonly string _Connectstring = "Host=localhost;Username=postgres;Password=postgres;Database=swe1";
        public static NpgsqlConnection ConnectObj()
        {
            return new NpgsqlConnection(_Connectstring);
        }
        public Databasehelper(bool ShouldDoTableExistsCheck = false)
        {
            if (ShouldDoTableExistsCheck)
            {
                this.initialSetupTableExists();
            }
        }
        public async void initialSetupTableExists()
        {
            var conn = ConnectObj();
            string querystring = @"Create table if not exists TourEntry(
                                        tourID serial primary key,
                                        title varchar,
                                        description varchar,
                                        imgSource varchar,
                                        from varchar,
                                        too varchar
                                    );";
            using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
            {
                await command.ExecuteNonQueryAsync();
            }
            querystring = @"Create table if not exists LogEntry(
                                        logID serial primary key,
                                        tourID_fk int,
                                        date varchar,
                                        duration varchar,
                                        distance varchar,
                                        rating varchar,
                                        report varchar,
                                        averagespeed varchar,
                                        energyused varchar,
                                        wheater varchar,
                                        traffic varchar,
                                        nicenessoflocals varchar,
                                        FOREIGN KEY(tourID_fk) REFERENCES Tour(tourID) ON DELETE CASCADE
                                    );";
            using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
            {
                await command.ExecuteNonQueryAsync();
            }

        }
        
        
        static public async Task<ObservableCollection<TourEntry>> GetListOfTours()
        {
            string querystring = @$"Select * from TourEntry;";
            var conn = ConnectObj();
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
                    var item = new TourEntry(Int32.Parse(reader[0].ToString()), reader[1].ToString(), reader[2].ToString(), reader[3].ToString(), reader[4].ToString(), reader[5].ToString());
                    TourList.Add(item);
                }
                return TourList;
            }
        }
        static public async Task<ObservableCollection<LogEntry>> GetListOfLogs(int TourID)
        {
            string querystring = @$"Select * from LogEntry where tourID_fk = {TourID}";
            var conn = ConnectObj();
            using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
            {
                NpgsqlDataReader reader = await command.ExecuteReaderAsync();
                ObservableCollection<LogEntry> TourList = new ObservableCollection<LogEntry>();

                if (reader.HasRows == false)
                {
                    return TourList;
                }

                while (reader.Read())
                {
                    var item = new LogEntry(Int32.Parse(reader[0].ToString()), Int32.Parse(reader[1].ToString()), reader[2].ToString(), reader[3].ToString(),
                        reader[4].ToString(), reader[5].ToString(), reader[6].ToString(), reader[7].ToString(), reader[8].ToString(), reader[9].ToString(), reader[10].ToString(), reader[11].ToString());
                    TourList.Add(item);
                }
                return TourList;
            }
        }
        

    }
}
