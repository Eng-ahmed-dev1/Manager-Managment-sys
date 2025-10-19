using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerManagmentsys.Model
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        [Required]
        [StringLength(250)]
        public string ?Name { get; set; }
        [Required]
        [StringLength(250)]
        public string? Email { get; set; }
        [Required]
        [StringLength(250)]
        public string? Password { get; set; }
        [Required]
        [StringLength(250)]
        public string ?Role {  get; set; }
        public ICollection<Tasks> tasks { get; set; }  = new List<Tasks>();

    }
}
