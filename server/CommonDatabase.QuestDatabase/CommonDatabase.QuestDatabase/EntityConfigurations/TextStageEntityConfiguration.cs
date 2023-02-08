using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CommonDatabase.QuestDatabase.Models.Stages;

namespace CommonDatabase.QuestDatabase.EntityConfigurations
{
    internal class TextStageEntityConfiguration : StageEntityConfiguration<TextStageEntity>
    {
        public override void Configure(EntityTypeBuilder<TextStageEntity> builder)
        {
            base.Configure(builder);
            builder.ToTable("text_stage");
        }
    }
}
