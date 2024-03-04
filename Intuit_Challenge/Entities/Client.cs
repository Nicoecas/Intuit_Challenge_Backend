using Intuit_Challenge.Validations;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Intuit_Challenge.Entities
{
    public class Client : BaseEntity
    {
        [Required]
        public string Names { get; set; }
        [Required]
        public string LastNames { get; set; }
        [Required]
        public string Cuit { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string CellPhone { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [RequiredGreaterInYears]
        public DateTime Birthday { get; set; }

        [ScaffoldColumn(false)]
        [NotMapped]
        public string FullName
        {
            get
            {
                return this.Names + " " + this.LastNames;
            }
        }
    }
}