using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UNiDAYS.Identity.Models;
using UNiDAYS.Identity.Repositories;

namespace UNiDAYS.Test
{
    class TestRepository : IRepository<UserModel>
    {
        private readonly IMethodLookup _methodLookup;

        public TestRepository(IMethodLookup methodLookup)
        {
            _methodLookup = methodLookup;
        }
        public Task Execute(string methodName, CancellationToken token, params (string name, object value)[] paramters)
        {
            if (string.IsNullOrWhiteSpace(_methodLookup.StoredProcedure(methodName)))
                throw new Exception();
            return Task.CompletedTask;
        }

        public Task<UserModel> GetData(string methodName, CancellationToken token, params (string name, object value)[] paramters)
        {
            if (string.IsNullOrWhiteSpace(_methodLookup.StoredProcedure(methodName)))
                throw new Exception();
            return Task.FromResult(new UserModel());
        }

        public Task<List<UserModel>> GetDataList(string methodName, CancellationToken token, params (string name, object value)[] paramters)
        {
            if (string.IsNullOrWhiteSpace(_methodLookup.StoredProcedure(methodName)))
                throw new Exception();
            return Task.FromResult(new List<UserModel>());
        }

        public Task<string> GetString(string methodName, CancellationToken token, params (string name, object value)[] paramters)
        {
            if (string.IsNullOrWhiteSpace(_methodLookup.StoredProcedure(methodName)))
                throw new Exception();
            return Task.FromResult("Success");
        }
    }
}
