using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Swe2_tour_planer.Model;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Swe2_tour_planer.helpers
{
    public static class logEntryExtentions
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static public async void AddLog(this LogEntry newEntry)
        {
            try
            {
                string querystring = @$"Insert into LogEntry(tourID_fk,date,duration,distance,rating,report,averagespeed,energyused,wheater,traffic,nicenessoflocals) values (
                                    {newEntry.TourID},
                                    '{newEntry.Date}',
                                    '{newEntry.Duration}',
                                    '{newEntry.Distance}',
                                    '{newEntry.Rating}',
                                    '{newEntry.Report}',
                                    '{newEntry.AverageSpeed}',
                                    '{newEntry.EnergyUsed}',
                                    '{newEntry.Wheater}',
                                    '{newEntry.Traffic}',
                                    '{newEntry.NicenessOfLocals}',
                                    );";
                var conn = Databasehelper.ConnectObj();
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {
                log.Error("inserting new Log into db failed");
                log.Debug(e.StackTrace);
                log.Debug(e.Message);
                throw new Exception();
            }
        }

        static public async void RemoveLog(this LogEntry newEntry)
        {
            try
            {
                string querystring = @$"Delete from TourEntry where logID = {newEntry.LogID} ";
                var conn = Databasehelper.ConnectObj();
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {
                log.Error("Remove of Log failed");
                log.Debug(e.StackTrace);
                log.Debug(e.Message);
                throw new Exception();
            }
        }

        static public async void UpdateLog(this LogEntry updateEntry)
        {
            try
            {
                string querystring = @$"Update LogEntry set 
                                date='{updateEntry.Date}',
                                duration='{updateEntry.Duration}',
                                distance='{updateEntry.Distance}',
                                rating='{updateEntry.Rating}',
                                report='{updateEntry.Report}',
                                averagespeed='{updateEntry.AverageSpeed}',
                                energyused='{updateEntry.EnergyUsed}',
                                wheater='{updateEntry.Wheater}',
                                traffic='{updateEntry.Traffic}',
                                nicenessoflocals='{updateEntry.NicenessOfLocals}' where logID = {updateEntry.LogID}";
                var conn = Databasehelper.ConnectObj();
                using (NpgsqlCommand command = new NpgsqlCommand(querystring, conn))
                {
                    await command.ExecuteNonQueryAsync();
                }
            }
            catch (Exception e)
            {
                log.Error("Update of Logs failed");
                log.Debug(e.StackTrace);
                log.Debug(e.Message);
                throw new Exception();
            }
        }
    }
}
