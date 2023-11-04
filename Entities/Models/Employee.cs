using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Employee
    {
        [Column("EmployeeId")]
        public Guid Id {  get; set; }
        [Required(ErrorMessage ="Employee name is a required field")]
        [MaxLength(30,ErrorMessage ="Employee name can't be longer tahn 30 words")]
        public string? Name { get; set; }
        [Required(ErrorMessage ="Age is required")]
        public int Age {  get; set; }
        [Required(ErrorMessage = "Position is required")]
        public string? Position { get; set; }
        [ForeignKey(nameof(Company))]
        public Guid CompanyId { get; set; }
        public Company? Company { get; set; }
    }
}
