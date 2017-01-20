using System.Collections.Generic;
using Shadow_Arena.Models;

namespace Shadow_Arena.Contexts
{
    public interface IItemSQLContext
    {
        void AddItemToCharacter(Item itemToAdd, Character c);
        ICollection<Item> Read();
    }
}