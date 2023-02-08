using CommonDatabase.QuestDatabase.Models;
using CommonDatabase.QuestDatabase.Models.Stages;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace CommonDatabase.QuestDatabase
{
    public class QuestContext : DbContext
    {
        internal DbSet<QuestEntity> Quests { get; set; }
        internal DbSet<CoordinatesEntity> Coordinates { get; set; }
        internal DbSet<MapStageEntity> MapStages { get; set; }
        internal DbSet<QrCodeStageEntity> QrCodeStages { get; set; }
        internal DbSet<TestStageEntity> TestStages { get; set; }
        internal DbSet<TextStageEntity> TextStages { get; set; }
        internal DbSet<VideoStageEntity> VideoStages { get; set; }

        public QuestContext(DbContextOptions<QuestContext> options)
        : base(options)
        {
        }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
