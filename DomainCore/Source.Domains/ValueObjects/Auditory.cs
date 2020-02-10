using System;
using System.ComponentModel.DataAnnotations;

namespace Source.Domains.ValueObjects
{
    public class Auditory
    {
        [Required(ErrorMessage = "CreatedBy required.")]
        public long CreatedBy { get; set; }
        [DataType(DataType.Date)]
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        
        
        public long? UpdateBy { get; set; }
        [DataType(DataType.Date)]
        public DateTime? UpdatedAt { get; set; }
        
        
    }
}