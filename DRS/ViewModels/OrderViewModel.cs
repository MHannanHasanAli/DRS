using DRS.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DRS.ViewModels
{
    public class OrderViewModel
    {
    }
    public class OrderListingViewModel
    {
        public List<Order> Orders { get; set; }
    }
    public class OrderActionViewModel
    {
        public List<Branch> Branches { get; set; }
        public List<Customer> Customers { get; set; }
        public List<Brand> Brands { get; set; }
        public int ID { get; set; }
        public int IDBranch { get; set; }
        public string IDUser { get; set; }
        public int IDCustomer { get; set; }
        public int IDBrand { get; set; }
        public int IDSupplier { get; set; }
        public string Plate { get; set; }
        public string Chassis { get; set; }
        public string Note { get; set; }
    }
}