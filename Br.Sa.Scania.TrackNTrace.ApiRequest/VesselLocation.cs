using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Br.Sa.Scania.TrackNTrace.ApiRequest
{
    [Serializable]
    public class VesselLocation
    {
        [Required]
        public int VesselLocationId { get; set; }
        public string  Mmsi { get; set; }
        public string Lon { get; set; }
        public string Lat { get; set; }
        [Required]
        public DateTime SavedHourOnDB { get; set; }

    }
}
