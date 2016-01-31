﻿using UnitOfWorkApplication.Model.Entities;
using UnitOfWorkApplication.Repository.Interfaces;
using UnitOfWorkApplication.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitOfWorkApplication.Model.Model;

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

        public RideEstimate GetFare(RideNowModel model, bool isAirportDrop, string sourceCityName)
        {
            var rateModel = this.GetAll().ToList().Where(x => x.CarType.Type.Equals(model.CabType, StringComparison.OrdinalIgnoreCase)).OrderBy(x => x.MaxDistanceValue).OrderBy(x=>x.Position).ToList();
            Double inBoundTotalFare = 0;
            Double outBoundTotalFare = 0;
            Double airportFare = 0;            
            int currentStepDistance = 0;
            RateCard currentRateMaxDistance;
            int rateCardCounter = 0;
            double roundtripFactor = rateModel.Where(x => x.DistanceDisplayText.Equals("RoundTripFactor", StringComparison.OrdinalIgnoreCase)).First().Price;           

            foreach (var step in model.DistanceBreakup.OrderBy(x=>x.Position))
            {
                currentStepDistance = step.Value;
                currentRateMaxDistance = rateModel[rateCardCounter];

                #region BaseCharges
                if (currentStepDistance<=currentRateMaxDistance.MaxDistanceValue)
                {
                    if (step.IsInBound)
                    {
                        inBoundTotalFare += (currentRateMaxDistance.Price / currentRateMaxDistance.MaxDistanceValue) * currentStepDistance;
                    }
                    else
                    {
                        outBoundTotalFare += (currentRateMaxDistance.Price / currentRateMaxDistance.MaxDistanceValue) * currentStepDistance * roundtripFactor;
                    }
                    currentStepDistance = 0;
                    currentRateMaxDistance.MaxDistanceValue -= currentStepDistance;
                }
                else
                {
                    if (step.IsInBound)
                    {
                        inBoundTotalFare += currentRateMaxDistance.Price;
                    }
                    else
                    {
                        outBoundTotalFare += currentRateMaxDistance.Price * roundtripFactor;
                    }
                    currentStepDistance -= currentRateMaxDistance.MaxDistanceValue;
                    currentRateMaxDistance.MaxDistanceValue = 0;
                }                

                if(currentStepDistance==0)
                {
                    continue;
                }
                else
                {
                    rateCardCounter++;
                    currentRateMaxDistance = rateModel[rateCardCounter];
                }
                #endregion

                #region PerKMCharges
                if(rateCardCounter>0)
                {
                    if (currentStepDistance <= currentRateMaxDistance.MaxDistanceValue)
                    {
                        if (step.IsInBound)
                        {
                            inBoundTotalFare += currentRateMaxDistance.Price * currentStepDistance;
                        }
                        else
                        {
                            outBoundTotalFare += currentRateMaxDistance.Price * currentStepDistance * roundtripFactor;
                        }
                        currentStepDistance = 0;
                        currentRateMaxDistance.MaxDistanceValue -= currentStepDistance;
                    }
                    else
                    {
                        if (step.IsInBound)
                        {
                            inBoundTotalFare += currentRateMaxDistance.Price;
                        }
                        else
                        {
                            outBoundTotalFare += currentRateMaxDistance.Price * roundtripFactor;
                        }
                        currentStepDistance -= currentRateMaxDistance.MaxDistanceValue;
                        currentRateMaxDistance.MaxDistanceValue = 0;
                    }

                    if (currentStepDistance == 0)
                    {
                        continue;
                    }
                    else
                    {
                        rateCardCounter++;
                        currentRateMaxDistance = rateModel[rateCardCounter];
                    }
                }
                #endregion
            }
            model.RideEstimate.OneWayBaseFare = inBoundTotalFare+outBoundTotalFare;
            model.RideEstimate.TwoWayBaseFare = inBoundTotalFare * 2+ outBoundTotalFare;
            if (isAirportDrop)
            {
                airportFare = rateModel.Where(x => x.IsAirportDrop && x.DistanceDisplayText.Equals(sourceCityName, StringComparison.OrdinalIgnoreCase)).First().Price;
                if(airportFare<=(inBoundTotalFare+outBoundTotalFare))
                {
                    model.RideEstimate.AirPortFare = airportFare;
                    model.RideEstimate.OneWayBaseFare = 0;
                    model.RideEstimate.TwoWayBaseFare = 0;
                }                
            }
            return model.RideEstimate;
        }
    }
}
