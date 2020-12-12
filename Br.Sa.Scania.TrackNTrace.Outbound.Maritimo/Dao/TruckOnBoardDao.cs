using Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Dao
{
    public class TruckOnBoardDao
    {
        private readonly OutboundTrackNTraceContext _context;
        public TruckOnBoardDao(OutboundTrackNTraceContext context)
        {
            this._context = context;
        }
        public List<TruckOnBoard> GetTruckLocation()
        {
            List<TruckOnBoard> Data = new List<TruckOnBoard>();             
                Data = this._context.TruckOnBoards.Where(v => v.DataDaLocalizacao.DayOfYear == DateTime.Now.DayOfYear).ToList();
            return Data;
        }

        public bool SetTruckLocation(string info)
        {
            var dataArray = info.Split(new string[] { "," }, 5, StringSplitOptions.RemoveEmptyEntries);
          
            TruckOnBoard data2 = new TruckOnBoard {
                TrackNumber = dataArray[1],
                LicensePlate = Convert.ToInt32(dataArray[0]),
                Latitude = dataArray[2],
                Longitude = dataArray[3],
                DataDeGravacao = DateTime.Now,
                DataDaLocalizacao = Convert.ToDateTime(dataArray[4])
            };
            
            if (info==null)
            {
                return false;
            }
                
            try
            {
                this._context.TruckOnBoards.Add(data2);
                this._context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }         

        }








    }
}
