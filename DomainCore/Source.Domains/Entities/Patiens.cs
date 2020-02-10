using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Source.Domains.BaseEntities;

namespace Source.Domains.Entities
{
    [Table("Patiens")]
    public class Patiens : Person
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