using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DRS.Services;
using DRS.ViewModels;

namespace DRS.Controllers
{
    public class Supplier_BrandController : Controller
    {
        // GET: Supplier_Brand
        public ActionResult Index(string SearchTerm = "")
        {
            Supplier_BrandListingViewModel model = new Supplier_BrandListingViewModel();
            var datalist = new List<DisplayModelForRelation>();
            
            var Supplier_Brands = Supplier_BrandServices.Instance.GetSupplier_Brands();
            foreach (var item in Supplier_Brands)
            {
                DisplayModelForRelation data = new DisplayModelForRelation();
                data.ID = item.ID;
                var branddata = BrandServices.Instance.GetBrandById(item.IDBrand);
                data.BrandLogo = branddata.Logo;
                data.BrandDescription = branddata.Description;

                var supplierdata = SupplierServices.Instance.GetSupplierById(item.IDSupplier);
                data.SupplierDescription = supplierdata.Description;
                data.Default = item.Default;
                data.Note = item.Note;
                datalist.Add(data);
            }
            model.BrandListing = datalist;
            return View("Index", model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {

            Supplier_BrandActionViewModel model = new Supplier_BrandActionViewModel();
            if (ID != 0)
            {
                var Supplier_Brand = Supplier_BrandServices.Instance.GetSupplier_BrandById(ID);
                model.ID = Supplier_Brand.ID;
                model.IDBrand = Supplier_Brand.IDBrand;
                model.IDSupplier = Supplier_Brand.IDSupplier;
                model.Default = Supplier_Brand.Default;
                model.Note = Supplier_Brand.Note;
            }
            model.Brands = BrandServices.Instance.GetBrands();
            model.Supplier = SupplierServices.Instance.GetSuppliers();
            return View("Action", model);
        }


        [HttpPost]
        public ActionResult Action(Supplier_BrandActionViewModel model)
        {
            

            if (model.ID != 0)
            {
                
                var Supplier_Brand = Supplier_BrandServices.Instance.GetSupplier_BrandById(model.ID);
                if(Supplier_Brand.Default == null)
                {
                    Supplier_Brand.Default = "off";
                }
                if(model.Default == "on")
                {
                    var data = Supplier_BrandServices.Instance.GetSupplier_Brand();

                    foreach (var item in data)
                    {
                        if (Supplier_Brand.IDBrand == item.IDBrand)
                        {
                            if (item.Default == "on")
                            {
                                item.Default = "off";
                                Supplier_BrandServices.Instance.UpdateSupplier_Brand(item);
                            }
                        }
                    }
                }
                

                Supplier_Brand.ID = model.ID;
                Supplier_Brand.IDBrand = model.IDBrand;
                Supplier_Brand.IDSupplier = model.IDSupplier;
                Supplier_Brand.Default = model.Default;
                Supplier_Brand.Note = model.Note;


                Supplier_BrandServices.Instance.UpdateSupplier_Brand(Supplier_Brand);

            }
            else
            {
                var Supplier_Brand = new Entities.Supplier_Brand();
                if (model.Default == "on")
                {
                    var data = Supplier_BrandServices.Instance.GetSupplier_Brand();

                    foreach (var item in data)
                    {
                        if (model.IDBrand == item.IDBrand)
                        {
                            if (item.Default == "on")
                            {
                                item.Default = "off";
                                Supplier_BrandServices.Instance.UpdateSupplier_Brand(item);
                            }
                        }
                    }
                }
                Supplier_Brand.IDBrand = model.IDBrand;
                Supplier_Brand.IDSupplier = model.IDSupplier;
                Supplier_Brand.Default = model.Default;
                Supplier_Brand.Note = model.Note;

                Supplier_BrandServices.Instance.CreateSupplier_Brand(Supplier_Brand);
            }




            return Json(new { success = true }, JsonRequestBehavior.AllowGet);


        }


        
        



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            Supplier_BrandActionViewModel model = new Supplier_BrandActionViewModel();
            var Supplier_Brand = Supplier_BrandServices.Instance.GetSupplier_BrandById(ID);
            model.ID = Supplier_Brand.ID;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(Supplier_BrandActionViewModel model)
        {
            if (model.ID != 0)
            {
                var Supplier_Brand = Supplier_BrandServices.Instance.GetSupplier_BrandById(model.ID);

                Supplier_BrandServices.Instance.DeleteSupplier_Brand(Supplier_Brand.ID);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}