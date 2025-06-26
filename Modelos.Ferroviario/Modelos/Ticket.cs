using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Modelos.Ferroviario.Modelos
{
    public class Ticket
    {
        [Key] public int Id { get; set; }
        [ForeignKey(nameof(Client))]public int ClienteId { get; set; }
        [ForeignKey(nameof(Ruta))]public int RutaId { get; set; }
        [ForeignKey(nameof(Schedule))]public int HorarioId { get; set; }
        [ForeignKey(nameof(Seat))]public int SeatId { get; set; }
        public DateTime FechaCompra { get; set; }
        public double PrecioFinal { get; set; }

        public string estadoTicket { get; set; }

        // Relaciones
        public Client? Cliente { get; set; }
        public Ruta? Ruta { get; set; }
        public Schedule? Horario { get; set; }
        public Seat? Asiento    { get; set; }

        // Otros campos adicionales
        public string Clase { get; set; } // Primera clase, clase económica, etc.
        public string CategoriaPasajero { get; set; } // Adulto, Niño, Tercera Edad, etc.
    }
}
