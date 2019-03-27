using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Threading;
using System.Threading.Tasks;
using UNiDAYS.Identity.Models;
using UNiDAYS.Identity.Repositories;
using UNiDAYS.Identity.Stores;

namespace UNiDAYS.Test
{
    [TestClass]
    public class UserPasswordStoreMock
    {
        Mock<IRepository<UserModel>> repository;
        IUserPasswordStore<UserModel> userStore;
        IUserPasswordStore<UserModel> userStoreMethodLookup;
        private CancellationTokenSource cancellationTokenSource;

        [TestInitialize]
        public void Init()
        {
            repository = new Mock<IRepository<UserModel>>();
            userStore = new UserStore(repository.Object);
            userStoreMethodLookup = new UserStore(new TestRepository(new SqlMethodLookup()));
            cancellationTokenSource = new CancellationTokenSource();
        }

        [TestMethod]
        public void GetPasswordHashAsync()
        {
            var user = new UserModel()
            {
                Id = Guid.NewGuid().ToString()
            };
            repository.Setup(x => x.GetString(It.IsAny<string>(), It.IsAny<CancellationToken>(), It.IsAny<(string, object)[]>())).ReturnsAsync("test").Verifiable();
            var task = userStore.GetPasswordHashAsync(user, cancellationTokenSource.Token);
            task.Wait();
            Assert.AreEqual(task.Result, "test");
            repository.Verify();
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void HasPasswordAsync()
        {
            var user = new UserModel()
            {
                Id = Guid.NewGuid().ToString()
            };
            repository.Setup(x => x.GetString(It.IsAny<string>(), It.IsAny<CancellationToken>(), It.IsAny<(string, object)[]>())).ReturnsAsync("test").Verifiable();
            var task = userStore.HasPasswordAsync(user, cancellationTokenSource.Token);
            task.Wait();
            Assert.IsTrue(task.Result);
            repository.Verify();
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SetPasswordHashAsync()
        {
            var user = new UserModel();
            var task = userStore.SetPasswordHashAsync(user, "password", cancellationTokenSource.Token);
            task.Wait();
            Assert.AreEqual(user.PasswordHash, "password");
            repository.Verify();
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GetPasswordHashAsyncProcExists()
        {
            try
            {
                var userModel = new UserModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = It.IsAny<string>(),
                    NormalizedUserName = It.IsAny<string>(),
                    PasswordHash = It.IsAny<string>(),
                    IsActive = true,
                };
                userStoreMethodLookup.GetPasswordHashAsync(userModel, cancellationTokenSource.Token).Wait();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void HasPasswordAsyncStoredProcExists()
        {
            try
            {
                var userModel = new UserModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = It.IsAny<string>(),
                    NormalizedUserName = It.IsAny<string>(),
                    PasswordHash = It.IsAny<string>(),
                    IsActive = true,
                };
                userStoreMethodLookup.HasPasswordAsync(userModel, cancellationTokenSource.Token).Wait();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void SetPasswordHashAsyncStoredProcExists()
        {
            try
            {
                var userModel = new UserModel()
                {
                    Id = Guid.NewGuid().ToString(),
                    UserName = It.IsAny<string>(),
                    NormalizedUserName = It.IsAny<string>(),
                    PasswordHash = It.IsAny<string>(),
                    IsActive = true,
                };
                userStoreMethodLookup.SetPasswordHashAsync(userModel, "password", cancellationTokenSource.Token).Wait();
            }
            catch
            {
                Assert.Fail();
            }
        }
    }
}
