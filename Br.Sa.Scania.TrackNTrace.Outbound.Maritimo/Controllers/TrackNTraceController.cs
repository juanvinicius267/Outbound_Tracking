using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Dao;
using Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models;
using Microsoft.AspNetCore.Mvc;

namespace Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Controllers
{
    public class TrackNTraceController : Controller
    {
      
        private readonly TrackNTraceDao _context;
        public TrackNTraceController(TrackNTraceDao context)
        {
            this._context = context;
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("[controller]/Index")]
        public IActionResult Index()
        {
            return View();
        }



        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/[controller]/ReturnAllJustOnTransport")]
        [HttpGet]
        public ActionResult ReturnAllJustOnTransport()
        {
            List<Maritimos> data = this._context.GetAll();
            Maritimos[] checkedData = this._context.FilterOnlyOnTransport(data);
            VesselData[] checkedVesselData = this._context.GetVesselInformartion(checkedData);
            VesselLocation[] vesselLocationsFinal = this._context.GetGeoLocation(checkedVesselData);
            var information = (vesselLocationsFinal, checkedVesselData, checkedData);
            return Ok(information);

        }




        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/[controller]/GetInfoPerVessel")]
        [HttpPost]
        public ActionResult GetInfoPerVessel([FromBody] object _mmsi)
        {
            return Ok(this._context.GetInfoPerVessel(Convert.ToString(_mmsi)));
        }

        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/[controller]/PositionHistory")]
        [HttpPost]
        public ActionResult PositionHistory([FromBody] object _mmsi)
        {
            return Ok(this._context.GetHistoryOfPositions(Convert.ToString(_mmsi)));
        }


    }
}