using System;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq.Expressions;
using Microsoft.Extensions.Logging;

namespace Shadow_Arena.Contexts
{
    public class DatabaseManager : IDatabaseManager
    {
        private DbConnection _connection = new SqlConnection();
        private ILogger<DatabaseManager> logger;

        public DatabaseManager(ILogger<DatabaseManager> logger)
        {
            this.logger = logger;
        }

        public DbConnection Connection
        {
            get { return _connection; }
            set { _connection = value; }
        }

         void Open()
        {
            try
            {
                if (_connection.State != System.Data.ConnectionState.Open)
                {
                    _connection.ConnectionString =
                        @"Server=tcp:sagamedbserver.database.windows.net,1433;Initial Catalog=ShadowBeta_db;Persist Security Info=False;User ID=sa_admin;Password= P-5:.Z:bRHu}?NgQ;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
                    //change for security reasons!!!
                    _connection.Open();
                }
                else
                {
                    logger.LogWarning("Tried to open connection, state was: " + _connection.State);
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

        public void RunCommandNonQuery(DbCommand command)
        {
            Open();
            command.Connection = _connection;
            try
            {
                command.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                logger.LogError(e.StackTrace);
                
            }
            Close();
        }

        public DbDataReader RunCommand(DbCommand command)
        {
            Open();
                command.Connection = _connection;
                DbDataReader reader = command.ExecuteReader();
            return reader;
        }

        void Close()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
            else throw new Exception("Tried to close a connection that was not open");
        }
    }
}
