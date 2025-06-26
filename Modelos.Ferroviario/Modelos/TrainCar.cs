using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Ferroviario.Modelos
{
    public class TrainCar
    {
        [Key]public int Id { get; set; }
        public int CantidadAsientos { get; set; }
        public string Tipo { get; set; } // Primera clase, clase económica, etc.
        public string Estado { get; set; } // Disponible, Ocupado, Mantenimiento, etc.

        [ForeignKey(nameof(Train))]public int TrenId { get; set; } // ID del tren al que pertenece este vagón
        public Train? Tren { get; set; }

        public List<Seat>? Seats { get; set; }

    }
}
