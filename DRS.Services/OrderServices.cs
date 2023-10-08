using DRS.Database;
using DRS.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Services
{
    public class OrderServices
    {
        #region Singleton
        public static OrderServices Instance
        {
            get
            {
                if (instance == null) instance = new OrderServices();
                return instance;
            }
        }
        private static OrderServices instance { get; set; }
        private OrderServices()
        {
        }
        #endregion
        public List<Order> GetOrder(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                if (SearchTerm != "")
                {
                    return context.orders.Where(p => p.Note != null && p.Note.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x.Note)
                                            .ToList();
                }
                else
                {
                    return context.orders.OrderBy(x => x.Note).ToList();
                }
            }
        }
        public List<string> GetOrderNames()
        {
            using (var context = new DSContext())
            {
                var data = context.orders.Select(c => c.Note).ToList();
                data.Reverse();
                return data;
            }
        }
        public int GetLastOrderId()
        {
            using (var context = new DSContext())
            {
                var lastAdjustment = context.orders.OrderByDescending(a => a.ID).FirstOrDefault();
                if (lastAdjustment != null)
                {
                    return lastAdjustment.ID;
                }
                // Return a default value (e.g., -1) or throw an exception if there are no entries.
                // You can decide the appropriate behavior based on your application requirements.
                return -1; // Default value when there are no entries.
            }
        }
        public Order GetOrderInOrders(int Sentid)
        {
            using (var context = new DSContext())
            {
                var category = context.orders.FirstOrDefault(c => c.ID == Sentid);
                return category;

            }
        }
        public List<Order> GetOrders()
        {
            using (var context = new DSContext())
            {
                var data = context.orders.ToList();
                data.Reverse();
                return data;
            }
        }
        public List<Order> GetOrders(string SearchTerm)
        {
            using (var context = new DSContext())
            {
                return context.orders.Where(p => p.Note != null && p.Note.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x.Note)
                                            .ToList();
            }
        }

        public Entities.Order GetOrderById(int id)
        {
            using (var context = new DSContext())
            {
                return context.orders.Find(id);

            }
        }

        public void CreateOrder(Order Order)
        {
            using (var context = new DSContext())
            {
                context.orders.Add(Order);
                context.SaveChanges();
            }
        }

        public void UpdateOrder(Entities.Order Order)
        {
            using (var context = new DSContext())
            {
                context.Entry(Order).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


        public void DeleteOrder(int ID)
        {
            using (var context = new DSContext())
            {

                var Product = context.orders.Find(ID);
                context.orders.Remove(Product);
                context.SaveChanges();
            }
        }
    }
}
