using System.Collections.Generic;
using Shadow_Arena.Models;

namespace Shadow_Arena.Repositories
{
    public interface IPlayerRepository
    {
        void Add(Player player);
        void Delete(Player player);
        void Update(Player player);
        ICollection<Player> Read();
    }
}
