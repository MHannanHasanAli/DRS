using Microsoft.AspNet.Identity.EntityFramework;
using UModule.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UModule.Database
{
    public class DSContext :IdentityDbContext<User>,IDisposable
    {
        public DSContext() : base("DSConnectionStrings")
        {

        }

        public static DSContext Create()
        {
            return new DSContext();
        }




    }
}
