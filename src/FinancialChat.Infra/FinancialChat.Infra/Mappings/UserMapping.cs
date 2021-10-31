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
    public sealed class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Email).HasColumnType("varchar").HasMaxLength(255).IsRequired();
            builder.Property(x => x.UserName).HasColumnType("varchar").HasMaxLength(50).IsRequired();
            builder.HasMany(x => x.Messages).WithOne(e => e.Sender);
        }
    }
}
