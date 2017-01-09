using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalFun2Application
{
    interface IPlayerRepository
    {
        void add(PlayerModel player);
        void delete(PlayerModel player);
        void update(PlayerModel player);
        ICollection<PlayerModel> read();
    }
}
