using System;
using System.Data.Common;
using System.Data.SqlClient;

namespace DalFun2Application
{
    public class DatabaseManager
    {
        private DbConnection connection = new SqlConnection();

        public DbConnection Connection
        {
            get { return connection; }
            set { connection = value; }
        }

         void Open()
        {
            try
            {
                if (connection.State != System.Data.ConnectionState.Open)
                {
                    connection.ConnectionString =
                        @"Server = (localdb)\\mssqllocaldb; Database = aspnet - Shadow_Arena - 886aa145 - e488 - 49d1 - 95f5 - 9630692916a6; Trusted_Connection = True; MultipleActiveResultSets = true";
                    //change for security reasons!!!
                    connection.Open();
                }
                else
                {
                    Console.WriteLine("Tried to open connection, state was: " + connection.State);
                    Close();
                    Open();
                }
            }
            catch (SqlException e )
            {
                Console.WriteLine(e.StackTrace); // honestly, it should use the logger and probably tbh it shouldnt handle this to begin with - asp.net will hide and fail to display the page on an error by default.
                //thats how it should be. God knows the ungodly effects of a broken db connection on a dynamic site....
            }
        }

        public void runCommandNonQuery(DbCommand command)
        {
            Open();
            command.Connection = connection;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                Console.WriteLine(e.StackTrace);
                
            }
            Close();
        }

        public DbDataReader runCommand(DbCommand command)
        {
            Open();
                command.Connection = connection;
                DbDataReader reader = command.ExecuteReader();
            return reader;
        }

        void Close()
        {
            if (connection.State == System.Data.ConnectionState.Open)
            {
                connection.Close();
            }
            else throw new Exception("Tried to close a connection that was not open");
        }
    }
}
