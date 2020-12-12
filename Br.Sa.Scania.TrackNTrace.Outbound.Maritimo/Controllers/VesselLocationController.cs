using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.Cors;
using Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Dao;
using Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Controllers
{
    
    [ApiController]
    public class VesselLocationController : ControllerBase
    {
        private readonly VesselLocationDao _context;
        public VesselLocationController(VesselLocationDao context)
        {
            this._context = context;
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]

        [Route("api/[controller]/SetLocations")]
        [HttpGet]
        public ActionResult SetLocations(string data)
        {           
            return Ok(this._context.SetLocations(data));            
        }
        [EnableCors(origins: "*", headers: "*", methods: "*")]

        [Route("api/[controller]/GetAllMmsi")]
        [HttpGet]
        public ActionResult GetAllMmsi()
        {
            try
            {
                List<VesselData> v = this._context.GetAllMmsi();
                string s = JsonConvert.SerializeObject(v);
                return Ok(s);
            }
            catch (Exception)
            {
                return NotFound();
            }
         
        }
        //[Route("api/[controller]/GetAllMmsiOnTransport")]
        //[HttpGet]
        //public ActionResult GetAllMmsiOnTransport()
        //{
            
        //        VesselData[] info = new VesselData[];
               
        //        int n = 0;
        //        List<Maritimos> data = this._context.GetAll();
        //        Maritimos[] checkedData = this._context.FilterOnlyOnTransport(data);
        //        VesselData[] checkedVesselData = this._context.GetVesselInformartion(checkedData);
        //        for (int i = 0; i < checkedVesselData.Length; i++)
        //        {
        //            if (n ==0)
        //            {
        //                info[n] = checkedVesselData[i];
        //                n = n + 1;
        //            }
                    
        //            if (checkedVesselData[i].Name.Contains(info[n].Name) == false)
        //            {
        //                info[n] = checkedVesselData[i];
        //                n = n + 1;
        //            }
        //        }
        //        return Ok(info);
           
           

        //}

    }
}
