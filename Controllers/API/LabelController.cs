using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PowerService.Controllers.API
{
    //Get lables from a model (e.g. accountmodel - display fields only) from configuration database
    public class LabelController : Controller
    {
        // GET: LabelController
        public ActionResult Index()
        {
            return View();
        }

        // GET: LabelController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LabelController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LabelController/Create
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

        // GET: LabelController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LabelController/Edit/5
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

        // GET: LabelController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LabelController/Delete/5
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
