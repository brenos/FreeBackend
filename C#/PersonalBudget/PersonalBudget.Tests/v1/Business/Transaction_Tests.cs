using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using NUnit.Framework;
using PersonalBudget.Business.v1;
using PersonalBudget.Business.v1.Objects;
using PersonalBudget.Models;
using PersonalBudget.Tests.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PersonalBudget.Tests.v1.Business
{
    [TestFixture]
    class Transaction_Tests
    {
        [TestCase("123", 20)]
        public async Task GetByUserId_Return_A_IEnumerable(string userId, int limit)
        {
            string databaseName = "GetByUserId_Return_A_IEnumerable";
            using (var context = DataBaseContext.GetContext(databaseName))
            {
                await SaveDefaultTransaction(context, userId:userId);
                ITransactionBO transactionBO = new TransactionBO(context, null);
                var result = await transactionBO.GetByUserId(userId, limit);
                var type = typeof(IEnumerable<Transaction>);
                Assert.IsInstanceOf(type, result);
            }
        }

        [TestCase("123", 1)]
        public async Task GetByUserId_Return_A_Limit(string userId, int limit)
        {
            string databaseName = "GetByUserId_Return_A_Limit";
            using (var context = DataBaseContext.GetContext(databaseName))
            {
                await SaveDefaultTransaction(context, userId:userId);
                ITransactionBO transactionBO = new TransactionBO(context, null);
                var result = await transactionBO.GetByUserId(userId, limit);
                int resultCount = result.Count();
                Assert.LessOrEqual(resultCount, limit, "Result is nos less o equal");
            }
        }

        [TestCase("123")]
        public async Task Create_New_Transaction(string userId)
        {
            string databaseName = "Create_New_Transaction";
            using (var context = DataBaseContext.GetContext(databaseName))
            {
                int result = await SaveDefaultTransaction(context, userId:userId);
                Assert.AreEqual(result, 1);
            }
        }

        [TestCase("123")]
        public async Task Update_Expense(string userId)
        {
            string databaseName = "Update_Expense";
            using (var context = DataBaseContext.GetContext(databaseName))
            {
                await SaveDefaultTransaction(context, userId);
                var t = context.Transaction.FirstOrDefault();
                string transactionId = t.Id;
                context.Entry(t).State = EntityState.Detached;
                ITransactionBO transactionBO = new TransactionBO(context, null);
                Transaction transaction = new Transaction
                {
                    Id = transactionId,
                    Description = "Teste",
                    DtTransaction = DateTime.Now,
                    YearRef = 2020,
                    MonthRef = 04,
                    Amount = 200,
                    CategorieId = "123",
                    UserId = userId,
                    TypeId = "123"
                };
                int result = await transactionBO.Update(transactionId, transaction);
                Assert.AreEqual(result, 1);
            }
        }

        [TestCase("123")]
        public async Task Delete_With_Id(string userId)
        {
            string databaseName = "Delete_With_Id";
            using (var context = DataBaseContext.GetContext(databaseName))
            {
                await SaveDefaultTransaction(context, userId);
                var t = context.Transaction.FirstOrDefault();
                string transactionId = t.Id;
                context.Entry(t).State = EntityState.Detached;
                ITransactionBO transactionBO = new TransactionBO(context, null);
                var result = await transactionBO.Delete(transactionId);
                Assert.AreEqual(result, 1);
            }
        }

        private async Task<int> SaveDefaultTransaction(PersonalBudgetContext context, string userId = null, Transaction transaction = null)
        {
            ITransactionBO transactionBO = new TransactionBO(context, null);
            int result = 0;
            if (transaction != null)
            {
                result = await transactionBO.Save(transaction);
            }
            else
            {
                result = await transactionBO.Save(new Transaction
                {
                    Description = "Teste",
                    DtTransaction = DateTime.Now,
                    YearRef = 2020,
                    MonthRef = 04,
                    Amount = 100,
                    CategorieId = "123",
                    UserId = userId,
                    TypeId = "123"
                });
            }
            return result;
        }
    }
}
