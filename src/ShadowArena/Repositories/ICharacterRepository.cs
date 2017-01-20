using System.Collections.Generic;
using Shadow_Arena.Models;

namespace Shadow_Arena.Repositories
{
    public interface ICharacterRepository
    {
        void Add(Character character);
        void Delete(Character character);
        ICollection<Character> Read();
        Character Read(int characterid);
        Character Read(Character character);
        void Update(Character character);
    }
}