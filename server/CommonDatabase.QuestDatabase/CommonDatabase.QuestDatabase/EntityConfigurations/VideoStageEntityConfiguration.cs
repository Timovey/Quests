using GenerateQuestsService.DataContracts.Models.Stages;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CommonDatabase.QuestDatabase.EntityConfigurations
{
    public class VideoStageEntityConfiguration : IEntityTypeConfiguration<VideoStage>
    {
        public void Configure(EntityTypeBuilder<VideoStage> builder)
        {
            builder.ToTable("video_stage");
        }
    }
}
