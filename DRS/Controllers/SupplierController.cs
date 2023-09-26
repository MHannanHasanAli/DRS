using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRS.Services;
using DRS.ViewModels;

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