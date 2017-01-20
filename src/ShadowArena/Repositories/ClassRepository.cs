using System.Collections.Generic;
using System.Linq;
using Shadow_Arena.Contexts;
using Shadow_Arena.Models;

namespace Shadow_Arena.Repositories
{
    class ClassRepository : IClassRepository
    {
        private IClassSQLContext _context;

        /// <summary>Initializes a new instance of the <see cref="T:System.Object" /> class.</summary>
        public ClassRepository(IClassSQLContext context)
        {
            _context = context;
        }

        public void Delete(Class classToDelete)
        {
            _context.Delete(classToDelete);
        }

        public ICollection<Class> Read()
        {
            return _context.Read();
        }

        public Class Read(int classid)
        {
            return Read().FirstOrDefault(p => p.Id == classid);
        }

        public Class Read(Class classToRead)
        {
            return Read(classToRead.Id);
        }
    }
}
