﻿using DRS.Entities;
using DRS.Services;
using DRS.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DRS.Controllers
{
    public class OrderController : Controller
    {
        private AMSignInManager _signInManager;
        private AMUserManager _userManager;
        public AMSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<AMSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }
        public AMUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<AMUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private AMRolesManager _rolesManager;
        public AMRolesManager RolesManager
        {
            get
            {
                return _rolesManager ?? HttpContext.GetOwinContext().GetUserManager<AMRolesManager>();
            }
            private set
            {
                _rolesManager = value;
            }
        }
        public OrderController()
        {
        }



        public OrderController(AMUserManager userManager, AMSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }
        // GET: Order
        [HttpPost] // You can use [HttpGet] if appropriate
        public ActionResult GetSuppliersByBrand(int brandID)
        {
            var supplierList = new List<Supplier>();
            var supplierids = new List<int>();
            var suppliers = Supplier_BrandServices.Instance.GetSupplier_BrandsByBrandID(brandID);
            
            foreach (var item in suppliers)
            {
                if(item.Default == "on")
                {
                    supplierids.Add(item.IDSupplier);
                }
            }
            foreach (var item in suppliers)
            {
                if (item.Default == "off")
                {
                    supplierids.Add(item.IDSupplier);
                }
            }
            foreach (var item in supplierids)
            {
                var data = SupplierServices.Instance.GetSupplierById(item);
                supplierList.Add(data);
            }
            // Return the supplier data as JSON
            return Json(supplierList, JsonRequestBehavior.AllowGet);
        }
        public ActionResult ActionProducts(string products)
        {

            var Orderid = OrderServices.Instance.GetLastOrderId();
            var ListOfInventory = JsonConvert.DeserializeObject<List<OrderProductModel>>(products);
            var OrderItem = new Order_Item();
            foreach (var item in ListOfInventory)
            {
                if (item.Quantity == null)
                {
                    item.Quantity = "0";
                }
                if (item.ItemId == null)
                {
                    item.ItemId = "0";
                }
                if (item.Name == null)
                {
                    item.Name = "No Name";
                }

                OrderItem.IDOrder = Orderid;
                OrderItem.Note = item.Note;
                OrderItem.Description = item.Name;
                OrderItem.Quantity = int.Parse(item.Quantity);
                OrderItem.DateOrder = DateTime.Now;

                Order_ItemServices.Instance.CreateOrder_Item(OrderItem);

            }
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Index(string SearchTerm = "")
        {
            OrderListingViewModel model = new OrderListingViewModel();
            model.Orders = OrderServices.Instance.GetOrders();
            return View("Index", model);
        }


        [HttpGet]
        public ActionResult Action(int ID = 0)
        {

            OrderActionViewModel model = new OrderActionViewModel();
            if (ID != 0)
            {
                var Order = OrderServices.Instance.GetOrderById(ID);
                model.ID = Order.ID;
                model.IDBranch = Order.IDBranch;
                model.IDCustomer = Order.IDCustomer;
                model.IDBrand = Order.IDBrand;
                model.IDSupplier = Order.IDSupplier;
                model.Note = Order.Note;
                model.Plate = Order.Plate;
                model.Chassis = Order.Chassis;
                model.IDUser = Order.IDUser;

            }
            model.Branches = BranchServices.Instance.GetBranchs();
            model.Customers = CustomerServices.Instance.GetCustomers();
            model.Brands = BrandServices.Instance.GetBrands();

            return View("Action", model);
        }


        [HttpPost]
        public ActionResult Action(OrderActionViewModel model)
        {
            var user = UserManager.FindById(User.Identity.GetUserId());
            if (model.ID != 0)
            {
                var Order = OrderServices.Instance.GetOrderById(model.ID);
                Order.ID = model.ID;
                Order.IDBranch = model.IDBranch;
                Order.IDCustomer = model.IDCustomer;
                Order.IDBrand = model.IDBrand;
                Order.IDSupplier = model.IDSupplier;
                Order.Note = model.Note;
                Order.Plate = model.Plate;
                Order.Chassis = model.Chassis;
                Order.IDUser = user.Name;

                OrderServices.Instance.UpdateOrder(Order);

            }
            else
            {
                var Order = new Entities.Order();
                Order.IDBranch = model.IDBranch;
                Order.IDCustomer = model.IDCustomer;
                Order.IDBrand = model.IDBrand;
                Order.IDSupplier = model.IDSupplier;
                Order.Note = model.Note;
                Order.Plate = model.Plate;
                Order.Chassis = model.Chassis;
                Order.IDUser = user.Name;
                OrderServices.Instance.CreateOrder(Order);

            }


            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }



        [HttpGet]
        public ActionResult Delete(int ID)
        {
            OrderActionViewModel model = new OrderActionViewModel();
            var Order = OrderServices.Instance.GetOrderById(ID);
            model.ID = Order.ID;
            return PartialView("_Delete", model);
        }

        [HttpPost]
        public ActionResult Delete(OrderActionViewModel model)
        {
            if (model.ID != 0)
            {
                var Order = OrderServices.Instance.GetOrderById(model.ID);

                OrderServices.Instance.DeleteOrder(Order.ID);
            }

            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
    }
}