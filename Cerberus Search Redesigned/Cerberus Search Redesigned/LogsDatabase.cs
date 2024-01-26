using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cerberus_Search_Redesigned
{
    public static class LogsDatabase //DONE
    {
        public static async Task<List<HXT264Log>> GetAllLogs()
        {
            return await SimpleSearch("");
        }

        public static async Task<List<HXT264Log>> SimpleSearch(string search)
        {
            List<HXT264Log> logs = new List<HXT264Log>();
            logs = await Task.Run(() => SQLSearch(search));
            return logs;
        }

        private static List<HXT264Log> SQLSearch(string search)
        {
            List<HXT264Log> logs = new List<HXT264Log>();

            string searchQuery = "SELECT * FROM Logs";
            if (!string.IsNullOrEmpty(search))
            {
                searchQuery += $" WHERE ((coalesce(id,\"\") || coalesce(Timestamp,\"\") || coalesce(Level,\"\") || coalesce(Exception,\"\") || coalesce(RenderedMessage,\"\") || coalesce(Properties,\"\")) LIKE '%{search}%')";
            }

            using (SqliteConnection sqliteConnection = new SqliteConnection(Globals.SQL.Connections.DB))
            using (SqliteCommand sqliteCommand = new SqliteCommand(searchQuery, sqliteConnection))
            {
                sqliteConnection.Open();
                using (var sqliteDataReader = sqliteCommand.ExecuteReader())
                {
                    while (sqliteDataReader.Read())
                    {
                        long id = sqliteDataReader.GetInt64("id");
                        DateTime timestamp = sqliteDataReader.GetDateTime("Timestamp");
                        string level = sqliteDataReader.GetString("Level");
                        string exception = sqliteDataReader.GetString("Exception");
                        string properties = sqliteDataReader.GetString("Properties");
                        string renderedMessage = sqliteDataReader.GetString("RenderedMessage");

                        HXT264Log log = new HXT264Log(id, timestamp, level, exception, properties, renderedMessage);
                        logs.Add(log);
                    }
                    sqliteDataReader.Close();
                }

            }
            return logs;
        }
    }
}
