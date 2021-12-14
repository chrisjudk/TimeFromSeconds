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
        private static bool isInitialized = false;
        public static bool IsInitialized
        {
            get
            {
                return isInitialized;
            }
            private set
            {
                isInitialized = value;
            }
        }
        public static void Initialize()
        {
            if (!IsInitialized)
            {
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=TFS.db;Version=3;"))
                {
                    try
                    {
                        conn.Open();
                        using (SQLiteCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = "" +
                                "CREATE TABLE IF NOT EXISTS History (" +
                                "Input REAL NOT NULL," +
                                "Unit INTEGER NOT NULL," +
                                "Time NUMERIC NOT NULL," +
                                "PRIMARY KEY (Input,Time));";
                            cmd.ExecuteNonQuery();
                        }//cmd
                        IsInitialized = true;
                    }//try
                    catch (Exception e)
                    {
                        IsInitialized = false;
                        throw new SQLiteException("An error occured while attempting to initialize the database. Source of eror = " + e.Source);
                    }
                }//conn
            }//fi
        }//Initialize()
    }//DBConnector
}//namespace
