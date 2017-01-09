using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalFun2Application
{
    class PlayerRepository : IPlayerRepository
    {
        private IPlayerContext context;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public PlayerRepository(IPlayerContext context)
        {
            this.context = context;
        }

        public void add(PlayerModel player)
        {
            context.add(player);
        }

        public void delete(PlayerModel player)
        {
            context.delete(player);
        }

        public void update(PlayerModel player)
        {
            context.update(player);
        }

        public ICollection<PlayerModel> read()
        {
            return context.read();
        }
    }
}
