using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PersonalBudget.Business.v1;
using PersonalBudget.Business.v1.Objects;
using PersonalBudget.Models;
using PersonalBudget.Tests.Services;

namespace PersonalBudget.Tests.v1.Business
{
    [TestFixture]
    class TransactionType_Tests
    {
        [TestCase]
        public async Task GetAll_Return_IEnumerable()
        {
            string databaseName = "GetAll_Return_A_IEnumerable";
            using (var context = DataBaseContext.GetContext(databaseName))
            {
                await SaveDefaultTransactionType(context);
                ITransactionTypeBO transactionTypeBO = new TransactionTypeBO(context, null);
                var result = await transactionTypeBO.GetAll();
                var type = typeof(IEnumerable<TransactionType>);
                Assert.IsInstanceOf(type, result);
            }
        }

        [TestCase]
        public async Task GetById_Retunr_TransactionType()
        {
            string databaseName = "GetById_Retunr_TransactionType";
            using (var context = DataBaseContext.GetContext(databaseName))
            {
                await SaveDefaultTransactionType(context);
                ITransactionTypeBO transactionTypeBO = new TransactionTypeBO(context, null);
                string typeId = context.TransactionType.FirstOrDefault().Id;
                var result = await transactionTypeBO.GetById(typeId);
                var type = typeof(TransactionType);
                Assert.IsInstanceOf(type, result);
            }
        }

        [TestCase("Test Create")]
        public async Task Create_New_TransactionType(string name)
        {
            string databaseName = "Create_New_Type";
            using (var context = DataBaseContext.GetContext(databaseName))
            {
                int result = await SaveDefaultTransactionType(context, name);
                Assert.AreEqual(result, 1);
            }
        }

        [TestCase("Tests Update")]
        public async Task Update_TransactionType(string name)
        {
            string databaseName = "Update_TransactionType";
            using (var context = DataBaseContext.GetContext(databaseName))
            {
                await SaveDefaultTransactionType(context);
                var tType = context.TransactionType.FirstOrDefault();
                string typeId = tType.Id;
                context.Entry(tType).State = EntityState.Detached;
                ITransactionTypeBO transactionTypeBO = new TransactionTypeBO(context, null);
                TransactionType transactionType = new TransactionType
                {
                    Id = typeId,
                    Name = name
                };
                int result = await transactionTypeBO.Update(typeId, transactionType);
                Assert.AreEqual(result, 1);
            }
        }

        [TestCase]
        public async Task Delete()
        {
            string databaseName = "Delete";
            using (var context = DataBaseContext.GetContext(databaseName))
            {
                await SaveDefaultTransactionType(context);
                var tType = context.TransactionType.FirstOrDefault();
                string typeId = tType.Id;
                context.Entry(tType).State = EntityState.Detached;
                ITransactionTypeBO transactionTypeBO = new TransactionTypeBO(context, null);
                var result = await transactionTypeBO.Delete(typeId);
                Assert.AreEqual(result, 1);
            }
        }

        private async Task<int> SaveDefaultTransactionType(PersonalBudgetContext context, string name = "Test")
        {
            ITransactionTypeBO transactionTypeBO = new TransactionTypeBO(context, null);
            var result = await transactionTypeBO.Save(new TransactionType
            {
                Name = name
            });
            return result;
        }
    }
}
