using System.Collections.Generic;
using System.Data.SqlClient;

namespace UNiDAYS.Identity.Repositories
{
    public interface ISqlTranslator<TData> where TData : class
    {
        TData Translate(SqlDataReader reader, bool isList = false);
        List<TData> TranslateAsList(SqlDataReader reader);
    }
}
