using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Controllers
{
    public class CadastroDeRemessaController : Controller
    {
        [EnableCors(origins: "*", headers: "*", methods: "*")]

        [Route("[controller]/Index")]
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

    }
}