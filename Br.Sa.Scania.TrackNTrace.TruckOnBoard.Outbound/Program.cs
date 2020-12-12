using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Br.Sa.Scania.TrackNTrace.TruckOnBoard.Outbound
{
    class Program
    {
        static void Main(string[] args)
        {
            //Variable declaration
            string urlBase = "https://apimobile.abccargas.com/api/TrackPosition/GetPositonVehicle/0x1C8FD1D22C6C265736BD7BE15EC2B587";
            string jsonString;
            //Requesting the information from ABCCarga API
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format(urlBase));
            //Choosing the HTTP Method 
            WebReq.Method = "GET";
            //Getting response
            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
            //Showing the response
            Console.WriteLine(WebResp.StatusCode);
            Console.WriteLine(WebResp.Server);

            //Modified from your code since the using statement disposes the stream automatically when done
            using (Stream stream = WebResp.GetResponseStream())
            {

                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                jsonString = reader.ReadToEnd();
                Console.WriteLine(jsonString.Count());

                List<SetLocations> info = JsonConvert.DeserializeObject<List<SetLocations>>(jsonString);

                for (int i = 0; i < info.Count; i++)
                {
                    SetLocations info2 = new SetLocations();
                    Posicoes p = new Posicoes();
                    int ilistPosition = info[i].posicoes.Count;
                    info2.LicensePlate = info[i].LicensePlate;
                    info2.TrackNumber = info[i].TrackNumber;

                    string LicensePlate = Convert.ToString(info2.LicensePlate);
                    string TrackNumber = Convert.ToString(info2.TrackNumber);
                    string Latitude = Convert.ToString(info[i].posicoes[ilistPosition - 1].Latitude);
                    string Longitude = Convert.ToString(info[i].posicoes[ilistPosition - 1].Longitude);
                    p.DataHora = info[i].posicoes[ilistPosition - 1].DataHora;
                    string DataHora = Convert.ToString(p.DataHora);
                    if (p.DataHora.Date == DateTime.Now.Date)
                    {
                        //info2.DataDeGravacao = info[i].DataDeGravacao;
                        //Cria a requisição HTTP ao site
                        HttpWebRequest WebReq2 = (HttpWebRequest)WebRequest.Create(string.Format("http://10.251.16.112/TrackNTrace/api/TruckOnBoard/SetTruckLocationOnTable?data=" + LicensePlate + "," + TrackNumber + "," + Latitude + "," + Longitude + "," + DataHora));
                        //Define o metodo de requisição HTTP
                        WebReq2.Method = "GET";
                        //Recebe a resposta do site e coloca em uma variavel WebResp
                        HttpWebResponse WebResp2 = (HttpWebResponse)WebReq2.GetResponse();
                        //Imprime no console o Status retornado pela requisição
                        Console.WriteLine(WebResp2.StatusCode + "  " + WebResp2.Server);
                        using (Stream stream2 = WebResp2.GetResponseStream())   //modified from your code since the using statement disposes the stream automatically when done
                        {
                            // Transforma a leitura para o formato em que um humano entende
                            StreamReader reader2 = new StreamReader(stream2, System.Text.Encoding.UTF8);
                            //Coloca a informação lida(string) na variavel informação

                        }


                    }





                }



            }
            Console.ReadLine();
        }
    }
}