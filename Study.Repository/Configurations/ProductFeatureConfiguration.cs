using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Study.Core;

namespace Study.Repository.Configurations
{
    internal class ProductFeatureConfiguration:IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasKey(x=>x.Id);
            builder.Property(x => x.Id).UseMySqlIdentityColumn();
            builder.HasOne(x => x.Product).WithOne(x => x.ProductFeature)
                .HasForeignKey<ProductFeature>(x => x.ProductId);
        }
    }
}
