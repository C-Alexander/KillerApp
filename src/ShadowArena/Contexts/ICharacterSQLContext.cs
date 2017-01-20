using System.Collections.Generic;
using Shadow_Arena.Models;

namespace Shadow_Arena.Contexts
{
    interface ICharacterSQLContext
    {
        void Add(Character character);
        void Delete(Character character);
        ICollection<Character> Read();
        void Update(Character character);
    }
}