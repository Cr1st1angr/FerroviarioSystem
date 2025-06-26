using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos.Ferroviario.Modelos;
using Ferroviaria.Consumer;
using Modelos.Ferroviario.DTOS;

namespace MVC.Ferroviaria.Controllers
{
    public class VagonController : Controller
    {
        // GET: VagonController
        public ActionResult Index(int id)
        {
            var vagones = Crud<TrainCar>.GetBy("Train", id);
            TempData["HorarioId"] = id;
            TempData.Keep("HorarioId");
            return View(vagones);
        }

        // GET: VagonController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: VagonController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: VagonController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: VagonController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: VagonController/Edit/5
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

        // GET: VagonController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: VagonController/Delete/5
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
