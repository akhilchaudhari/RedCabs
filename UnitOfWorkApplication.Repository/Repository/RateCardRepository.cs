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
    public class RateCardRepository : GenericRepository<RateCard>, IRateCardRepository
    {
        public RateCardRepository(DbContext context)
              : base(context)
        {

        }       
    }
}
