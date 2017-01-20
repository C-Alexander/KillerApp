using System.Collections.Generic;
using System.Linq;
using Shadow_Arena.Contexts;
using Shadow_Arena.Models;

namespace Shadow_Arena.Repositories
{
    class CharacterRepository : ICharacterRepository
    {
        private ICharacterSQLContext _context;
        private readonly IClassRepository _classRepo;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public CharacterRepository(ICharacterSQLContext context, IClassRepository classRepo)
        {
            _context = context;
            _classRepo = classRepo;
        }

        public void Add(Character character)
        {
            _context.Add(character);
        }

        public void Delete(Character character)
        {
            _context.Delete(character);
        }

        public void Update(Character character)
        {
            _context.Update(character);
        }

        public ICollection<Character> Read()
        {
            ICollection<Character> listCharacters = _context.Read();
            foreach (Character c in listCharacters)
            {
                c.Class = _classRepo.Read(c.Classid);
            }
            return _context.Read();
        }

        public Character Read(int characterid)
        {
            return Read().FirstOrDefault(p => p.Id == characterid);
        }

        public Character Read(Character character)
        {
            return Read(character.Id);
        }
    }
}
