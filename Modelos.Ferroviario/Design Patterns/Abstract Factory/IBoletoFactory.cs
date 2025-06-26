using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos.Ferroviario.Modelos;

namespace Modelos.Ferroviario.Design_Patterns.Abstract_Factory
{
    public interface IBoletoFactory
    {
        public Ticket CrearBoleto(int ClienteId, int RutaId, int HorarioId, int SeatId, DateTime FechaCompra, double PrecioFinal, string CategoriaPasajero);
    }
}
