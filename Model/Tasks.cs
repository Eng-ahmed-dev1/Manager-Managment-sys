using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ManagerManagmentsys.Model
{
    public class Tasks
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        [StringLength(250)]
        public string? Title { get; set; }
        [MaxLength]
        public string? Description { get; set; }
        [Required]
        [StringLength(250)]
        public string? Status { get; set; }
        public DateTime DueDate { get; set; }
        public int Userid { get; set; }
        [ForeignKey("Userid")]
        public User? User { get; set; }
    }

}
