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
    public class CategorySeed:IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(new Category{Id = 1,Name = "Pencils"});
            builder.HasData(new Category{Id = 2,Name = "Books"});
            builder.HasData(new Category{Id = 3,Name = "Rubbers"});
        }
    }
}
