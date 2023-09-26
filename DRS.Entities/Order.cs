using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DRS.Entities
{
    public class Order: BaseEntity
    {
        public int IDBranch { get; set; }
        public string IDUser { get; set; }
        public int IDCustomer { get; set; }
        public int IDBrand { get; set; }
        public int IDSupplier{ get; set; }
        public string Plate { get; set; }
        public string Chassis { get; set; }
        public string Note { get; set; }
    }
}
