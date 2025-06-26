using System.Security.Claims;
using Ferroviaria.Consumer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Modelos.Ferroviario.Design_Patterns.Estrategy;
using Modelos.Ferroviario.Design_Patterns.Observer;
using Modelos.Ferroviario.DTOS;
using Modelos.Ferroviario.Modelos;
using Modelos.Ferroviario.Design_Patterns.Observer;
using Modelos.Ferroviario.Design_Patterns.Abstract_Factory;

namespace MVC.Ferroviaria.Controllers
{
    public class TicketController : Controller,IMensajeUI
    {

        public void MostrarMensaje(string mensaje)
        { 
            TempData["TicketMensaje"] = mensaje;
            TempData["TicketMensajeTipo"] = "success";
        }
        // GET: TicketController

        [Authorize]
        public ActionResult Index(int id)
        {
            if (TempData["TicketMensaje"] != null)
            {
                ViewBag.Mensaje = TempData["TicketMensaje"];
            }

            var data = Crud<Ticket>.GetBy("Cliente", id);
            return View(data);
        }

        // GET: TicketController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TicketController/Create
        public ActionResult Create(int id)
        {
            TempData["SeatId"] = id;
            TempData.Keep("SeatId");
            ViewBag.CategoriaPasajero = new List<SelectListItem>
            {
                new SelectListItem { Text = "Adulto", Value = "Adulto" },
                new SelectListItem { Text = "Niño", Value = "Niño" },
                new SelectListItem { Text = "Tercera Edad", Value = "Tercera Edad" }
            };

            ViewBag.Clase = new List<SelectListItem>
            {
                new SelectListItem { Text = "Economica", Value = "Economica" },
                new SelectListItem { Text = "Preferencial", Value = "Preferencial" }
            };
            return View();
        }

        // POST: TicketController/Create
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CrearTicketDTO data)
        {
            int asientoId = TempData["SeatId"] != null ? (int)TempData["SeatId"] : 0;
            var horarioId = TempData["HorarioId"] != null ? (int)TempData["HorarioId"] : 0;

            var horario = Crud<Schedule>.GetById(horarioId);
            CalculadoraPrecioAdultoMayor calculadora = new CalculadoraPrecioAdultoMayor();
            ClaculadoraPrecioAdulto calculadoraAdulto = new ClaculadoraPrecioAdulto();
            ClaculadoraPrecioKid calculadoraKid = new ClaculadoraPrecioKid();
            BoletoEconomico boletoEconomico = new BoletoEconomico();
            BoletoPreferencial boletoPreferencial = new BoletoPreferencial();
            double precio = 0;

            if (data.CategoriaPasajero.Equals("Adulto"))
            {
                precio = calculadoraAdulto.CalcularPrecio();
            }else if(data.CategoriaPasajero.Equals("Tercera Edad"))
            {
                precio = calculadora.CalcularPrecio();
            }else if (data.CategoriaPasajero.Equals("Niño"))
            {
                precio = calculadoraKid.CalcularPrecio();
            }
            
            try
                {
                var ticket = new Ticket();

                if (data.Clase.Equals("Economica"))
                {
                    ticket = boletoEconomico.CrearBoleto(
                        int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0"),
                        horario.RutaId,
                        horarioId,
                        asientoId,
                        DateTime.UtcNow,
                        precio,
                        data.CategoriaPasajero);
                    Crud<Ticket>.Create(ticket);
                }
                else if (data.Clase.Equals("Preferencial"))
                {
                    ticket = boletoPreferencial.CrearBoleto(
                        int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? "0"),
                        horario.RutaId,
                        horarioId,
                        asientoId,
                        DateTime.UtcNow,
                        precio,
                        data.CategoriaPasajero);
                    Crud<Ticket>.Create(ticket);
                }
                    

                var asiento = Crud<Seat>.GetById(asientoId);
                asiento.IsAvailable = false;
                asiento.Type = data.CategoriaPasajero;
                Crud<Seat>.Update(asientoId, asiento);

                return RedirectToAction("Index", "Ticket", new { id = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value });
            }
                catch (Exception e)
                {
                Console.WriteLine(e.Message);
                return View();
                }
        }

        // GET: TicketController/Edit/5
        public ActionResult PagarTicket(int id)
        {
            ViewBag.TicketId = id;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> UploadReceipt(int ticketId, IFormFile receiptFile)
        {
            try
            {
                if (receiptFile == null || receiptFile.Length == 0)
                    return BadRequest("Debe subir un archivo válido.");

                // Ruta donde guardarás la imagen, por ejemplo wwwroot/uploads/
                var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                // Genera un nombre único para el archivo para evitar colisiones
                var uniqueFileName = $"{Guid.NewGuid()}_{receiptFile.FileName}";
                var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await receiptFile.CopyToAsync(stream);
                }

                var ticket = Crud<Ticket>.GetById(ticketId);
                ticket.estadoTicket = "Pagado";
                Crud<Ticket>.Update(ticketId, ticket);

                // Aquí usamos el patrón Observer
                var notificador = new NotificadorCliente(this);
                var observer = new TicketObserver(ticket, notificador);
                observer.CambiarEstado("Pagado");

                return RedirectToAction("Index", "Ticket", new { id = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value });
            }

            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return View();

            }
        }

        public ActionResult Cancelar(int id)
        {
            try
            {
                var ticket = Crud<Ticket>.GetById(id);
                ticket.estadoTicket = "Cancelado";
                Crud<Ticket>.Update(id, ticket);

                var asiento = Crud<Seat>.GetById(ticket.SeatId);
                asiento.IsAvailable = true; // Marcar el asiento como disponible
                asiento.Type = "Disponible"; // Limpiar el tipo de pasajero

                Crud<Seat>.Update(ticket.SeatId, asiento);
                // Usar Observer para cambiar el estado
                var notificador = new NotificadorCliente(this);
                var observer = new TicketObserver(ticket, notificador);
                observer.CambiarEstado("Cancelado");
                Console.WriteLine("Mensaje enviado al notificador.");
                return RedirectToAction("Index", "Ticket", new { id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "No se pudo cancelar el ticket.";
                return RedirectToAction("Index", "Ticket", new { id = User.FindFirst(ClaimTypes.NameIdentifier)?.Value });
            }
        }

        // POST: TicketController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TicketController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TicketController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
