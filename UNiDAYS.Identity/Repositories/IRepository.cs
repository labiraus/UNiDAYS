using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace UNiDAYS.Identity.Repositories
{
    public interface IRepository<TData> where TData : class
    {

        Task Execute(string methodName, CancellationToken token, params (string name, object value)[] paramters);

        Task<string> GetString(string methodName, CancellationToken token, params (string name, object value)[] paramters);

        Task<TData> GetData(string methodName, CancellationToken token, params (string name, object value)[] paramters);

        Task<List<TData>> GetDataList(string methodName, CancellationToken token, params (string name, object value)[] paramters);
    }
}
