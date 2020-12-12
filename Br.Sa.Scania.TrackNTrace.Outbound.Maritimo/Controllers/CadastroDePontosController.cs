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
    public class CadastroDePontosController : Controller
    {
        private readonly CadastroDePontosDao _context;
        public CadastroDePontosController(CadastroDePontosDao context)
        {
            this._context = context;
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]

        [Route("[controller]/Index")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]

        [Route("api/[controller]/SetPortDestination")]
        [HttpPost]
        public ActionResult SetPortDestination([FromBody] object _portInfo)
        {
            string status = this._context.SetPortDestinationInDB(_portInfo);
            if (status == "Cadastrado")
            {
                return Ok();
            }
            return NotFound();
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]

        [Route("api/[controller]/ReturnPortinDB")]
        [HttpGet]
        public ActionResult ReturnPortinDB()
        {
            List<PortLocation> data = this._context.GetAll();
            return Ok(data);
        }

    }
}