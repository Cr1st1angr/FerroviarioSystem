using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos.Ferroviario.Modelos;

namespace Modelos.Ferroviario.Design_Patterns.Abstract_Factory
{
    public class BoletoPreferencial: IBoletoFactory
    {
        public Ticket CrearBoleto(int ClienteId, int RutaId, int HorarioId, int SeatId, DateTime FechaCompra, double PrecioFinal, string CategoriaPasajero)
        {
            var ticket = new Ticket
            {
                Id = 0,
                ClienteId = ClienteId,
                RutaId = RutaId,
                HorarioId = HorarioId,
                SeatId = SeatId,
                FechaCompra = FechaCompra,
                PrecioFinal = PrecioFinal,
                estadoTicket = "Reservado", // Estado inicial del ticket
                Clase = "Preferencial",
                CategoriaPasajero = CategoriaPasajero
            };
            return ticket;
        }
    }
}
