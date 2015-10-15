using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Model.Entities
{
    public class UnitOfWorkApplicationDBContext : DbContext
    {
        public UnitOfWorkApplicationDBContext()
            : base("Name=DBConnectionString")
        {
            Database.SetInitializer<UnitOfWorkApplicationDBContext>(null);
        }

        public DbSet<Person> Person { get; set; }
        public DbSet<Country> Country { get; set; }
        public DbSet<User> User { get; set; }

    }
}
