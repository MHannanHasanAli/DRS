using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRS.Services;
using DRS.ViewModels;

namespace DRS.Controllers
{
    public class BrandController : Controller
    {
        // GET: Brand
        public ActionResult Index(string SearchTerm = "")
        {
            BrandListingViewModel model = new BrandListingViewModel();
            model.Brands = BrandServices.Instance.GetBrands();
            return View("Index", model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {

            BrandActionViewModel model = new BrandActionViewModel();
            if (ID != 0)
            {
                var Brand = BrandServices.Instance.GetBrandById(ID);
                model.ID = Brand.ID;
                model.Description = Brand.Description;
                model.Note = Brand.Note;
                model.Logo = Brand.Logo;

            }
            return View("Action", model);
        }


        [HttpPost]
        public ActionResult Action(BrandActionViewModel model)
        {

            if (model.ID != 0)
            {
                var Brand = BrandServices.Instance.GetBrandById(model.ID);
                Brand.ID = model.ID;
                Brand.Description = model.Description;
                Brand.Note = model.Note;
                Brand.Logo = model.Logo;


                BrandServices.Instance.UpdateBrand(Brand);

            }
            else
            {
                var Brand = new Entities.Brand();
                Brand.Description = model.Description;
                Brand.Note = model.Note;
                Brand.Logo = model.Logo;

                BrandServices.Instance.CreateBrand(Brand);

            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            BrandActionViewModel model = new BrandActionViewModel();
            var Brand = BrandServices.Instance.GetBrandById(ID);
            model.ID = Brand.ID;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(BrandActionViewModel model)
        {
            if (model.ID != 0)
            {
                var Brand = BrandServices.Instance.GetBrandById(model.ID);

                BrandServices.Instance.DeleteBrand(Brand.ID);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}