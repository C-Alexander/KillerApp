﻿using System.Collections.Generic;
using System.Linq;
using Shadow_Arena.Contexts;
using Shadow_Arena.Models;

namespace Shadow_Arena.Repositories
{
    class CharacterRepository : ICharacterRepository
    {
        private ICharacterSQLContext _context;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public CharacterRepository(ICharacterSQLContext context)
        {
            _context = context;
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
