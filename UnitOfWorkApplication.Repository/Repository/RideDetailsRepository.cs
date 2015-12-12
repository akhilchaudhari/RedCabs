using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Repository.Interfaces;

namespace UnitOfWorkApplication.Repository.Repository
{ 
    public class RideDetailsRepository : GenericRepository<RideDetails>, IRideDetailsRepository
    {
        public RideDetailsRepository(DbContext context)
              : base(context)
        {

        }
        public RideDetails GetById(int id)
        {
            return FindBy(x => x.Id == id).FirstOrDefault();
        }

        public bool BookRide(RideDetails model)
        {
            bool result = false;

            try
            {
                this.Add(model);
                this.Save();
                result = true;
            }
            catch
            {

            }
            return result;
        }
    }
}
