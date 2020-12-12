using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Dao;
using System.Web.Http.Cors;

namespace Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Controllers
{
    public class CadastroDeNaviosController : Controller
    {
        private readonly CadastroDeNaviosDao _context;
        public CadastroDeNaviosController(CadastroDeNaviosDao context)
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

        [Route("api/[controller]/SetVessel")]
        [HttpPost]
        public IActionResult SetVessel([FromBody]  object receivedInformation)
        {      
            
            String statusReturn = this._context.SetVesselDataInDb(receivedInformation);
            
            if (statusReturn.Contains("Cadastrado!")== true)
            {
                return Ok(statusReturn);
            }
            else
            {
                return NotFound(statusReturn);
            }
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]

        [Route("api/[controller]/GetVessel")]
        [HttpGet]
        public string GetVessel()
        {
            
            return this._context.GetAllVesselInDB();
        }

    }
}