using Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Dao
{
    public class VesselLocationDao
    {
        private readonly OutboundTrackNTraceContext _context;
        public VesselLocationDao(OutboundTrackNTraceContext context)
        {
            this._context = context;
        }
        public bool SetLocations(string data)
        {
            if (data != null)
            {
                try
                {
                    var dataArray = data.Split(new string[] { "," }, 5, StringSplitOptions.RemoveEmptyEntries);
                    VesselLocation vessel = new VesselLocation
                    {
                        Mmsi = dataArray[0],
                        Lat = dataArray[1],
                        Lon = dataArray[2],
                        SavedHourOnDB = DateTime.Now
                    };
                    this._context.VesselLocation.Add(vessel);
                    this._context.SaveChanges();
                    return true;
                }
                catch (Exception)
                {

                    return false;
                }
            }
            return false;
        }

        public List<VesselData> GetAllMmsi()
        {
            List<VesselData> v = this._context.VesselData.ToList();
            return v;
        }
        public List<Maritimos> GetAll()
        {
            //Carrega todas as informações do banco de dados da tabela de maritimo.
            List<Maritimos> data = new List<Maritimos>();
            data = _context.Maritimo
                          .ToList();
            return data;
        }

        public Maritimos[] FilterOnlyOnTransport(List<Maritimos> data)
        {
            int contadorDoFiltro = 0;
            DateTime dateTime = DateTime.Now;

            Maritimos[] checkedData = new Maritimos[data.Count];
            //Filtra as informações lida do banco de dados e grava na memoria checkedData apenas as que estão em transporte
            for (int i = 0; i < data.Count; i++)
            {
                try
                {
                    DateTime ETD = Convert.ToDateTime(data[i].ETDSantos);
                    DateTime ETA = Convert.ToDateTime(data[i].ETADestination);
                    if (data[i].ETA2Destination == null || data[i].ETA2Destination == "")
                    {
                        if ((dateTime >= ETD) && ((dateTime <= ETA) || (dateTime == ETA)))
                        {
                            checkedData[contadorDoFiltro] = data[i];
                            contadorDoFiltro++;
                        }

                    }
                    else
                    {
                        DateTime ETA2 = Convert.ToDateTime(data[i].ETA2Destination);
                        if ((dateTime >= ETD) && ((dateTime <= ETA) || (dateTime == ETA) || (dateTime <= ETA2) || (dateTime == ETA2)))
                        {
                            checkedData[contadorDoFiltro] = data[i];
                            contadorDoFiltro++;
                        }
                    }
                }
                catch (Exception)
                {
                }
            }
            Maritimos[] checkedData2 = new Maritimos[contadorDoFiltro];
            int filterNumber = 0;
            //Filtra as informações 
            for (int i = 0; i < contadorDoFiltro; i++)
            {
                if (checkedData[i] != null)
                {
                    checkedData2[filterNumber] = checkedData[i];
                    filterNumber++;
                }

            }








            return checkedData2;
        }

        public VesselData[] GetVesselInformartion(Maritimos[] checkedData2)
        {

            int filterNumber = checkedData2.Length;
            List<VesselData> vesselDatas = new List<VesselData>();
            List<VesselData> vesselDatas2 = new List<VesselData>();
            VesselData[] checkedVesselData = new VesselData[filterNumber];
            int contadorDoFiltro = 0;


            for (int i = 0; i < filterNumber; i++)
            {
                try
                {

                    var a = checkedData2[i].Vessel.Split(" ");


                    if (a[0] == "" || a[0] == " ")
                    {

                    }
                    else if (a.Length == 1)
                    {
                        vesselDatas = _context.VesselData.Where(v => v.Name.Contains(a[0])).ToList();
                        if (vesselDatas[0].Name.Contains(a[0]) == true)
                        {
                            checkedVesselData[contadorDoFiltro] = vesselDatas[0];
                            contadorDoFiltro++;
                        }
                    }
                    else if (a.Length == 2)
                    {
                        vesselDatas = _context.VesselData.Where(v => v.Name.Contains(a[0] + " " + a[1])).ToList();
                        if (vesselDatas[0].Name.Contains(a[0]) == true)
                        {
                            checkedVesselData[contadorDoFiltro] = vesselDatas[0];
                            contadorDoFiltro++;
                        }
                    }

                    else if (a.Length == 3)
                    {
                        vesselDatas = _context.VesselData.Where(v => v.Name.Contains(a[0] + " " + a[1])).ToList();

                        if (vesselDatas[0].Name.Contains(a[0]) == true)
                        {


                            checkedVesselData[contadorDoFiltro] = vesselDatas[0];
                            contadorDoFiltro++;
                        }
                    }
                    else if (a.Length == 4)
                    {
                        string compare = Convert.ToString(a[0] + " " + a[1]);
                        vesselDatas = _context.VesselData.Where(v => v.Name.Contains(compare)).ToList();

                        if (vesselDatas[0].Name.Contains(compare) == true)
                        {
                            checkedVesselData[contadorDoFiltro] = vesselDatas[0];
                            contadorDoFiltro++;
                        }
                    }

                }
                catch (Exception)
                {
                    try
                    {
                        var a = checkedData2[i].Vessel.Split(" ");
                        vesselDatas = _context.VesselData.Where(v => v.Name == a[0]).ToList();
                        if (vesselDatas[0].Name.Contains(a[0]) == true)
                        {
                            checkedVesselData[contadorDoFiltro] = vesselDatas[0];
                            contadorDoFiltro++;
                        }
                    }
                    catch (Exception)
                    {


                    }

                }



            }
            return checkedVesselData;
        }

    }
}
