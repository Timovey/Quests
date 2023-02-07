using GenerateQuestsService.DataContracts.Models.Stages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommonDatabase.QuestDatabase.EntityConfigurations
{
    public class CoordinatesEntityConfiguration : BaseEntityTypeConfiguration<Coordinates>
    {
        public override void Configure(EntityTypeBuilder<Coordinates> builder)
        {
            builder.ToTable("coordinates");
            base.Configure(builder);
        }
    }
}
