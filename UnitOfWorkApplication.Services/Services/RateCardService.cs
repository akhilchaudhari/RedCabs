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

    public class RateCardService : EntityService<RateCard>, IRateCardService
    {
        IUnitOfWork _unitOfWork;
        IRateCardRepository _rateCardRepository;

        public RateCardService(IUnitOfWork unitOfWork, IRateCardRepository rateCardRepository)
            : base(unitOfWork, rateCardRepository)
        {
            _unitOfWork = unitOfWork;
            _rateCardRepository = rateCardRepository;
        }
    }
}
