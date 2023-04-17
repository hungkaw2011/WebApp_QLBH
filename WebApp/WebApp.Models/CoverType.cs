using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class CoverType
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(50)]
        [Display(Name="CoverType")]
        [Required]
        public string? Name { get; set; }
    }
}
