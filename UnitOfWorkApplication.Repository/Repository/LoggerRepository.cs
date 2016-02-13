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
    public class LoggerRepository : GenericRepository<ExceptionLog>, ILoggerRepository
    {
        public LoggerRepository(DbContext context)
              : base(context)
        {

        }
        public void LogException(ExceptionLog ex)
        {
            this.Add(ex);
        }
    }
}
