using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UrlUtility.API.Entities;

namespace UrlUtility.API.Repository.Sql.Configurations
{
    public class ShorUrlEntityConfiguration : IEntityTypeConfiguration<ShortUrl>
    {
        public void Configure(EntityTypeBuilder<ShortUrl> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.PageUrl)
                .HasMaxLength(2500)
                .IsRequired();

        }
    }
}
