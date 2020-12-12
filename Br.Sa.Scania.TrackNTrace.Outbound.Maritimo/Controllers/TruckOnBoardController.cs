using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Dao;
using Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Controllers
{

    [ApiController]
    public class TruckOnBoardController : ControllerBase
    {
        private readonly TruckOnBoardDao _context;

        public TruckOnBoardController(TruckOnBoardDao context)
        {
            this._context = context;
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [Route("api/[controller]/GetAllTable")]
        [HttpGet]
        public ActionResult GetAllTable()

        {
            return Ok(this._context.GetTruckLocation());
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]

        [Route("api/[controller]/SetTruckLocationOnTable")]
        [HttpGet]
        public bool SetTruckLocationOnTable(string data)
        {
            
            return this._context.SetTruckLocation(data);
        }







    }
}