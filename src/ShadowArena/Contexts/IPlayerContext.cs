using System.Collections.Generic;
using ShadowArena.Models;

namespace DalFun2Application
{
    interface IPlayerContext
    {
        void add(Player player);
        void delete(Player player);
        void update(Player player);
        ICollection<Player> read();
    }
}
