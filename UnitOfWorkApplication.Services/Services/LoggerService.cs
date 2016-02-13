using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Repository.Interfaces;
using UnitOfWorkApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitOfWorkApplication.Services.Services
{

    public class LoggerService : EntityService<ExceptionLog>,ILoggerService
    {
        IUnitOfWork _unitOfWork;
        ILoggerRepository _loggerRepository;

        public LoggerService(IUnitOfWork unitOfWork, ILoggerRepository loggerRepository)
            : base(unitOfWork, loggerRepository)
        {
            _unitOfWork = unitOfWork;
            _loggerRepository = loggerRepository;
        }

        public void Log(ExceptionLog ex)
        {
            try
            {
                _loggerRepository.Add(ex);
                _loggerRepository.Save();
            }
            catch(Exception ex2)
            {

            }
        }
    }
}
