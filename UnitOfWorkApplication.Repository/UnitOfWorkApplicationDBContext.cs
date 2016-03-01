using System.Data.Entity;
using UnitOfWorkApplication.Model.Entities;

namespace UnitOfWorkApplication.Model.Repository
{
    public class UnitOfWorkApplicationDBContext : DbContext
    {
        public UnitOfWorkApplicationDBContext()
            : base("DBConnectionString")
        {
        }

        public DbSet<Car> Car { get; set; }

        public DbSet<CarType> CarType { get; set; }

        public DbSet<Coupon> Coupon { get; set; }

        public DbSet<CouponValueType> CouponValueType { get; set; }

        public DbSet<Driver> Driver { get; set; }

        public DbSet<DriverLocationDetails> DriverLocationDetails { get; set; }

        public DbSet<RideDetails> RideDetails { get; set; }

        public DbSet<User> User { get; set; }

        public DbSet<UserAddress> UserAddress { get; set; }

        public DbSet<UserCoupons> UserCoupons { get; set; }

        public DbSet<UserLocationDetails> UserLocationDetails { get; set; }

        public DbSet<RateCard> RateCard { get; set; }

        public DbSet<ExceptionLog> ExceptionLog { get; set; }

    }
}
