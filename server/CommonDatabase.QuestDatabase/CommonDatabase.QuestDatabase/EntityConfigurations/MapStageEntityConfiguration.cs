using GenerateQuestsService.DataContracts.Models.Stages;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CommonDatabase.QuestDatabase.EntityConfigurations
{
    public class MapStageEntityConfiguration : BaseEntityTypeConfiguration<MapStage>
    {
        public override void Configure(EntityTypeBuilder<MapStage> builder)
        {
            builder.ToTable("map_stage");
            base.Configure(builder);
        }
    }
}
