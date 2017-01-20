using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlClient;
using Shadow_Arena.Contexts;
using Shadow_Arena.Models;

namespace Shadow_Arena.Contexts
{
    public class ItemSQLContext : IItemSQLContext
    {
        private IDatabaseManager DatabaseManager { get; set; }
        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> item.</summary>
        public ItemSQLContext(IDatabaseManager dbManager)
        {
            DatabaseManager = dbManager;
        }

        public void AddItemToCharacter(Item itemToAdd, Character c)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "INSERT INTO CharacterItem(owningCharacterid, ownedItemid" +
                              "VALUES(@owner, @item)";
            cmd.Parameters.AddWithValue("@owner", c.Id);
            cmd.Parameters.AddWithValue("@item", c.Id);
            DatabaseManager.RunCommandNonQuery(cmd);
        }

        public ICollection<Item> Read()
        {
            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = "SELECT id, price, name FROM Item";
            DbDataReader reader = DatabaseManager.RunCommand(cmd);
            ICollection<Item> itemList = new List<Item>();
            while (reader.Read())
            {
                Item newItemModel = new Item();
                newItemModel.Id = reader.GetInt32(0);
                newItemModel.Price = reader.GetInt32(1);
                if (reader.IsDBNull(2))
                {
                    newItemModel.Name = null;
                }
                else
                {
                    newItemModel.Name = reader.GetString(2);
                }
                itemList.Add(newItemModel);
            }
            return itemList;
        }
    }
}
