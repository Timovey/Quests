using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonDatabase.QuestDatabase.EntityConfigurations
{
    /// <summary>
    /// Базовая сущность конфигурации
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class BaseEntityTypeConfiguration<T> : IEntityTypeConfiguration<T>
    where T : class
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.Property<bool>("IsDeleted")
                .IsRequired()
                .HasDefaultValue(false);
            //builder.Ignore(p => p.)
        }
    }
}
