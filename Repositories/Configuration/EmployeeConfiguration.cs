using Entities.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Configuration
{
    public class EmployeeConfiguration: IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entityTypeConfiguration)
        {
            entityTypeConfiguration.HasData(new Employee
            {
                Id = new Guid("3d9d4c05-49b6-410c-bc78-2d54a9991870"),
                Name ="Sam Raiden",
                Age = 26,
                Position="Software Developer",
                CompanyId = new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            }, new Employee
            {
                Id = new Guid("3d8d4c05-49b6-410c-bc78-2d54a9991870"),
                Name = "Jana McLeaf",
                Age = 30,
                Position="Software Developer",
                CompanyId= new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870")
            },
            new Employee
            {
                Id = new Guid("3d7d4c05-49b6-410c-bc78-2d54a9991870"),
                Name = "Kane Miller",
                Age = 35,
                Position="Administrator",
                CompanyId = new Guid("c8d4c053-49b6-410c-bc78-2d54a9991870")
            });
        }
    }
}
