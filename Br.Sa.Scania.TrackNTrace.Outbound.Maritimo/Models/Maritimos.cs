using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models
{
    [Serializable]
    public class Maritimos
    {
        public int id { get; set; }

        public string BatchId { get; set; }

        public string PopId { get; set; }

        public string Chassis { get; set; }

        public string CustomerOrder { get; set; }

        public string PartPeriod { get; set; }

        public string Type { get; set; }

        public string Market { get; set; }

        public string Model { get; set; }

        public string PDD { get; set; }

        public string PlanPacking { get; set; }

        public string PlanDelivery { get; set; }

        public string Liner { get; set; }

        public string PortDestination { get; set; }

        public string InttraNumber { get; set; }

        public string Booking { get; set; }

        public string Terminal { get; set; }

        public string Container40 { get; set; }

        public string Container20 { get; set; }

        public string Vessel { get; set; }

        public string LastDateOutSLA { get; set; }

        public string ETDSantos { get; set; }

        public string ETD2Santos { get; set; }

        public string ATDSantos { get; set; }

        public string ETADestination { get; set; }

        public string ETA2Destination { get; set; }

        public string ATADestination { get; set; }
    }
}
