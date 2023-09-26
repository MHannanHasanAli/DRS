using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRS.Services;
using DRS.ViewModels;

namespace DRS.Controllers
{
    public class BranchController : Controller
    {
        // GET: Branch
        public ActionResult Index(string SearchTerm = "")
        {
            BranchListingViewModel model = new BranchListingViewModel();
            model.Branches = BranchServices.Instance.GetBranchs();
            return View("Index", model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {

            BranchActionViewModel model = new BranchActionViewModel();
            if (ID != 0)
            {
                var Branch = BranchServices.Instance.GetBranchById(ID);
                model.ID = Branch.ID;
                model.Description = Branch.Description;
                model.Email = Branch.Email;
                model.Telephone = Branch.Telephone;
                model.Whatsapp = Branch.Whatsapp;
                model.Note = Branch.Note;
            

            }
            return View("Action", model);
        }


        [HttpPost]
        public ActionResult Action(BranchActionViewModel model)
        {

            if (model.ID != 0)
            {
                var Branch = BranchServices.Instance.GetBranchById(model.ID);
                Branch.ID = model.ID;
                Branch.Description = model.Description;
                Branch.Email = model.Email;
                Branch.Telephone = model.Telephone;
                Branch.Whatsapp = model.Whatsapp;
                Branch.Note = model.Note;
            

                BranchServices.Instance.UpdateBranch(Branch);

            }
            else
            {
                var Branch = new Entities.Branch();
                Branch.Description = model.Description;
                Branch.Email = model.Email;
                Branch.Telephone = model.Telephone;
                Branch.Whatsapp = model.Whatsapp;
                Branch.Note = model.Note;
             

                BranchServices.Instance.CreateBranch(Branch);

            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            BranchActionViewModel model = new BranchActionViewModel();
            var Branch = BranchServices.Instance.GetBranchById(ID);
            model.ID = Branch.ID;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(BranchActionViewModel model)
        {
            if (model.ID != 0)
            {
                var Branch = BranchServices.Instance.GetBranchById(model.ID);

                BranchServices.Instance.DeleteBranch(Branch.ID);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}