using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PersonalBudget.Business.v1;
using PersonalBudget.Business.v1.Objects;
using PersonalBudget.Models;
using PersonalBudget.Tests.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonalBudget.Tests.v1.Business
{
    [TestFixture]
    class Release_Tests
    {
        [TestCase("123")]
        public async Task GetByTransactionId_Return_A_IEnumerable(string transactionId)
        {
            string databaseName = "GetByTransactionId_Return_A_IEnumerable";
            using (var context = DataBaseContext.GetContext(databaseName))
            {
                await SaveDefaultRelease(context, transactionId);
                IReleaseBO releaseBO = new ReleaseBO(context, null);
                var result = await releaseBO.GetByTransactionId(transactionId);
                var type = typeof(IEnumerable<Release>);
                Assert.IsInstanceOf(type, result);
            }
        }

        [TestCase(05, 2020, 10, "123")]
        public async Task Create_New_Release(int monthRef, int yearRef, decimal amount, string transactionId)
        {
            string databaseName = "Create_New_Release";
            using (var context = DataBaseContext.GetContext(databaseName))
            {
                Release release = new Release
                {
                    MonthRef = monthRef,
                    YearRef = yearRef,
                    Amount = amount,
                    TransactionId = transactionId
                };
                int result = await SaveDefaultRelease(context,release:release);
                Assert.AreEqual(result, 1);
            }
        }

        [TestCase(05, 2020, 10, "123")]
        public async Task Update_Release(int monthRef, int yearRef, decimal amount, string transactionId)
        {
            string databaseName = "Update_Release";
            using (var context = DataBaseContext.GetContext(databaseName))
            {
                await SaveDefaultRelease(context, transactionId:transactionId);
                var releaseTest = context.Release.FirstOrDefault();
                string releaseId = releaseTest.Id;
                context.Entry(releaseTest).State = EntityState.Detached;
                IReleaseBO releaseBO = new ReleaseBO(context, null);
                Release release = new Release
                {
                    Id = releaseId,
                    MonthRef = monthRef,
                    YearRef = yearRef,
                    Amount = amount,
                    TransactionId = transactionId
                };
                int result = await releaseBO.Update(releaseId, release);
                Assert.AreEqual(result, 1);
            }
        }

        [TestCase("123")]
        public async Task Delete_With_Id(string transactionId)
        {
            string databaseName = "Delete_With_Id";
            using (var context = DataBaseContext.GetContext(databaseName))
            {
                await SaveDefaultRelease(context, transactionId);
                var releaseTest = context.Release.FirstOrDefault();
                string releaseId = releaseTest.Id;
                context.Entry(releaseTest).State = EntityState.Detached;
                IReleaseBO releaseBO = new ReleaseBO(context, null);
                var result = await releaseBO.Delete(releaseId);
                Assert.AreEqual(result, 1);
            }
        }

        private async Task<int> SaveDefaultRelease(PersonalBudgetContext context, string transactionId = null, Release release = null)
        {
            IReleaseBO releaseBO = new ReleaseBO(context, null);
            int result = 0;
            if (release != null)
            {
                result = await releaseBO.Save(release);
            }
            else
            {
                result = await releaseBO.Save(new Release
                {
                    MonthRef = 1,
                    YearRef = 2020,
                    Amount = 10,
                    TransactionId = transactionId
                });
            }
            return result;
        }
    }
}
