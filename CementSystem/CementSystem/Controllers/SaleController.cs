using System.Configuration;
using System.Data.SQLite;
using System.Linq;
using System.Web.Mvc;
using CementSystem.Models;

namespace CementSystem.Controllers
{
    public class SaleController : Controller
    {
        // GET: Sale
        public ActionResult Index()
        {
            saleDBEntities saleDbEntities=new saleDBEntities();
            var cements=saleDbEntities.Cement.ToList();

            
            return View();
        }

        // GET: Sale/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sale/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sale/Create
        [HttpPost]
        public ActionResult Create(Cement model)
        {
            try
            {
                saleDBEntities saleDbEntities = new saleDBEntities();
                saleDbEntities.Cement.Add(model);
                saleDbEntities.SaveChanges();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sale/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Sale/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sale/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Sale/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
