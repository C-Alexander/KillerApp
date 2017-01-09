using DalFun2Application;
using Shadow_Arena.Contexts;
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
