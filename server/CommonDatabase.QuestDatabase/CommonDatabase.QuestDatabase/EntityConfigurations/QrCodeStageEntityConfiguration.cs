using GenerateQuestsService.DataContracts.Models.Stages;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;

namespace CommonDatabase.QuestDatabase.EntityConfigurations
{
    public class QrCodeStageEntityConfiguration : IEntityTypeConfiguration<QrCodeStage>
    {
        public void Configure(EntityTypeBuilder<QrCodeStage> builder)
        {
            builder.ToTable("qrcode_stage");
        }
    }
}
