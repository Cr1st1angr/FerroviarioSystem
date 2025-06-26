using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Ferroviario.Modelos
{
    public class Roles
    {
        [Key] public int Id { get; set; }
        public string Nombre { get; set; } // Nombre del rol (Administrador, Usuario, etc.)
        public string Descripcion { get; set; } // Descripción del rol
    }
}
