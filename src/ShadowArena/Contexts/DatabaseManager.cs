using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void runCommandNonQuery(DbCommand command)
        {
            Open();
            command.Connection = connection;
            command.ExecuteNonQuery();
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
