using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRS.Services;
using DRS.ViewModels;

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