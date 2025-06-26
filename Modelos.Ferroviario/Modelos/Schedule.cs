using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Ferroviario.Modelos
{
    public class Schedule
    {
        [Key]
        public int Id { get; set; }
        public DateTime HoraSalida { get; set; }
        public DateTime HoraLlegada { get; set; }

        [ForeignKey(nameof(Ruta))]public int RutaId { get; set; } // ID de la ruta asociada a este horario
        [ForeignKey(nameof(Train))]public int TrenId { get; set; } // ID del tren asociado a este horario
        public Train? Tren { get; set; }

        public Ruta? Ruta { get; set; } // Ruta asociada a este horario


    }
}
