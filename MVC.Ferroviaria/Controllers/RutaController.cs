﻿using Ferroviaria.Consumer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Modelos.Ferroviario.Modelos;

namespace MVC.Ferroviaria.Controllers
{
    public class RutaController : Controller
    {
        // GET: RutaController
        public ActionResult Index()
        {
            var data = Crud<Ruta>.GetAll();
            return View(data);
        }

        // GET: RutaController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RutaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RutaController/Create
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

        // GET: RutaController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RutaController/Edit/5
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

        // GET: RutaController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RutaController/Delete/5
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
