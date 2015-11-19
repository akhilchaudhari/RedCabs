using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Repository.Repository
{
    public class DriverRepository : GenericRepository<Driver>, IDriverRepository
    {
        public DriverRepository(DbContext context)
              : base(context)
        {

        }

        public override IEnumerable<Driver> GetAll()
        {
            return FindBy(x => x.IsActive == true && x.IsAvailable == 1);
        }

        public Driver GetById(int id)
        {
            return FindBy(x => x.Id == id && x.IsActive == true && x.IsAvailable == 1).FirstOrDefault();
        }

        public List<Driver> GetDriversByCarType(string carType)
        {
            return FindBy(x => x.Car.CarType.Type.Equals(carType, StringComparison.OrdinalIgnoreCase) && x.IsActive==true && x.IsAvailable==1).ToList();
        }    
    }
}
