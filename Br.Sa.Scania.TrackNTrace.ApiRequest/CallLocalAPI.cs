using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


namespace Br.Sa.Scania.TrackNTrace.ApiRequest
{
    public class CallLocalAPI
    {
        public List<VesselData> GetAPI()
        {
            //Declaração de variaveis
            int valueArray = 0;
            String[] listOfMmsi = new String[100];
            string information;
            string compara;
            int indice;
            int numberOfArray = 0;
            double Num;
            string urlBase = "http://10.251.16.112/TrackNTrace";



            //Cria a requisição HTTP ao API
            HttpWebRequest WebReqBD = (HttpWebRequest)WebRequest.Create(string.Format(urlBase + "/api/VesselLocation/GetAllMmsi"));// GetAllMmsiOnTransport
            //Define o metodo de requisição HTTP
            WebReqBD.Method = "GET";
            //Recebe a resposta do API e coloca em uma variavel WebRespAPI
            HttpWebResponse WebRespBD = (HttpWebResponse)WebReqBD.GetResponse();
            //Imprime no console o Status retornado pela requisição
            Console.WriteLine(WebRespBD.StatusCode + "  " + WebRespBD.Server);


            //
            using (Stream stream = WebRespBD.GetResponseStream())
            {
                // Transforma a leitura para o formato em que um humano entende
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                //Coloca a informação lida(string) na variavel informação
                information = reader.ReadToEnd();

                // Inicio do filtro da informação para o formato certo 

                //compara = "Mmsi";
                //indice = information.IndexOf(compara);
                //string information3 = information.Substring(indice);

                //Divide as informações filtrandos por "Mmsi", ",", ":", "\\" , coloca o limite do Array e seta as opções de Split
               List<VesselData> data = JsonConvert.DeserializeObject<List<VesselData>>(information);


                return data;

            }
        }




    }
}
