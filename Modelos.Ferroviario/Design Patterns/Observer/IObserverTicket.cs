using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Modelos.Ferroviario.Modelos;

namespace Modelos.Ferroviario.Design_Patterns.Observer
{
    public interface IObserverTicket
    {
        public void ActualizarEstadoTicket(string mensaje, Ticket ticket);
    }
}
