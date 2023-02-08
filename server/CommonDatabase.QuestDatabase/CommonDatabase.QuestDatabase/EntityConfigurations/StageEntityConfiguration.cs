﻿using CommonDatabase.QuestDatabase.Models.Stages;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CommonDatabase.QuestDatabase.EntityConfigurations
{
    internal abstract class StageEntityConfiguration<T> : BaseEntityTypeConfiguration<T> 
    where T : StageEntity
    {
        public override void Configure(EntityTypeBuilder<T> builder)
        {
            base.Configure(builder);
            builder.ToTable("stage");
        }
    }
}
