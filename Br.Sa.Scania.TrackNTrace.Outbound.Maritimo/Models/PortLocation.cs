using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models
{
    [Serializable]
    public class PortLocation
    {
        [Required]
        public int id { get; set; }
        public string  portName { get; set; }

        public string Longitude { get; set; }

        public string Latitude { get; set; }

        public string Country { get; set; }
    }
}
