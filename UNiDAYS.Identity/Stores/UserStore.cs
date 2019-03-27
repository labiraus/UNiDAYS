using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using UNiDAYS.Identity.Repositories;
using UNiDAYS.Identity.Models;

namespace UNiDAYS.Identity.Stores
{
    public class UserStore : IUserStore<UserModel>, IUserPasswordStore<UserModel>
    {
        public IRepository<UserModel> _repository { get; }

        public UserStore(IRepository<UserModel> Repository)
        {
            _repository = Repository;
        }

        public async Task<IdentityResult> CreateAsync(UserModel user, CancellationToken cancellationToken)
        {
            var parameters = new (string, object)[] {
                ("Id", user.Id),
                ("UserName", user.UserName),
                ("NormalizedUserName", user.NormalizedUserName),
                ("PasswordHash", user.PasswordHash),
                ("IsActive", user.IsActive)
            };
            var newUser = await _repository.GetData("CreateUser", cancellationToken, parameters);
            if (newUser != null)
                return IdentityResult.Success;
            return IdentityResult.Failed(new IdentityError() { Code = "SS001", Description = "Sql Store create user failed" });
        }

        public async Task<IdentityResult> DeleteAsync(UserModel user, CancellationToken cancellationToken)
        {
            var parameters = new (string, object)[] {
                ("Id", user.Id)
            };
            var newUser = await _repository.GetString("DeleteUser", cancellationToken, parameters);
            if (newUser == "Success")
                return IdentityResult.Success;
            return IdentityResult.Failed(new IdentityError() { Code = "SS002", Description = "Sql Store delete user failed" });
        }

        public async Task<UserModel> FindByIdAsync(string userId, CancellationToken cancellationToken)
        {
            var parameters = new (string, object)[] {
                ("Id", userId)
            };
            var newUser = await _repository.GetData("FindUserById", cancellationToken, parameters);
            return newUser;
        }

        public async Task<UserModel> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
        {
            var parameters = new (string, object)[] {
                ("NormalizedUserName", normalizedUserName)
            };
            var newUser = await _repository.GetData("FindUserByName", cancellationToken, parameters);
            return newUser;
        }

        public Task<string> GetNormalizedUserNameAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user?.NormalizedUserName);
        }

        public Task<string> GetUserIdAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user?.Id);
        }

        public Task<string> GetUserNameAsync(UserModel user, CancellationToken cancellationToken)
        {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(UserModel user, string normalizedName, CancellationToken cancellationToken)
        {
            user.NormalizedUserName = normalizedName;
            return Task.CompletedTask;
        }

        public Task SetUserNameAsync(UserModel user, string userName, CancellationToken cancellationToken)
        {
            user.UserName = userName;
            return Task.CompletedTask;
        }

        public async Task<IdentityResult> UpdateAsync(UserModel user, CancellationToken cancellationToken)
        {
            var parameters = new (string, object)[] {
                ("Id", user.Id),
                ("UserName", user.UserName),
                ("NormalizedUserName", user.NormalizedUserName),
                ("PasswordHash", user.PasswordHash),
                ("IsActive", user.IsActive)
            };
            var response = await _repository.GetString("UpdateUser", cancellationToken, parameters);
            if (response == "Success")
                return IdentityResult.Success;
            return IdentityResult.Failed(new IdentityError() { Description = response , Code = "USS10004"});
        }

        public Task SetPasswordHashAsync(UserModel user, string passwordHash, CancellationToken cancellationToken)
        {
            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public async Task<string> GetPasswordHashAsync(UserModel user, CancellationToken cancellationToken)
        {
            var parameters = new (string, object)[] {
                ("Id", user.Id)
            };
            return await _repository.GetString("GetPasswordHash", cancellationToken, parameters);
        }

        public async Task<bool> HasPasswordAsync(UserModel user, CancellationToken cancellationToken)
        {
            var parameters = new (string, object)[] {
                ("Id", user.Id)
            };
            return !string.IsNullOrWhiteSpace(await _repository.GetString("GetPasswordHash", cancellationToken, parameters));
        }

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                // TODO: set large fields to null.

                disposedValue = true;
            }
        }

        // TODO: override a finalizer only if Dispose(bool disposing) above has code to free unmanaged resources.
        // ~UserStore() {
        //   // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
        //   Dispose(false);
        // }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
            // TODO: uncomment the following line if the finalizer is overridden above.
            // GC.SuppressFinalize(this);
        }
        #endregion
    }
}
