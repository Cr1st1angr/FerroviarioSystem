using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Ferroviario.Modelos
{
    public class Train
    {
        [Key]public int Id { get; set; }
        public string Nombre { get; set; } // Nombre del tren
        public List<TrainCar>? TrainsCar { get; set; }
    }
}
