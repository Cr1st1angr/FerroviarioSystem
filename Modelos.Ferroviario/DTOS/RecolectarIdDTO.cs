using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Ferroviario.DTOS
{
    public class RecolectarIdDTO
    {
        public int HorarioId { get; set; } // ID del horario seleccionado
        public int SeatId { get; set; } // ID del asiento seleccionado
    }
}
