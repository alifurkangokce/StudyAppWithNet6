using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Study.Core;

namespace Study.Repository.Seeds
{
    internal class ProductFeatureSeed:IEntityTypeConfiguration<ProductFeature>
    {
        public void Configure(EntityTypeBuilder<ProductFeature> builder)
        {
            builder.HasData(new ProductFeature {Id = 1, Color = "red", Width = 100, Height = 200, ProductId = 1});
            builder.HasData(new ProductFeature {Id = 2, Color = "yellow", Width = 200, Height = 300, ProductId = 2});
        }
    }
}
