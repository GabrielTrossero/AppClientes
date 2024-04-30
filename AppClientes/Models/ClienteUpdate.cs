using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppClientes.WebAPI.Models
{
    public class ClienteUpdate
    {
        [Required]
        [RegularExpression(@"^[0-9]+$")]
        public int Id { get; set; }

        [MaxLength(100)]
        public string? Nombres { get; set; }

        [MaxLength(100)]
        public string? Apellidos { get; set; }

        public DateTime? FechaNac { get; set; }

        [MinLength(13)]
        [MaxLength(13)]
        [RegularExpression(@"^[0-9-]+$")]
        public string? Cuit { get; set; }

        [MaxLength(100)]
        public string? Domicilio { get; set; }

        [MaxLength(20)]
        [Phone]
        public string? Telefono { get; set; }

        [MaxLength(50)]
        [EmailAddress]
        public string? Email { get; set; }
    }
}
