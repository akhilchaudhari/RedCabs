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

        public DbSet<Car> Car { get; set; }

        public DbSet<CarType> CarType { get; set; }

        public DbSet<Coupon> Coupon { get; set; }

        public DbSet<CouponValueType> CouponValueType { get; set; }

        public DbSet<Driver> Driver { get; set; }

        public DbSet<DriverLocationDetails> DriverLocationDetails { get; set; }

        public DbSet<RideDetails> RideDetails { get; set; }

        public DbSet<RideStatus> RideStatus { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<UserAddress> UserAddress { get; set; }

        public DbSet<UserCoupons> UserCoupons { get; set; }

        public DbSet<UserLocationDetails> UserLocationDetails { get; set; }

    }
}
