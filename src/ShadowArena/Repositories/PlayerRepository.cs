using System.Collections.Generic;
using ShadowArena.Models;

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

        public void add(Player player)
        {
            context.add(player);
        }

        public void delete(Player player)
        {
            context.delete(player);
        }

        public void update(Player player)
        {
            context.update(player);
        }

        public ICollection<Player> read()
        {
            return context.read();
        }
    }
}
