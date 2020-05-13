using System.Linq;
using System.Collections.Generic;
using NUnit.Framework;
using PersonalBudget.Business.v1;
using PersonalBudget.Business.v1.Objects;
using PersonalBudget.Models;
using System.Threading.Tasks;
using PersonalBudget.Tests.Services;
using Microsoft.EntityFrameworkCore;

namespace PersonalBudget.Tests.v1.Business
{
    [TestFixture]
    public class Categorie_Tests
    {
        [TestCase("123")]
        public async Task GetByUserId_Return_A_IEnumerable(string userId)
        {
            string databaseName = "GetByUserId_Return_A_IEnumerable";
            using (var context = DataBaseContext.GetContext(databaseName))
            {
                await SaveDefaultCategories(context, userId);
                ICategorieBO categorieBO = new CategorieBO(context, null);
                var result = await categorieBO.GetByUserId(userId);
                var type = typeof(IEnumerable<Categorie>);
                Assert.IsInstanceOf(type, result);
            }
        }

        [TestCase("Tests", "123")]
        public async Task Create_New_Categorie(string name, string userId)
        {
            string databaseName = "Create_New_Categorie";
            using (var context = DataBaseContext.GetContext(databaseName))
            {
                int result = await SaveDefaultCategories(context, userId, name);
                Assert.AreEqual(result, 1);
            }
        }

        [TestCase("Tests Update", "123")]
        public async Task Update_Categorie(string name, string userId)
        {
            string databaseName = "Update_Categorie";
            using (var context = DataBaseContext.GetContext(databaseName))
            {
                await SaveDefaultCategories(context, userId);
                var categorieTest = context.Categorie.FirstOrDefault();
                string categorieId = categorieTest.Id;
                context.Entry(categorieTest).State = EntityState.Detached;
                ICategorieBO categorieBO = new CategorieBO(context, null);
                Categorie categorie = new Categorie
                {
                    Id = categorieId,
                    Name = name,
                    UserId = userId
                };
                int result = await categorieBO.Update(categorieId, categorie);
                Assert.AreEqual(result, 1);
            }
        }
        
        [TestCase("123")]
        public async Task Delete_With_Id(string userId)
        {
            string databaseName = "Delete_With_Id";
            using (var context = DataBaseContext.GetContext(databaseName))
            {
                await SaveDefaultCategories(context, userId);
                var categorieTest = context.Categorie.FirstOrDefault();
                string categorieId = categorieTest.Id;
                context.Entry(categorieTest).State = EntityState.Detached;
                ICategorieBO categorieBO = new CategorieBO(context, null);
                var result = await categorieBO.Delete(categorieId);
                Assert.AreEqual(result, 1);
            }
        }

        private async Task<int> SaveDefaultCategories(PersonalBudgetContext context, string userId, string name = "Test")
        {
            ICategorieBO categorieBO = new CategorieBO(context, null);
            var result = await categorieBO.Save(new Categorie 
            {
                Name = name,
                UserId = userId
            });
            return result;
        }
    }
}