using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos.Ferroviario.Modelos;

namespace Modelos.Ferroviario.Design_Patterns.Observer
{
    public class TicketObserver
    {
        private readonly Ticket _ticket;
        private readonly IObserverTicket _notificador;

        public TicketObserver(Ticket ticket, IObserverTicket notificador)
        {
            _ticket = ticket;
            _notificador = notificador;
        }

        public void CambiarEstado(string nuevoEstado)
        {
            _ticket.estadoTicket = nuevoEstado;
            _notificador.ActualizarEstadoTicket(
                $" {nuevoEstado}", _ticket);
        }

        public Ticket ObtenerTicket() => _ticket;
    }
}
