using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MileageRecorderRemoteWeb.Models
{
    public class MileageClaim
    {
        public int ID { get; set; }

        [Required]
        public string Destination { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        [DisplayName("Reason for trip")]
        public string ReasonForTrip { get; set; }

        [Required]
        public int Miles { get; set; }
        
        [Required]
        [DisplayName("Engine size")]
        public int EngineSize { get; set; }

        public double Amount { get; set; }

        public void calculateAmount()
        {
            double result;
            double lowMileageRate = 0.6;
            double highMileageRate = 0.5;

            if (this.EngineSize < 1000)
            {
                lowMileageRate = 0.3;
                highMileageRate = 0.25;
            }

            if (this.Miles < 100)
            {
                result = this.Miles * lowMileageRate;
            }
            else
            {
                result = this.Miles * highMileageRate;
            }

            this.Amount = result;
        }

    }
}