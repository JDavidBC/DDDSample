using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Source.Domains.Entities
{
    using BaseEntities;

    [Table("Caregivers")]
    public class Caregivers : Person
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long CaregiverId { get; set; }
        
        [Required(ErrorMessage = "Email required.")]
        [StringLength(100)]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Phone required.")]
        [StringLength(14)]
        [Display(Name = "Phone")]
        public string Phone { get; set; }
        
    }
}