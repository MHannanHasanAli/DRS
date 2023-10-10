using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRS.Entities;
using DRS.Services;
using DRS.ViewModels;
using Microsoft.AspNet.Identity;
using OfficeOpenXml;

namespace DRS.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Index(string SearchTerm = "")
        {
            SupplierListingViewModel model = new SupplierListingViewModel();
            model.Suppliers = SupplierServices.Instance.GetSuppliers();
            return View("Index", model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {

            SupplierActionViewModel model = new SupplierActionViewModel();
            if (ID != 0)
            {
                var Supplier = SupplierServices.Instance.GetSupplierById(ID);
                model.ID = Supplier.ID;
                model.Description = Supplier.Description;
                model.Email = Supplier.Email;
                model.Telephone = Supplier.Telephone;
                model.Whatsapp = Supplier.Whatsapp;
                model.Note = Supplier.Note;
                model.Contact = Supplier.Contact;
                model.Logo = Supplier.Logo;

            }
            return View("Action", model);
        }
        public ActionResult ExportToExcel()
        {

            var supplier = SupplierServices.Instance.GetSuppliers();


            System.Data.DataTable tableData = new System.Data.DataTable();
            tableData.Columns.Add("ID", typeof(int)); // Replace "Column1" with the actual column name
            tableData.Columns.Add("Description", typeof(string)); // Replace "Column2" with the actual column name
            tableData.Columns.Add("Telephone", typeof(string)); // Replace "Column2" with the actual column name
            tableData.Columns.Add("Whatsapp", typeof(string)); // Replace "Column2" with the actual column nam
            tableData.Columns.Add("Email", typeof(string)); // Replace "Column2" with the actual column name
            tableData.Columns.Add("Note", typeof(string)); // Replace "Column2" with the actual column name
            tableData.Columns.Add("Contact", typeof(string)); // Replace "Column1" with the actual column name
            
            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

                foreach (var item in supplier)
                {
                        DataRow row = tableData.NewRow();
                        row["ID"] = item.ID;
                        row["Description"] = item.Description;
                        row["Telephone"] = item.Telephone;
                        row["Whatsapp"] = item.Whatsapp;
                        row["Email"] = item.Email;
                        row["Note"] = item.Note;
                        row["Contact"] = item.Contact;

                        tableData.Rows.Add(row);
                }

                

            

            // Create the Excel package
            using (ExcelPackage package = new ExcelPackage())
            {
                // Create a new worksheet
                ExcelWorksheet worksheet = package.Workbook.Worksheets.Add("Products");

                // Set the column names
                for (int i = 0; i < tableData.Columns.Count; i++)
                {
                    worksheet.Cells[1, i + 1].Value = tableData.Columns[i].ColumnName;
                }

                // Set the row data
                for (int row = 0; row < tableData.Rows.Count; row++)
                {
                    for (int col = 0; col < tableData.Columns.Count; col++)
                    {
                        worksheet.Cells[row + 2, col + 1].Value = tableData.Rows[row][col];
                    }
                }

                // Auto-fit columns for better readability
                worksheet.Cells.AutoFitColumns();

                // Convert the Excel package to a byte array
                byte[] excelBytes = package.GetAsByteArray();

                // Return the Excel file as a downloadable file
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Suppliers.xlsx");
            }
        }

        [HttpPost]
        public ActionResult Action(SupplierActionViewModel model)
        {

            if (model.ID != 0)
            {
                var Supplier = SupplierServices.Instance.GetSupplierById(model.ID);
                Supplier.ID = model.ID;
                Supplier.Description = model.Description;
                Supplier.Email = model.Email;
                Supplier.Telephone = model.Telephone;
                Supplier.Whatsapp = model.Whatsapp;
                Supplier.Note = model.Note;
                Supplier.Contact = model.Contact;
                Supplier.Logo = model.Logo;


                SupplierServices.Instance.UpdateSupplier(Supplier);

            }
            else
            {
                var Supplier = new Entities.Supplier();
                Supplier.Description = model.Description;
                Supplier.Email = model.Email;
                Supplier.Telephone = model.Telephone;
                Supplier.Whatsapp = model.Whatsapp;
                Supplier.Note = model.Note;
                Supplier.Contact = model.Contact;
                Supplier.Logo = model.Logo;

                SupplierServices.Instance.CreateSupplier(Supplier);

            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            SupplierActionViewModel model = new SupplierActionViewModel();
            var Supplier = SupplierServices.Instance.GetSupplierById(ID);
            model.ID = Supplier.ID;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(SupplierActionViewModel model)
        {
            if (model.ID != 0)
            {
                var Supplier = SupplierServices.Instance.GetSupplierById(model.ID);

                SupplierServices.Instance.DeleteSupplier(Supplier.ID);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}