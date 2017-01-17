using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;
using Shadow_Arena.Contexts;
using Xunit;

namespace XTests
{
    public class DatabaseManagerTests
    {
        [Fact(DisplayName = "Can open a database connection")]
        public void TestDatabaseConnection()
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddConsole();
            DatabaseManager dbManager = new DatabaseManager(loggerFactory.CreateLogger<DatabaseManager>());
            dbManager.Open(); //if it crashes itl fail.
            //mm it should be private maybe.. convenient for testing though.
        }
    }
}
