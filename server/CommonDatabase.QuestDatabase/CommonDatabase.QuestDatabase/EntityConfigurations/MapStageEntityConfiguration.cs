using GenerateQuestsService.DataContracts.Models.Stages;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CommonDatabase.QuestDatabase.EntityConfigurations
{
    public class MapStageEntityConfiguration : IEntityTypeConfiguration<MapStage>
    {
        public void Configure(EntityTypeBuilder<MapStage> builder)
        {
            builder.ToTable("map_stage");
        }
    }
}
