using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CommonDatabase.QuestDatabase
{
    public class QuestContext : DbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
