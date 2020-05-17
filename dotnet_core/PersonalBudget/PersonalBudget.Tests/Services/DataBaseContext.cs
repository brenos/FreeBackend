using PersonalBudget.Models;
using Microsoft.EntityFrameworkCore;

namespace PersonalBudget.Tests.Services
{
    public class DataBaseContext
    {
        public static DbContextOptions<PersonalBudgetContext> GetOptionsContext(string databaseName)
        {
            return new DbContextOptionsBuilder<PersonalBudgetContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;
        }

        public static DbContextOptions<PersonalBudgetRplContext> GetOptionsContextRpl(string databaseName)
        {
            return new DbContextOptionsBuilder<PersonalBudgetRplContext>()
            .UseInMemoryDatabase(databaseName)
            .Options;
        }
        
        public static PersonalBudgetContext GetContext(string databaseName)
        {
            var optionsBuilder = new DbContextOptionsBuilder<PersonalBudgetContext>();
            optionsBuilder.UseInMemoryDatabase(databaseName);
            var context = new PersonalBudgetContext(optionsBuilder.Options);
            return context;
        }

        public static PersonalBudgetRplContext GetContextRpl(string databaseName)
        {
            var optionsBuilderRpl = new DbContextOptionsBuilder<PersonalBudgetRplContext>();
            optionsBuilderRpl.UseInMemoryDatabase(databaseName);
            var contextRpl = new PersonalBudgetRplContext(optionsBuilderRpl.Options);
            return contextRpl;
        }
    }
}