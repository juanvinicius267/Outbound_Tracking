using Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Dao
{
    public class CadastroDePontosDao
    {
        public VesselData vesselData;
        public VesselLocation vesselLocation;
        private readonly OutboundTrackNTraceContext _context;
        public CadastroDePontosDao(OutboundTrackNTraceContext context)
        {
            this._context = context;
        }

        public string SetPortDestinationInDB(object _portInfo)
        {
            try
            {
                string conversion = Convert.ToString(_portInfo);
                string[] arrayOfInfo = conversion.Split(new string[] { ":", "\"" }, 45, StringSplitOptions.RemoveEmptyEntries);


                var portinfos = new PortLocation()
                {
                    portName = arrayOfInfo[3],
                    Longitude = arrayOfInfo[7],
                    Latitude = arrayOfInfo[11],
                    Country = arrayOfInfo[15]

                };

                _context.PortLocation.Add(portinfos);
                _context.SaveChanges();
                return "Cadastrado";
            }
            catch (Exception)
            {

                return "Não Cadastrado";
            }

        }

        public List<PortLocation> GetAll()
        {
            List<PortLocation> data = new List<PortLocation>();
            data = _context.PortLocation.ToList();

            return data;
        }






    }
}
