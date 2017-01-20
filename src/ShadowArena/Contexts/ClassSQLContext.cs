using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Shadow_Arena.Models;

namespace Shadow_Arena.Contexts
{
    class ClassSQLContext : IClassSQLContext
    {
        private IDatabaseManager DatabaseManager { get; set; }
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ClassSQLContext(IDatabaseManager dbManager)
        {
            DatabaseManager = dbManager;
        }

        public void Delete(Class classToDelete)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "DELETE FROM Class" +
                               " WHERE Class.id = @id";
            cmd.Parameters.AddWithValue("@id", classToDelete.Id);
            DatabaseManager.RunCommandNonQuery(cmd);
        }

        public ICollection<Class> Read()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Class JOIN Stat on Stat.id = Class.Statid";
            DbDataReader reader = DatabaseManager.RunCommand(cmd);
            ICollection<Class> classList = new List<Class>();
            while (reader.Read())
            {
                Class newClassModel = new Class();
                newClassModel.Id = reader.GetInt32(0);
                newClassModel.Statid = reader.GetInt32(1);
                if (reader.IsDBNull(2))
                {
                    newClassModel.Name = null;
                }
                else
                {
                    newClassModel.Name = reader.GetString(2);
                }
                newClassModel.Stat = new Stat()
                {
                    Id = reader.GetInt32(3),
                    Attack = reader.GetInt32(4),
                    Defense = reader.GetInt32(5),
                    MagicAttack = reader.GetInt32(6),
                    MagicDefence = reader.GetInt32(7),
                    Vitality = reader.GetInt32(8),
                    Intelligence = reader.GetInt32(9)
                };
                classList.Add(newClassModel);
            }
            return classList;
        }
    }
}
