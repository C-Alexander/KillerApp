using System.Collections.Generic;
using ShadowArena.Models;

namespace DalFun2Application
{
    public interface IPlayerRepository
    {
        void Add(Player player);
        void Delete(Player player);
        void Update(Player player);
        ICollection<Player> Read();
    }
}
