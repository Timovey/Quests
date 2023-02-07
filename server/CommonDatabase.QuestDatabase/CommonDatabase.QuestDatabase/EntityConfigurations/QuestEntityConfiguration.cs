using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GenerateQuestsService.DataContracts.Models;

namespace CommonDatabase.QuestDatabase.EntityConfigurations
{
    public class QuestEntityConfiguration : BaseEntityTypeConfiguration<Quest>
    {
        public override void Configure(EntityTypeBuilder<Quest> builder)
        {
            builder.ToTable("quest");
            base.Configure(builder);
        }
    }
}
