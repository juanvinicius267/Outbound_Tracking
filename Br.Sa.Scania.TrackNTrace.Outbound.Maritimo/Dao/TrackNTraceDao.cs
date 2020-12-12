using Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Dao
{
    public class TrackNTraceDao
    {
        private readonly OutboundTrackNTraceContext _context;
        public TrackNTraceDao(OutboundTrackNTraceContext context)
        {
            this._context = context;
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

        public VesselData[] GetVesselInformartion( Maritimos[] checkedData2)
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
                        vesselDatas = _context.VesselData.Where(v => v.Name.Contains(a[0]+" "+a[1])).ToList();
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

        public VesselLocation[] GetGeoLocation(VesselData[] checkedVesselData)
        {
            DateTime dateTime = DateTime.Now;
            int filterNumber = checkedVesselData.Length;
            List<VesselLocation> vesselLocations2 = new List<VesselLocation>();
            VesselLocation[] vesselLocations = new VesselLocation[filterNumber];
            int contadorDoFiltro = 0;
            
            for (int i = 0; i < filterNumber; i++)
            {
                try
                {
                    if (checkedVesselData[i] == null)
                    {
                        break;
                    }
                    vesselLocations2 = _context.VesselLocation.Where(v => v.Mmsi.Contains(checkedVesselData[i].Mmsi) &&
                (v.SavedHourOnDB.DayOfYear == dateTime.DayOfYear)).ToList();

                    if (vesselLocations2[0].Mmsi.Contains(checkedVesselData[contadorDoFiltro].Mmsi))
                    {
                        vesselLocations[contadorDoFiltro] = vesselLocations2[0];
                        contadorDoFiltro++;
                    }
                }
                catch (Exception)
                {

                    
                }

                


            }
            string mmsi = "mmsi";
            VesselLocation[] vesselLocationsFinal = new VesselLocation[filterNumber];
            int p = 0;
            for (int i = 0; i < vesselLocations.Length; i++)
            {
                try
                {
                    if (vesselLocations[i].Mmsi.Contains(mmsi) == false)
                    {
                        vesselLocationsFinal[p] = vesselLocations[i];
                        mmsi = vesselLocations[i].Mmsi;
                        p++;
                    }
                }
                catch (Exception)
                {

                    break;
                }
               

            }

            return vesselLocationsFinal;

        }

        public List<VesselLocation> GetHistoryOfPositions(string mmsi)
        {
            List<VesselLocation> data = new List<VesselLocation>();
            data = _context.VesselLocation.Where(v => v.Mmsi.Contains(mmsi)).ToList();
            return data;
        }
        public Maritimos[] GetInfoPerVessel(string _mmsi) {
            DateTime dateTime = DateTime.Now;
            var _mmsiNumber = Convert.ToString(_mmsi);
            List<VesselData> data1 = new List<VesselData>();
            data1 = _context.VesselData
                          .Where(b => b.Mmsi == _mmsiNumber)
                          .ToList();
            string compare = data1[0].Name;
            int contadorDoFiltro = 0;

            var data = _context.Maritimo.ToList();
            Maritimos[] information = new Maritimos[data.Capacity];
            for (int i = 0; i <= data.Capacity; i++)
            {

                try
                {
                    if ((string.IsNullOrEmpty(data[i].Vessel) == false))
                    {
                        if ((data[i].Vessel.Contains(compare) == true))
                        {
                            var dateSplitEtd = data[i].ETDSantos.Split('/', ' ');
                            int dayETDSantos = Convert.ToInt32(dateSplitEtd[0]);
                            int monthETDSantos = Convert.ToInt32(dateSplitEtd[1]);
                            int yearETDSantos = Convert.ToInt32(dateSplitEtd[2]);
                            // DateTime time = Convert.ToDateTime(dayETDSantos + "/" + monthETDSantos + "/" + yearETDSantos + " " + "00:00:00");
                            var dateSplitEta = data[i].ETADestination.Split('/', ' ');
                            int dayETASantos = Convert.ToInt32(dateSplitEta[0]);
                            int monthETASantos = Convert.ToInt32(dateSplitEta[1]);
                            int yearETASantos = Convert.ToInt32(dateSplitEta[2]);
                            // DateTime time2 = Convert.ToDateTime(dayETASantos + "/" + monthETASantos + "/" + yearETASantos + " " + "00:00:00");
                            if ((dateTime.Day >= dayETDSantos) && (monthETDSantos == dateTime.Month) && (yearETDSantos == dateTime.Year))
                            {
                                if ((dayETASantos >= dateTime.Day || dayETASantos <= dateTime.Day) && (monthETASantos == dateTime.Month) && (yearETASantos == dateTime.Year))
                                {
                                    information[contadorDoFiltro] = data[i];
                                    contadorDoFiltro++;
                                }
                            }



                        }

                    }

                }
                catch (Exception)
                {



                }


            }
            Maritimos[] finalInformation = new Maritimos[contadorDoFiltro];
            for (int i = 0; i < contadorDoFiltro; i++)
            {
                finalInformation[i] = information[i];
            }
            return finalInformation;
        }



    }
}
