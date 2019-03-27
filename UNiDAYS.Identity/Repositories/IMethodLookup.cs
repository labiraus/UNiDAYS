using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UNiDAYS.Identity.Repositories
{
    public interface IMethodLookup
    {
        string StoredProcedure(string methodName);
    }
}
