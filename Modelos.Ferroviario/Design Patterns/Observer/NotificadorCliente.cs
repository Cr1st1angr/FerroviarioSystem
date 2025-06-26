using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Ferroviaria.Consumer;
using Modelos.Ferroviario.Modelos;

namespace Modelos.Ferroviario.Design_Patterns.Observer
{
    public class NotificadorCliente: IObserverTicket
    {
        private readonly IMensajeUI _ui;

        public NotificadorCliente(IMensajeUI ui)
        {
            _ui = ui;
        }

        public void ActualizarEstadoTicket(string mensaje, Ticket ticket)
        {
            var ruta = Crud<Ruta>.GetById(ticket.RutaId);
            _ui.MostrarMensaje($"El estado de tu ticket {ruta.CiudadInicio} - {ruta.CiudadDestino} cambió a: {mensaje}");
        }

    }
}
