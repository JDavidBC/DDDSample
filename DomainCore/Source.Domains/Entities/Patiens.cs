using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Source.Domains.Entities
{
    [Table("Patiens")]
    public class Patiens
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PatienId { get; set; }
        
        [Required(ErrorMessage = "Organization required.")]
        [Display(Name = "Organization")]
        public long OrganizationId { get; set; }

        [Display(Name = "Last Login")]
        [DataType(DataType.Date)]
        public DateTime? LastLogin { get; set; }
    }
}