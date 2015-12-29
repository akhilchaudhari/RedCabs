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

        public Double GetFare(Double distance, String cabType, bool isAirportDrop, string sourceCityName)
        {
            var rateModel = this.GetAll().ToList().Where(x => x.CarType.Type.Equals(cabType, StringComparison.OrdinalIgnoreCase)).OrderBy(x => x.MaxDistanceValue).ToList();
            Double totalFare = 0;
            while (distance > 0)
            {
                foreach (var rateCategory in rateModel.Where(x => !x.IsAirportDrop))
                {
                    if (!(rateCategory.IsAirportDrop || rateCategory.IsPerKmPrice))
                    {
                        totalFare = totalFare + rateCategory.Price;
                        distance = distance - rateCategory.MaxDistanceValue;
                    }
                    else
                    {
                        if (distance >= rateCategory.MaxDistanceValue)
                        {
                            totalFare = totalFare + rateCategory.Price * rateCategory.MaxDistanceValue;
                            distance = distance - rateCategory.MaxDistanceValue;
                        }
                        else
                        {
                            totalFare = totalFare + rateCategory.Price * distance;
                            distance = 0;
                        }
                    }
                }
            }

            if (isAirportDrop)
            {
                var airportFare = rateModel.Where(x => x.IsAirportDrop && x.DistanceDisplayText.Equals(sourceCityName, StringComparison.OrdinalIgnoreCase)).FirstOrDefault().Price;

                if (airportFare < totalFare)
                {
                    totalFare = airportFare;
                }
            }

            return totalFare;
        }
    }
}
