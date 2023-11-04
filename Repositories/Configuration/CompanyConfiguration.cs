﻿using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Configuration
{
    public class CompanyConfiguration: IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasData(
                new Company
                {
                    Id = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"),
                    Name = "IT_Solutions Ltd",
                    Address = "632 Wall dr.",
                    Country = "USA"
                },
                new Company
                {
                    Id = new Guid("c8d4c053-49b6-410c-bc78-2d54a9991870"),
                    Name = "Admin_solutions Ltd",
                    Address = "312 Forest",
                    Country="USA"
                }
                );
        }
    }
}
