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
    public class ProductSeed:IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasData(new Product
                {Id = 1, CategoryId = 1, Price = 100, Stock = 20, CreatedDate = DateTime.Now, Name = "Pencil 1"});
            builder.HasData(new Product
                { Id = 2, CategoryId = 2, Price = 200, Stock =30, CreatedDate = DateTime.Now, Name = "Book 1" });
            builder.HasData(new Product
                { Id = 3, CategoryId = 3, Price = 300, Stock = 40, CreatedDate = DateTime.Now, Name = "Rubber 1" });
        }
    }
}
