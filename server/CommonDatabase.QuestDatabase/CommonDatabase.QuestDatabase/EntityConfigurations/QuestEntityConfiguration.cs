using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using GenerateQuestsService.DataContracts.Models;

namespace CommonDatabase.QuestDatabase.EntityConfigurations
{
    public class QuestEntityConfiguration : IEntityTypeConfiguration<Quest>
    {
        public void Configure(EntityTypeBuilder<Quest> builder)
        {
            builder.ToTable("quest");
        }
    }
}
