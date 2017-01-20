using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using Shadow_Arena.Models;

namespace Shadow_Arena.Contexts
{
    class CharacterSQLContext : ICharacterSQLContext
    {
        private IDatabaseManager DatabaseManager { get; set; }
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public CharacterSQLContext(IDatabaseManager dbManager)
        {
            DatabaseManager = dbManager;
        }

        public void Add(Character character)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "CreateCharacter";
            cmd.Parameters.AddWithValue("@Name", character.Name);
            cmd.Parameters.AddWithValue("@ClassID", character.Classid);
            if (character.OwningPlayerid != null)
            {
                cmd.Parameters.AddWithValue("@PlayerID", character.OwningPlayerid);
            }
            else
            {
                cmd.Parameters.AddWithValue("@PlayerID", DBNull.Value);
            }
            DatabaseManager.RunCommandNonQuery(cmd);
        }

        public void Delete(Character character)
        {
            SqlCommand cmd = new SqlCommand(); //FK ensures the stat is deleted.
            cmd.CommandText = "DELETE FROM Character" +
                               " WHERE Character.id = @id";
            cmd.Parameters.AddWithValue("@id", character.Id);
            DatabaseManager.RunCommandNonQuery(cmd);
        }

        public void Update(Character character)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "UPDATE Character SET name=@name" +
                               " WHERE Character.id = @id";
            cmd.Parameters.AddWithValue("@id", character.Id);
            cmd.Parameters.AddWithValue("@name", character.Name);
            DatabaseManager.RunCommandNonQuery(cmd);
        }

        public ICollection<Character> Read()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT * FROM Character JOIN Stat ON Character.Statid = Stat.id";
            DbDataReader reader = DatabaseManager.RunCommand(cmd);
            ICollection<Character> characterList = new List<Character>();
            while (reader.Read())
            {
                Character newCharacterModel = new Character();
                newCharacterModel.Id = reader.GetInt32(0);
                if (!reader.IsDBNull(1))
                    newCharacterModel.Name = reader.GetString(1);
                if (!reader.IsDBNull(2))
                newCharacterModel.OwningPlayerid = reader.GetInt32(2);
                newCharacterModel.Classid = reader.GetInt32(3);
                newCharacterModel.Statid = reader.GetInt32(4);
                newCharacterModel.Stat = new Stat()
                {
                    Id = reader.GetInt32(5),
                    Attack = reader.GetInt32(6),
                    Defense = reader.GetInt32(7),
                    MagicAttack = reader.GetInt32(8),
                    MagicDefence = reader.GetInt32(9),
                    Vitality = reader.GetInt32(10),
                    Intelligence = reader.GetInt32(11)
                };
                characterList.Add(newCharacterModel);
            }
            return characterList;
        }
    }
}
