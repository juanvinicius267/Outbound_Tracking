using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Br.Sa.Scania.TrackNTrace.ApiRequest
{
    class Program
    {
        static void Main(string[] args)
        {
           
            while (true)
            {
                //Lê a hora do sistema
                //DateTime Time = DateTime.Now;
                ////Coloca a hora lida em formato de TimeOfDay
                //TimeSpan horaAtual = Time.TimeOfDay;
                ////Recebe o valor da hora de leitura
                //TimeSpan horaDeLeituraMin = TimeSpan.Parse("4:30:00.1");
                //TimeSpan horaDeLeituraMax = TimeSpan.Parse("6:40:00.1");
                //TimeSpan tempoFaltante = horaDeLeituraMin - horaAtual;

                ////Ciclicamento executa a este programa
                //Console.WriteLine("Inicio: " + horaDeLeituraMin + " Fim: " + horaDeLeituraMax + " Atual " + horaAtual);
                //Thread.Sleep(TimeSpan.FromMinutes(0));
                //if (horaAtual >= horaDeLeituraMin && horaAtual <= horaDeLeituraMax)
                //{

                    // Chama o metodo de leitura do API 
                    CallLocalAPI aPI = new CallLocalAPI();
                    List<VesselData> listOfMmsi = aPI.GetAPI();

                    // Chama o metodo de leitura do MarineTraffic
                    CallMarineTraffic callMarineTraffic = new CallMarineTraffic();
                    String[] listCoordinates = callMarineTraffic.GetVesselLocation(listOfMmsi);                    
                    Thread.Sleep(TimeSpan.FromHours(8));
                //}

            }
            
        }
        
        
    }
}
