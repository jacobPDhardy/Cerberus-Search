using Microsoft.Data.Sqlite;

namespace Gate_Solver
{
    public static class SearchLogs
    {
        private const string dbpath = @"..\..\..\hxt-264.db";
        public static async Task<List<Log>> SimpleSearch(string search)
        {
            List<Log> logs = new List<Log>();
            logs = await Task.Run(() => SQLSearch(search));
            return logs;
        }

        private static List<Log> SQLSearch(string search)
        {

            List<Log> logs = new List<Log>();

            string searchQuery = "";
            if(string.IsNullOrEmpty(search))
            {
                searchQuery = "SELECT * FROM Logs";
            }
            else
            {
                 searchQuery = $"SELECT * FROM Logs WHERE ((coalesce(id,\"\") || coalesce(Timestamp,\"\") || coalesce(Level,\"\") || coalesce(Exception,\"\") || coalesce(RenderedMessage,\"\") || coalesce(Properties,\"\")) LIKE '%{search}%')";
            }

            SqliteConnection conn = new SqliteConnection($"Data Source={dbpath}");
            conn.Open();

            SqliteCommand command = conn.CreateCommand();
            command.CommandText = searchQuery;

            using (var reader = command.ExecuteReader())
            {
                while (reader.Read())
                {

                    Log log = new Log(reader.GetStringNullSafe(0), reader.GetStringNullSafe(1), reader.GetStringNullSafe(2), reader.GetStringNullSafe(3), reader.GetStringNullSafe(4), reader.GetStringNullSafe(5));
                    logs.Add(log);
                }
               // reader.Close();
            }
          //  conn.Close();
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
