using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Ferroviario.DTOS
{
    public class CrearTicketDTO
    {

        public string Clase { get; set; } // Primera clase, clase económica, etc.
        public string CategoriaPasajero { get; set; } // Adulto, Niño, Tercera Edad, etc.
    }
}
