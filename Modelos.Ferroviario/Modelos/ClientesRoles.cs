using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Ferroviario.Modelos
{
    public class ClientesRoles
    {
        [Key]public int Id { get; set; }
        [ForeignKey(nameof(Client))]public int ClienteId { get; set; } // ID del cliente
        [ForeignKey(nameof(Roles))] public int RolId { get; set; } // ID del rol
        public Client? Cliente { get; set; } // Relación con el cliente
        public Roles? Rol { get; set; } // Relación con el rol
    }
}
