using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GenerateQuestsService.DataContracts.Models.Stages;

namespace CommonDatabase.QuestDatabase.EntityConfigurations
{
    public class TestStageEntityConfiguration : IEntityTypeConfiguration<TestStage>
    {
        public void Configure(EntityTypeBuilder<TestStage> builder)
        {
            builder.ToTable("test_stage");
        }
    }
}
