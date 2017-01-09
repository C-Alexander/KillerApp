using System.Collections.Generic;
using Shadow_Arena.Contexts;
using Shadow_Arena.Models;

namespace Shadow_Arena.Repositories
{
    class PlayerRepository : IPlayerRepository
    {
        private IPlayerContext _context;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public PlayerRepository(IPlayerContext context)
        {
            _context = context;
        }

        public void Add(Player player)
        {
            _context.Add(player);
        }

        public void Delete(Player player)
        {
            _context.Delete(player);
        }

        public void Update(Player player)
        {
            _context.Update(player);
        }

        public ICollection<Player> Read()
        {
            return _context.Read();
        }
    }
}
