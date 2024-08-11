using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProductManagement.DAL.Models
{
    public class ProductAudit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        [Required]
        public long UserId { get; set; }

        [Required]
        public string LogMessage { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
    }
}