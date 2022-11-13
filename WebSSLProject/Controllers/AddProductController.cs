//using CrystalDecisions.CrystalReports.Engine;
using WebSSLProject.Models;
using WebSSLProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using CrystalDecisions.CrystalReports.Engine;

namespace WebSSLProject.Controllers
{
    public class AddProductController : Controller
    {
        // GET: AddProduct
        [HttpGet]
        public ActionResult Crud(int? id)
        {
            var db = new Model1();
            var oProduct = (from o in db.Products where o.ProductId == id select o).FirstOrDefault();
            oProduct = oProduct == null ? new Product() : oProduct;
            ViewData["List"] = db.Products.ToList();
            ViewBag.BrandId = new SelectList(db.ProductBrands, "BrandId", "BrandName");
            return View(oProduct);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Crud(Product model, HttpPostedFileBase img)
        {
            string fileName = "";
            if (img != null)
            {
                fileName = img.FileName;
                // To save a image to a folder
                string picture = System.IO.Path.GetFileName(img.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/Photo"), picture);
                img.SaveAs(path);
            }
            var db = new Model1();
            var oProduct = db.Products.Find(model.ProductId);
            ViewBag.BrandId = new SelectList(db.ProductBrands, "BrandId", "BrandName");
            if (oProduct == null)
            {
                #region insert
                model.ProductPhoto = "/Photo/" + fileName;
                db.Products.Add(model);
                #endregion
                ViewBag.Message = "inserted successfully.";
            }
            else
            {
                #region update
                oProduct.ProductName = model.ProductName;
                oProduct.Price = model.Price;
                oProduct.Quantity = model.Quantity;
                oProduct.Stock = model.Stock;
                oProduct.SalesDate = model.SalesDate;
                oProduct.ProductPhoto = "/Photo/" + fileName;
                oProduct.BrandId = model.BrandId;
                #endregion
                ViewBag.Message = "updated successfully.";

            }
            
            db.SaveChanges();
            ViewData["List"] = db.Products.ToList();
            return RedirectToAction("Crud");
        }
        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var db = new Model1();
            var oProduct = (from o in db.Products where o.ProductId == id select o).FirstOrDefault();
            if (oProduct != null)
            {
                db.Products.Remove(oProduct);
                db.SaveChanges();
            }
            return RedirectToAction("Crud");
        }

        public ActionResult ExportCustomer(int? id)
        {
            var db = new Model1();
            // http://localhost:65102/
            var listProduct = (from x in db.Products
                               join y in db.ProductBrands on x.BrandId equals y.BrandId
                               where x.ProductId == id
                               select new VmProduct
                               {
                                   ProductId = x.ProductId,
                                   ProductName = x.ProductName,
                                   Price = x.Price,
                                   Quantity = x.Quantity,
                                   Stock = x.Stock,
                                   SalesDate = x.SalesDate,
                                   ProductPhoto = "localhost:44329" + x.ProductPhoto,
                                   BrandId = x.BrandId,
                                   BrandName = y.BrandName
                               }).ToList();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "CrystalReport1.rpt"));
            rd.SetDataSource(ListToDataTable(listProduct));
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Products.pdf");
            }
            catch
            {
                throw;
            }
        }

        public ActionResult ExportCustomers()
        {
            var db = new Model1();
            // http://localhost:65102/
            var listProduct = (from x in db.Products
                               join y in db.ProductBrands on x.BrandId equals y.BrandId
                               select new VmProduct
                               {
                                   ProductId = x.ProductId,
                                   ProductName = x.ProductName,
                                   Price = x.Price,
                                   Quantity = x.Quantity,
                                   Stock = x.Stock,
                                   SalesDate = x.SalesDate,
                                   ProductPhoto = "https://localhost:44329" + x.ProductPhoto,
                                   BrandId = x.BrandId,
                                   BrandName = y.BrandName
                               }).ToList();

            ReportDocument rd = new ReportDocument();
            rd.Load(Path.Combine(Server.MapPath("~/Report"), "CrystalReport1.rpt"));
            rd.SetDataSource(ListToDataTable(listProduct));
            Response.ClearContent();
            Response.ClearHeaders();
            try
            {
                Stream stream = rd.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                return File(stream, "application/pdf", "Products.pdf");
            }
            catch
            {
                throw;
            }
        }

        private DataTable ListToDataTable<T>(List<T> items)
        {
            DataTable datatable = new DataTable(typeof(T).Name);
            PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);
            foreach (PropertyInfo prop in Props)
            {
                datatable.Columns.Add(prop.Name);
            }
            foreach (T item in items)
            {
                var values = new object[Props.Length];
                for (int i = 0; i < Props.Length; i++)
                {
                    values[i] = Props[i].GetValue(item, null);
                }
                datatable.Rows.Add(values);
            }
            return datatable;
        }
    }
}