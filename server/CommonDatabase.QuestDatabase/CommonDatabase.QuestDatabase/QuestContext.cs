using GenerateQuestsService.DataContracts.Models;
using GenerateQuestsService.DataContracts.Models.Stages;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CommonDatabase.QuestDatabase
{
    public class QuestContext : DbContext
    {
        public DbSet<Quest> Quests { get; set; }
        public DbSet<Coordinates> Coordinates { get; set; }
        public DbSet<MapStage> MapStages { get; set; }
        public DbSet<QrCodeStage> QrCodeStages { get; set; }
        public DbSet<TestStage> TestStages { get; set; }
        public DbSet<TextStage> TextStages { get; set; }
        public DbSet<VideoStage> VideoStages { get; set; }
      
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
