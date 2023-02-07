using GenerateQuestsService.DataContracts.Models.Stages;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CommonDatabase.QuestDatabase.EntityConfigurations
{
    public class TextStageEntityConfiguration : BaseEntityTypeConfiguration<TextStage>
    {
        public override void Configure(EntityTypeBuilder<TextStage> builder)
        {
            builder.ToTable("text_stage");
            base.Configure(builder);
        }
    }
}
