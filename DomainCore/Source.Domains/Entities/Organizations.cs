using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Source.Domains.ValueObjects;

namespace Source.Domains.Entities
{
    [Table("Organizations")]
    public class Organizations : Auditory
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int OrganizationId { get; set; }
        
        [Required(ErrorMessage = "Cif Required.")]
        [StringLength(25)]
        [Display(Name = "Cif")]
        [DataType(DataType.Text)]
        public string Cif { get; set; }
        
        [Required(ErrorMessage = "Bussines Name Required.")]
        [StringLength(25)]
        [Display(Name = "Bussines Name")]
        [DataType(DataType.Text)]
        public string BussinesName { get; set; }
        
        [Required(ErrorMessage = "Collective Required.")]
        [StringLength(50)]
        [Display(Name = "Collective")]
        [DataType(DataType.Text)]
        public string Collective { get; set; }
        
        
        [StringLength(100)]
        [Display(Name = "Name Contact")]
        [DataType(DataType.Text)]
        public string NameContact { get; set; }
        
        [Required(ErrorMessage = "First Surname contact Required.")]
        [StringLength(100)]
        [Display(Name = "First Surname Contact")]
        [DataType(DataType.Text)]
        public string FirstSurnameContact { get; set; }
        
        
        [StringLength(100)]
        [Display(Name = "Second Surname Contact")]
        [DataType(DataType.Text)]
        public string SecondSurnameContact { get; set; }
        
        [Required(ErrorMessage = "Email contact required.")]
        [StringLength(150)]
        [Display(Name = "Email contact")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        
        [Required(ErrorMessage = "Phone contact required.")]
        [StringLength(15)]
        [Display(Name = "Phone contact")]
        [DataType(DataType.Text)]
        public string PhoneContact { get; set; }
        
        [Required(ErrorMessage = "Position contact required.")]
        [StringLength(50)]
        [Display(Name = "Position contact")]
        [DataType(DataType.Text)]
        public string PositionContact { get; set; }
        
        [Required(ErrorMessage = "Entry date required.")]
        [Display(Name = "Entry Date")]
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; } = DateTime.Now;

        [Display(Name = "Leaving Date")]
        [DataType(DataType.Date)]
        public DateTime? LeavingDate { get; set; }

        [Display(Name = "Active")]
        public bool Active { get; set; } = true;

        
        
        
    }
}