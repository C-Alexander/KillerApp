using System.Data.Common;

namespace Shadow_Arena.Contexts
{
    public interface IDatabaseManager
    {
        DbConnection Connection { get; set; }

        DbDataReader RunCommand(DbCommand command);
        void RunCommandNonQuery(DbCommand command);
    }
}