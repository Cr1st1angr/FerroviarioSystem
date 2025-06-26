using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Ferroviario.Modelos
{
    public class PasswordResetToken
    {
        [Key] public int Id { get; set; }
        [Required] public string Token { get; set; }
        public DateTime Expiracion { get; set; } = DateTime.UtcNow.AddHours(1);

        [ForeignKey("Cliente")] public int ClienteId { get; set; }
        public Client? Cliente { get; set; }
    }

}
