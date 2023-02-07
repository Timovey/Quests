using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GenerateQuestsService.DataContracts.Models.Stages;

namespace CommonDatabase.QuestDatabase.EntityConfigurations
{
    public class TestStageEntityConfiguration : BaseEntityTypeConfiguration<TestStage>
    {
        public override void Configure(EntityTypeBuilder<TestStage> builder)
        {
            builder.ToTable("test_stage");
            base.Configure(builder);
        }
    }
}
