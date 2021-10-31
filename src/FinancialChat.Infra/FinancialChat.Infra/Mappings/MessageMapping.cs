using FinancialChat.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinancialChat.Infra.Data.Mappings
{
    public sealed class MessageMapping : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Message","dbo");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnType("uniqueidentifier").IsRequired();
            builder.Property(x => x.Text).HasColumnType("varchar").HasMaxLength(1000).IsRequired();
            builder.Property(x => x.Created).HasColumnType("datetime").IsRequired();
            builder.HasOne(x => x.Sender).WithMany(c => c.Messages);
        }
    }
}
