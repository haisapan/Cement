using System.Configuration;
using System.Data.Entity;
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
                var result = new
                {
                    Successed = true,
                    Error = "",
                    Message = ""
                };
                return Json(result);
            }
            catch
            {
                return View();
            }
        }

        // GET: Sale/Edit/5
        public ActionResult Edit(int id)
        {
            saleDBEntities saleDbEntities = new saleDBEntities();
            var cement=saleDbEntities.Cement.FirstOrDefault(p => p.Id == id);
            if (cement!=null)
            {
                return View(cement);
            }
            return HttpNotFound();
        }

        // POST: Sale/Edit/5
        [HttpPost]
        public ActionResult Edit(Cement model)
        {
            try
            {
                saleDBEntities saleDbEntities = new saleDBEntities();
                saleDbEntities.Entry(model).State=EntityState.Modified;
                saleDbEntities.SaveChanges();
                var result = new
                {
                    Successed = true,
                    Error = "",
                    Message = ""
                };
                return Json(result);
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

        [HttpPost]
        public ActionResult GetSaleData(string strJson,string search)
        {
            saleDBEntities saleDbEntities=new saleDBEntities();
            var cements=saleDbEntities.Cement.AsQueryable();
            //cements.Where(p=>p.CreatedTime)
            var result = new
            {
                total = cements.Count(), //总页数
                page = 1,   //当前页数
                records = cements.Count(),  //总记录数
                rows = cements,   //总
            };
            return Json(result);
        }
    }
}
