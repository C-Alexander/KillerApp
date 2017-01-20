using System.Collections.Generic;
using Shadow_Arena.Models;

namespace Shadow_Arena.Repositories
{
    interface IClassRepository
    {
        void Delete(Class classToDelete);
        ICollection<Class> Read();
        Class Read(int classid);
        Class Read(Class classToRead);
    }
}