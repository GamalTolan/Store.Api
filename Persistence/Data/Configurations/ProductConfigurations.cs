﻿using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Data.Configurations
{
    public class ProductConfigurations: IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand).WithMany().HasForeignKey(p => p.BrandId);
            builder.HasOne(p => p.ProductType).WithMany().HasForeignKey(p => p.TypeId);
            //builder.Property(p => p.Name).IsRequired().HasMaxLength(100);
            //builder.Property(p => p.Description).IsRequired().HasMaxLength(500);
            //builder.Property(p => p.PictureUrl).IsRequired();
            //builder.Property(p => p.Price).HasColumnType("decimal(18,2)");
        }
    }
   
}
