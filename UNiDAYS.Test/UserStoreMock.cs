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
    public class UserStoreMock
    {
        Mock<IRepository<UserModel>> repository;
        IUserStore<UserModel> userStore;
        IUserStore<UserModel> userStoreMethodLookup;
        CancellationTokenSource cancellationTokenSource;

        [TestInitialize]
        public void Init()
        {
            repository = new Mock<IRepository<UserModel>>();
            userStore = new UserStore(repository.Object);
            userStoreMethodLookup = new UserStore(new TestRepository(new SqlMethodLookup()));
            cancellationTokenSource = new CancellationTokenSource();
        }

        [TestMethod]
        public void CreateAsync()
        {
            var userModel = new UserModel()
            {
                Id = Guid.NewGuid().ToString(),
                UserName = It.IsAny<string>(),
                NormalizedUserName = It.IsAny<string>(),
                PasswordHash = It.IsAny<string>(),
                IsActive = true,
            };
            var returnModel = new UserModel();
            repository.Setup(x => x.GetData(It.IsAny<string>(), It.IsAny<CancellationToken>(), It.IsAny<(string, object)[]>())).ReturnsAsync(returnModel).Verifiable();
            var task = userStore.CreateAsync(userModel, cancellationTokenSource.Token);
            task.Wait();
            Assert.IsTrue(task.Result.Succeeded);
            repository.Verify();
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void DeleteAsync()
        {
            var userModel = new UserModel()
            {
                Id = Guid.NewGuid().ToString()
            };
            repository.Setup(x => x.GetString(It.IsAny<string>(), It.IsAny<CancellationToken>(), It.IsAny<(string, object)[]>())).ReturnsAsync("Success").Verifiable();
            var task = userStore.DeleteAsync(userModel, cancellationTokenSource.Token);
            task.Wait();
            Assert.IsTrue(task.Result.Succeeded);
            repository.Verify();
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void FindByIdAsync()
        {
            var returnModel = new UserModel();
            repository.Setup(x => x.GetData(It.IsAny<string>(), It.IsAny<CancellationToken>(), It.IsAny<(string, object)[]>())).ReturnsAsync(returnModel).Verifiable();
            var task = userStore.FindByIdAsync(It.IsAny<string>(), cancellationTokenSource.Token);
            task.Wait();
            Assert.AreSame(task.Result, returnModel);
            repository.Verify();
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void FindByNameAsync()
        {
            var returnModel = new UserModel();
            repository.Setup(x => x.GetData(It.IsAny<string>(), It.IsAny<CancellationToken>(), It.IsAny<(string, object)[]>())).ReturnsAsync(returnModel).Verifiable();
            var task = userStore.FindByNameAsync(It.IsAny<string>(), cancellationTokenSource.Token);
            task.Wait();
            Assert.AreSame(task.Result, returnModel);
            repository.Verify();
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GetNormalizedUserNameAsync()
        {
            var userModel = new UserModel()
            {
                NormalizedUserName = "test"
            };
            var task = userStore.GetNormalizedUserNameAsync(userModel, cancellationTokenSource.Token);
            task.Wait();
            Assert.AreEqual(task.Result, "test");
            repository.Verify();
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GetUserIdAsync()
        {
            var userId = Guid.NewGuid().ToString();
            var userModel = new UserModel()
            {
                Id = userId
            };
            var task = userStore.GetUserIdAsync(userModel, cancellationTokenSource.Token);
            task.Wait();
            Assert.AreEqual(task.Result, userId);
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void GetUserNameAsync()
        {
            var userModel = new UserModel()
            {
                UserName = "test"
            };
            var task = userStore.GetUserNameAsync(userModel, cancellationTokenSource.Token);
            task.Wait();
            Assert.AreEqual(task.Result, "test");
            repository.Verify();
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SetNormalizedUserNameAsync()
        {
            var userModel = new UserModel();
            var task = userStore.SetNormalizedUserNameAsync(userModel, "test", cancellationTokenSource.Token);
            task.Wait();
            Assert.AreEqual(userModel.NormalizedUserName, "test");
            repository.Verify();
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void SetUserNameAsync()
        {
            var userModel = new UserModel();
            var task = userStore.SetUserNameAsync(userModel, "test", cancellationTokenSource.Token);
            task.Wait();
            Assert.AreEqual(userModel.UserName, "test");
            repository.Verify();
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void UpdateAsync()
        {
            var userModel = new UserModel()
            {
                Id = Guid.NewGuid().ToString()
            };
            repository.Setup(x => x.GetString(It.IsAny<string>(), It.IsAny<CancellationToken>(), It.IsAny<(string, object)[]>())).ReturnsAsync("Success").Verifiable();
            var task = userStore.UpdateAsync(userModel, cancellationTokenSource.Token);
            task.Wait();
            Assert.IsTrue(task.Result.Succeeded);
            repository.Verify();
            repository.VerifyNoOtherCalls();
        }

        [TestMethod]
        public void CreateAsyncStoredProcExists()
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
                userStoreMethodLookup.CreateAsync(userModel, cancellationTokenSource.Token).Wait();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void DeleteAsyncStoredProcExists()
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
                userStoreMethodLookup.DeleteAsync(userModel, cancellationTokenSource.Token).Wait();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void FindByIdAsyncStoredProcExists()
        {
            try
            {
                userStoreMethodLookup.FindByIdAsync(Guid.NewGuid().ToString(), cancellationTokenSource.Token).Wait();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void FindByNameAsyncStoredProcExists()
        {
            try
            {
                userStoreMethodLookup.FindByNameAsync(It.IsAny<string>(), cancellationTokenSource.Token).Wait();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetNormalizedUserNameAsyncStoredProcExists()
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
                userStoreMethodLookup.GetNormalizedUserNameAsync(userModel, cancellationTokenSource.Token).Wait();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetUserIdAsyncStoredProcExists()
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
                userStoreMethodLookup.GetUserIdAsync(userModel, cancellationTokenSource.Token).Wait();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void GetUserNameAsyncStoredProcExists()
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
                userStoreMethodLookup.GetUserNameAsync(userModel, cancellationTokenSource.Token).Wait();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void SetNormalizedUserNameAsyncStoredProcExists()
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
                userStoreMethodLookup.SetNormalizedUserNameAsync(userModel, It.IsAny<string>(), cancellationTokenSource.Token).Wait();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void SetUserNameAsyncStoredProcExists()
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
                userStoreMethodLookup.SetUserNameAsync(userModel, It.IsAny<string>(), cancellationTokenSource.Token).Wait();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void UpdateAsyncStoredProcExists()
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
                userStoreMethodLookup.UpdateAsync(userModel, cancellationTokenSource.Token).Wait();
            }
            catch
            {
                Assert.Fail();
            }
        }

    }
}
