using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace Controller
{
    public static class DBConnector
    {
        public static void Initialize()
        {
            using (SQLiteConnection conn = new SQLiteConnection("Data Source=TFS.db;Version=3;"))
            {
                using(SQLiteCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "" +
                        "CREATE TABLE IF NOT EXISTS History (" +
                        "Input REAL NOT NULL," +
                        "Unit INTEGER NOT NULL," +
                        "Time NUMERIC NOT NULL," +
                        "PRIMARY KEY (Input,Time));";
                    cmd.ExecuteNonQuery();
                }
            }//conn
        }//Initialize()
    }//DBConnector
}//namespace
