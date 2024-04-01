using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Repository_Layer.Entity
{
    public class BookEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int BookId { get; set; }

        [Required]
        public string BookName { get; set; }

        [Required]
        public string Author { get; set; }

        [Required]
        public int Price { get; set; }

        [Required]
        public int Quantity { get; set; }


        [ForeignKey("Registrations_Details")]
        public int AdminId { get; set; }

        [JsonIgnore]
        public virtual UserRegistrationEntity Registrations_Details { get; set; }
    }
}
