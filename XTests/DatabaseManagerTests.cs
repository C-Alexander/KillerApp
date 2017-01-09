using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using DalFun2Application;
using Remotion.Linq.Clauses;
using Xunit;

namespace XTests
{
    public class DatabaseManagerTests
    {
        [Fact(DisplayName = "Can open a database connection")]
        public void TestDatabaseConnection()
        {
            DatabaseManager dbManager = new DatabaseManager();
            dbManager.Connection.Open(); //if it crashes itl fail.
            //mm it should be private maybe.. convenient for testing though.
        }
    }
}
