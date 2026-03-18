using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Vehicle
    {
        public int VehicleId { get; set; }

        [Required]
        public string Make { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }

        public decimal DailyRate { get; set; }
        public string Status { get; set; } = "Available";

        public int PassengerCapacity { get; set; }
        public string EngineCapacity { get; set; }

    }
}
