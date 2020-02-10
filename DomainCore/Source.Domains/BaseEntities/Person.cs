using System;
using System.ComponentModel.DataAnnotations;

namespace Source.Domains.BaseEntities
{
    public class Person
    {
    
        [Required(ErrorMessage = "Nif Required.")]
        [StringLength(25)]
        [Display(Name = "Nif")]
        [DataType(DataType.Text)]
        public string Nif { get; set; }

        [Required(ErrorMessage = "BirthDate required.")]
        [Display(Name = "Birth Date")]
        [DataType(DataType.Date)]
        public DateTime BirthDate { get; set; }


        [Required(ErrorMessage = "Name required.")]
        [StringLength(50)]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required(ErrorMessage = "FirstSurname required.")]
        [StringLength(50)]
        [Display(Name = "First Surname")]
        [DataType(DataType.Text)]
        public string FirstSurname { get; set; }
        
        [StringLength(50)]
        [Display(Name = "Second Surname")]
        [DataType(DataType.Text)]
        public string SecondSurname { get; set; }
        
        [StringLength(30)]
        [Display(Name = "Alias")]
        [DataType(DataType.Text)]
        public string Alias { get; set; }
        
        [Required(ErrorMessage = "Password required.")]
        [StringLength(30)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }
        
        [StringLength(50)]
        [Display(Name = "Photo Name")]
        [DataType(DataType.Text)]
        public string PhotoName { get; set; }
        
        [StringLength(50)]
        [Display(Name = "Photo Type")]
        [DataType(DataType.Text)]
        public string PhotoType { get; set; }

        [Display(Name = "Photo")]
        public byte[] Photo { get; set; }
        
        [Required(ErrorMessage = "Street home required.")]
        [StringLength(50)]
        [Display(Name = "Street Home")]
        [DataType(DataType.Text)]
        public string StreetHome { get; set; }
        
        [Required(ErrorMessage = "Number home required.")]
        [StringLength(50)]
        [Display(Name = "Number Home")]
        [DataType(DataType.Text)]
        public string NumberHome { get; set; }

        [Display(Name = "Portal Home")]
        public long? PortalHome { get; set; }
        
        [Display(Name = "Stairs Home")]
        public long? StairsHome{ get; set; }
        
        [Display(Name = "Floor Home")]
        public int? FloorHome { get; set; }
        
        [StringLength(1)]
        [Display(Name = "Letter home")]
        [DataType(DataType.Text)]
        public string LetterHome { get; set; }
        
        [Display(Name = "Country Home")]
        public long CountryHomeId { get; set; }
        
        [Display(Name = "Province Home")]
        public long ProvinceHomeId { get; set; }
        
        [Display(Name = "Town Home")]
        public long TownHomeId { get; set; }
        
        [Required(ErrorMessage = "Postal code required.")]
        [StringLength(5)]
        [Display(Name = "Postal Code")]
        [DataType(DataType.Text)]
        public string PostalCodeHome { get; set; }
        
        [Required(ErrorMessage = "Phone house required.")]
        [StringLength(14)]
        [Display(Name = "Phone house")]
        [DataType(DataType.Text)]
        public string Landline { get; set; }
        
        [Required(ErrorMessage = "Mobile Phone.")]
        [StringLength(14)]
        [Display(Name = "Mobile Phone")]
        [DataType(DataType.Text)]
        public string MobilePhone { get; set; }
        
        [Required(ErrorMessage = "Imei.")]
        [StringLength(50)]
        [Display(Name = "Imei")]
        [DataType(DataType.Text)]
        public string Imei { get; set; }
        
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