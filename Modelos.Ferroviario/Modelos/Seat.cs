using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Ferroviario.Modelos
{
    public class Seat
    {
        [Key]
        public int Id { get; set; }
        public int Number { get; set; }
        public string Type  { get; set; }
        public bool IsAvailable { get; set; } // Indica si el asiento está disponible o 
        [ForeignKey(nameof(TrainCar))]public int TrainCarId { get; set; } // ID del vagón al que pertenece este asiento
        public TrainCar? TrainCar { get; set; }


    }
}
