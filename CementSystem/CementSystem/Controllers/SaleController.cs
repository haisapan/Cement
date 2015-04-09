using System;
using System.Configuration;
using System.Data.Entity;
using System.Data.SQLite;
using System.Globalization;
using System.Linq;
using System.Linq.Dynamic;
using System.Web.Mvc;
using CementSystem.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

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
        public ActionResult GetSaleData(bool? _search, string nd, int page, int rows, string sidx, string sord, string filters, string customerName, string productionName, string driverName, string startDate,string endDate)
        {
            saleDBEntities saleDbEntities=new saleDBEntities();
            var cements=saleDbEntities.Cement.AsQueryable();

            if (!string.IsNullOrEmpty(customerName))
            {
                cements =
                    cements.Where(p => p.CustomerName != null && p.CustomerName.ToLower().Contains(customerName.ToLower()));
            }
            if (!string.IsNullOrEmpty(productionName))
            {
                cements =
                    cements.Where(p => p.ProductionName != null && p.ProductionName.ToLower().Contains(productionName.ToLower()));
            }
            if (!string.IsNullOrEmpty(driverName))
            {
                cements =
                    cements.Where(p => p.DriverName != null && p.DriverName.ToLower().Contains(driverName.ToLower()));
            }
            if (!string.IsNullOrEmpty(startDate))
            {
                var start=DateTime.Parse(startDate);
                cements = cements.Where(p => p.CreatedTime != null && p.CreatedTime >= start);
            }
            if (!string.IsNullOrEmpty(endDate))
            {
                var end = DateTime.Parse(endDate);
                cements = cements.Where(p => p.CreatedTime != null && p.CreatedTime <= end);
            }


            if (sidx != null)
            {
                sidx = sidx.Trim().EndsWith(sord) ? sidx : sidx.Trim() + " " + sord;
            }

            cements= cements.OrderBy(sidx);
            //cements.Where(p=>p.CreatedTime)
            var count = cements.Count();
            cements = cements.Skip((page - 1)*rows).Take(rows);

            var cementModelList = cements.Select(p => new
            {
                Id=p.Id,
                CustomerName=p.CustomerName,
                ProductionName = p.ProductionName,
                SendWeight = p.SendWeight,
                ReceiveWeight = p.ReceiveWeight,
                SendUnitPrice = p.SendUnitPrice,
                ReceiveUnitPrice = p.ReceiveUnitPrice,
                TransferUnitPriceInContract = p.TransferUnitPriceInContract,
                ReceiveTransferUnitPrice = p.ReceiveTransferUnitPrice,
                DriverName = p.DriverName,
                PaidAmount = p.PaidAmount,
                OverDraft = p.OverDraft,
                CreatedTime = p.CreatedTime,
                Remark = p.Remark,
                ShouldPaidAmount = p.ReceiveWeight * (p.ReceiveUnitPrice + p.TransferUnitPriceInContract),
                NotPaidAmount = p.ReceiveWeight * (p.ReceiveUnitPrice + p.TransferUnitPriceInContract) - p.PaidAmount,
                ShouldPaidTransferAmount = p.ReceiveWeight * p.ReceiveTransferUnitPrice,
                PaidTransferAmount = p.PaidTransferAmount,
                NotPaidTransferAmount = p.ReceiveWeight * p.ReceiveTransferUnitPrice - p.PaidTransferAmount,
            });
            var result = new
            {
                total = Math.Ceiling((float) count/(float) rows), //总页数
                page = page,   //当前页数
                records = count,  //总记录数
                rows = cementModelList,   //总
            };
            
            IsoDateTimeConverter timeFormat = new IsoDateTimeConverter();
            timeFormat.DateTimeFormat = CultureInfo.CurrentCulture.DateTimeFormat.ShortDatePattern ;

            var jsonResult=JsonConvert.SerializeObject(result, Formatting.Indented, timeFormat);
            return Content(jsonResult);
        }
    }
}
