namespace UNiDAYS.Identity.Repositories
{
    public class SqlMethodLookup : IMethodLookup
    {
        public string StoredProcedure(string methodName)
        {
            switch (methodName)
            {
                case "CreateUser": return "usp_CreateUser";
                case "DeleteUser": return "usp_DeleteUser";
                case "FindUserById": return "usp_FindUserById";
                case "FindUserByName": return "usp_FindUserByName";
                case "UpdateUser": return "usp_UpdateUser";
                case "UpdatePasswordHash": return "usp_UpdatePasswordHash";
                case "GetPasswordHash": return "usp_GetPasswordHash";
            }
            return null;
        }
    }
}
