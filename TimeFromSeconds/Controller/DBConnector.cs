﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using Entities;
using Controller;

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

        public static HistoryList GetHistoryList()
        {
            HistoryList outList = new HistoryList();
            decimal aInput;
            TFS.Unit aUnit;
            DateTime aTime;
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=TFS.db;Version=3;"))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand("SELECT Input, Unit, Time FROM History", conn))
                    {
                        using (SQLiteDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                aInput = reader.GetDecimal(0);
                                aUnit = (TFS.Unit)reader.GetInt32(1);
                                aTime = reader.GetDateTime(2).ToLocalTime();
                                outList.AddItem(new HistoryItem(aInput, aUnit, aTime));
                            }//while
                        }//reader
                    }//cmd
                }//conn
                return outList;
            }//try
            catch(Exception e)
            {
                throw new SQLiteException($"Something went wrong while attempting to get history. Source: {e.Source}");
            }
        }//GetHistoryList()
        public static void Save(decimal aInput, TFS.Unit aUnit)
        {
            try
            {
                using (SQLiteConnection conn = new SQLiteConnection("Data Source=TFS.db;Version=3;"))
                {
                    conn.Open();
                    using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO History(Input, Unit, Time) VALUES (@tv1, @tv2, @tv3)", conn))
                    {
                        if (!(aInput <= 0))
                        {
                            cmd.Parameters.AddWithValue("@tv1", aInput);
                        }
                        else
                            throw new ArgumentOutOfRangeException($"Input cannot be less than or equal to zero. Input was {aInput}");
                        cmd.Parameters.AddWithValue("@tv2", (int)aUnit);
                        cmd.Parameters.AddWithValue("@tv3", DateTime.UtcNow);
                        cmd.ExecuteNonQuery();
                    }// using cmd
                }//using conn
            }//try
            catch(Exception e)
            {
                if (e is ArgumentOutOfRangeException)
                    throw new SQLiteException($"Attempted to store an invalid input value in database!\n    {e.Message}");
                else
                    throw new SQLiteException($"Something went wrong while attempting to store input in history! Source: {e.Source}");
            }//catch
        }//Save()
    }//DBConnector
}//namespace
