using System.Collections.Generic;
using Shadow_Arena.Models;

namespace Shadow_Arena.Contexts
{
    interface IClassSQLContext
    {
        void Delete(Class classToDelete);
        ICollection<Class> Read();
    }
}