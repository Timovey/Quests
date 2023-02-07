using GenerateQuestsService.DataContracts.Models.Stages;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CommonDatabase.QuestDatabase.EntityConfigurations
{
    public class VideoStageEntityConfiguration : BaseEntityTypeConfiguration<VideoStage>
    {
        public override void Configure(EntityTypeBuilder<VideoStage> builder)
        {
            builder.ToTable("video_stage");
            base.Configure(builder);
        }
    }
}
