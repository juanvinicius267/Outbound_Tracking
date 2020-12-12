using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Br.Sa.Scania.TrackNTrace.TruckOnBoard.Outbound
{
    public class SetLocations
    {
        
        public int TruckOnBoardId { get; set; }

        public int LicensePlate { get; set; }


        public string TrackNumber { get; set; }

        public List<Posicoes> posicoes { get; set; }
        public DateTime DataDeGravacao { get; set; }

        
    }
}
