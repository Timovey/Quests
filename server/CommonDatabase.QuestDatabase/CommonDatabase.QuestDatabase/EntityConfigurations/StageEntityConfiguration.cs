
using GenerateQuestsService.DataContracts.Models.Stages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommonDatabase.QuestDatabase.EntityConfigurations
{
    public class StageEntityConfiguration : BaseEntityTypeConfiguration<Stage>
    {
        public override void Configure(EntityTypeBuilder<Stage> builder)
        {
            builder.ToTable("stage");
            base.Configure(builder);
        }
    }
}
