using Microsoft.Data.Sqlite;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Cerberus_Search_Complete
{
    public static class LogsDatabase //DONE - Needs to be made compatible with HXT264.db 
    {
        public static async Task<List<Log>> GetAllLogs()
        {
            return await SimpleSearch("");
        }

        public static async Task<List<Log>> SimpleSearch(string search)
        {
            List<Log> logs = new List<Log>();
            logs = await Task.Run(() => SQLSearch(search));
            return logs;
        }

        private const string dbpath = @"..\..\..\hxt-264.db"; //To be removed see below comment
        private static List<Log> SQLSearch(string search) //Needs to be adapted to HXT264Log
        {
            List<Log> logs = new List<Log>();

            string searchQuery = "SELECT * FROM Logs";
            if(!string.IsNullOrEmpty(search))
            {
                 searchQuery += $" WHERE ((coalesce(id,\"\") || coalesce(Timestamp,\"\") || coalesce(Level,\"\") || coalesce(Exception,\"\") || coalesce(RenderedMessage,\"\") || coalesce(Properties,\"\")) LIKE '%{search}%')";
            }

            using (SqliteConnection sqliteConnection = new SqliteConnection($"Data Source={dbpath}")) //Change connection string to Globals.SQL.Connections.DB and remove dp path variable
            using (SqliteCommand sqliteCommand = new SqliteCommand(searchQuery, sqliteConnection))
            {
                sqliteConnection.Open();
                using (var sqliteDataReader = sqliteCommand.ExecuteReader())
                {
                    while (sqliteDataReader.Read())
                    { 
                        Log log = new Log(sqliteDataReader.GetStringNullSafe(0), sqliteDataReader.GetStringNullSafe(1), sqliteDataReader.GetStringNullSafe(2), sqliteDataReader.GetStringNullSafe(3), sqliteDataReader.GetStringNullSafe(4), sqliteDataReader.GetStringNullSafe(5));
                        logs.Add(log);
                    }
                    sqliteDataReader.Close();
                }

            }
            return logs;
        }

        private static string GetStringNullSafe(this SqliteDataReader reader, int columnIndex)
        {
            if (!reader.IsDBNull(columnIndex))
            {
                return reader.GetString(columnIndex);

            }
            else
            {
                return string.Empty;
            }
        }

    }
}
