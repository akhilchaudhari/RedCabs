﻿using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnitOfWorkApplication.Model.Entities
{
    public class CabDuration
    {
        /// <summary>
        /// Gets or sets Car type
        /// </summary>
        public string CarType { get; set; }

        /// <summary>
        /// Gets or sets the duration value
        /// </summary>
        public string DurationText { get; set; }

        /// <summary>
        /// Gets or sets the duration text
        /// </summary>
        public int DurationValue { get; set; }

        /// <summary>
        /// Gets the list of drivers
        /// </summary>
        public List<string> Drivers { get; set; }

        public CabDuration()
        {
        }    

        public CabDuration(string carType, string durationText, int durationValue)
        {
            this.CarType = carType;
            this.DurationText = durationText;
            this.DurationValue = durationValue;
            this.Drivers = new List<string>();
        }
    }
}