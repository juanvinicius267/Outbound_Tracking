using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models
{
    [Serializable]
    public class TruckOnBoard
    {
        [Required]
        public int TruckOnBoardId { get;  set; }
       
        public int LicensePlate { get; set; }

             
        public string TrackNumber { get; set; }
        public DateTime DataDeGravacao { get; set; }

        public string Latitude { get; set; }

        public string Longitude { get; set; }

        public DateTime DataDaLocalizacao { get; set; }
    }
}
