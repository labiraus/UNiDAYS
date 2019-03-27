using System.Collections.Generic;
using System.Data.SqlClient;
using UNiDAYS.Identity.Models;

namespace UNiDAYS.Identity.Repositories
{
    public class UserSqlTranslator : ISqlTranslator<UserModel>
    {
        public UserModel Translate(SqlDataReader reader, bool isList = false)
        {
            if (!isList)
            {
                if (!reader.HasRows)
                    return null;
                reader.Read();
            }
            var item = new UserModel();
            if (reader.IsColumnExists("Id"))
                item.Id = SqlHelper.GetNullableString(reader, "Id");

            if (reader.IsColumnExists("UserName"))
                item.UserName = SqlHelper.GetNullableString(reader, "UserName");

            if (reader.IsColumnExists("NormalizedUserName"))
                item.NormalizedUserName = SqlHelper.GetNullableString(reader, "NormalizedUserName");

            if (reader.IsColumnExists("IsActive"))
                item.IsActive = SqlHelper.GetBoolean(reader, "IsActive");

            return item;
        }

        public List<UserModel> TranslateAsList(SqlDataReader reader)
        {
            var list = new List<UserModel>();
            while (reader.Read())
            {
                list.Add(Translate(reader, true));
            }
            return list;
        }
    }

}
