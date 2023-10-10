using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRS.Services;
using DRS.ViewModels;
using OfficeOpenXml;

namespace DRS.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index(string SearchTerm = "")
        {
            CustomerListingViewModel model = new CustomerListingViewModel();
            model.Customers = CustomerServices.Instance.GetCustomers();
            return View("Index", model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {

            CustomerActionViewModel model = new CustomerActionViewModel();
            if (ID != 0)
            {
                var Customer = CustomerServices.Instance.GetCustomerById(ID);
                model.ID = Customer.ID;
                model.Description = Customer.Description;
                model.Email = Customer.Email;
                model.Telephone = Customer.Telephone;
                model.Whatsapp = Customer.Whatsapp;
                model.Note = Customer.Note;
                model.Alias = Customer.Alias;
                model.Logo = Customer.Logo;

            }
            return View("Action", model);
        }

        public ActionResult ExportToExcel()
        {

            var customer = CustomerServices.Instance.GetCustomers();


            System.Data.DataTable tableData = new System.Data.DataTable();
            tableData.Columns.Add("ID", typeof(int)); // Replace "Column1" with the actual column name
            tableData.Columns.Add("Description", typeof(string));
            tableData.Columns.Add("Alias", typeof(string)); // Replace "Column1" with the actual column name                                                            // Replace "Column2" with the actual column name
            tableData.Columns.Add("Telephone", typeof(string)); // Replace "Column2" with the actual column name
            tableData.Columns.Add("Whatsapp", typeof(string)); // Replace "Column2" with the actual column nam
            tableData.Columns.Add("Email", typeof(string)); // Replace "Column2" with the actual column name
            tableData.Columns.Add("Note", typeof(string)); // Replace "Column2" with the actual column name

            ExcelPackage.LicenseContext = OfficeOpenXml.LicenseContext.NonCommercial;

            foreach (var item in customer)
            {
                DataRow row = tableData.NewRow();
                row["ID"] = item.ID;
                row["Description"] = item.Description;
                row["Alias"] = item.Alias;
                row["Telephone"] = item.Telephone;
                row["Whatsapp"] = item.Whatsapp;
                row["Email"] = item.Email;
                row["Note"] = item.Note;
               

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
                return File(excelBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Customers.xlsx");
            }
        }

        [HttpPost]
        public ActionResult Action(CustomerActionViewModel model)
        {

            if (model.ID != 0)
            {
                var Customer = CustomerServices.Instance.GetCustomerById(model.ID);
                Customer.ID = model.ID;
                Customer.Description = model.Description;
                Customer.Email = model.Email;
                Customer.Telephone = model.Telephone;
                Customer.Whatsapp = model.Whatsapp;
                Customer.Note = model.Note;
                Customer.Alias = model.Alias;
                Customer.Logo = model.Logo;


                CustomerServices.Instance.UpdateCustomer(Customer);

            }
            else
            {
                var Customer = new Entities.Customer();
                Customer.Description = model.Description;
                Customer.Email = model.Email;
                Customer.Telephone = model.Telephone;
                Customer.Whatsapp = model.Whatsapp;
                Customer.Note = model.Note;
                Customer.Alias = model.Alias;
                Customer.Logo = model.Logo;

                CustomerServices.Instance.CreateCustomer(Customer);
              
            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            CustomerActionViewModel model = new CustomerActionViewModel();
            var Customer = CustomerServices.Instance.GetCustomerById(ID);
            model.ID = Customer.ID;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(CustomerActionViewModel model)
        {
            if (model.ID != 0)
            {
                var Customer = CustomerServices.Instance.GetCustomerById(model.ID);

                CustomerServices.Instance.DeleteCustomer(Customer.ID);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}