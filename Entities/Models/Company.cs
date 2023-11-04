using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Models
{
    public class Company
    {
        [Column("CompanyId")]
        public Guid Id { get; set; }
        [Required(ErrorMessage ="Company name is required")]
        [MaxLength(60,ErrorMessage ="Can't be longer than 60")]
        public string? Name { get; set; }
        public string? Country { get; set; }
        public string? Address {  get; set; }
        public ICollection<Employee>? Employees { get; set; }
    }
}
