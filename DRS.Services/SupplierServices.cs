using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DRS.Database;
using DRS.Entities;

namespace DRS.Services
{
    public class SupplierServices
    {
        #region Singleton
        public static SupplierServices Instance
        {
            get
            {
                if (instance == null) instance = new SupplierServices();
                return instance;
            }
        }
        private static SupplierServices instance { get; set; }
        private SupplierServices()
        {
        }
        #endregion
        public List<Supplier> GetSupplier(string SearchTerm = "")
        {
            using (var context = new DSContext())
            {
                if (SearchTerm != "")
                {
                    return context.suppliers.Where(p => p.Description != null && p.Description.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x.Description)
                                            .ToList();
                }
                else
                {
                    return context.suppliers.OrderBy(x => x.Description).ToList();
                }
            }
        }
        public List<string> GetSupplierNames()
        {
            using (var context = new DSContext())
            {
                var data = context.suppliers.Select(c => c.Description).ToList();
                data.Reverse();
                return data;
            }
        }
        public Supplier GetSupplierInSuppliers(int Sentid)
        {
            using (var context = new DSContext())
            {
                var category = context.suppliers.FirstOrDefault(c => c.ID == Sentid);
                return category;

            }
        }
        public List<Supplier> GetSuppliers()
        {
            using (var context = new DSContext())
            {
                var data = context.suppliers.ToList();
                data.Reverse();
                return data;
            }
        }
        public List<Supplier> GetSuppliers(string SearchTerm)
        {
            using (var context = new DSContext())
            {
                return context.suppliers.Where(p => p.Description != null && p.Description.ToLower()
                                            .Contains(SearchTerm.ToLower()))
                                            .OrderBy(x => x.Description)
                                            .ToList();
            }
        }



        public Entities.Supplier GetSupplierById(int id)
        {
            using (var context = new DSContext())
            {
                return context.suppliers.Find(id);

            }
        }

        public void CreateSupplier(Supplier Supplier)
        {
            using (var context = new DSContext())
            {
                context.suppliers.Add(Supplier);
                context.SaveChanges();
            }
        }

        public void UpdateSupplier(Entities.Supplier Supplier)
        {
            using (var context = new DSContext())
            {
                context.Entry(Supplier).State = EntityState.Modified;
                context.SaveChanges();
            }
        }


        public void DeleteSupplier(int ID)
        {
            using (var context = new DSContext())
            {

                var Product = context.suppliers.Find(ID);
                context.suppliers.Remove(Product);
                context.SaveChanges();
            }
        }
    }
}
