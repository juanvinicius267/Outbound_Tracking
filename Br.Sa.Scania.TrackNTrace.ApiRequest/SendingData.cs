using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;


using Newtonsoft.Json;

namespace Br.Sa.Scania.TrackNTrace.ApiRequest
{
    public class SendingData
    {
        public void EnviaRequisicaoPOST(string mmsi, string lat, string lon)
        {
           
            string information;
            string urlBase = "http://10.251.16.112/TrackNTrace";
            //Cria a requisição HTTP ao site
            HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format(urlBase+"/api/VesselLocation/SetLocations?data=" + mmsi+","+lat+","+lon));
            //Define o metodo de requisição HTTP
            WebReq.Method = "GET";
            //Recebe a resposta do site e coloca em uma variavel WebResp
            HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
            //Imprime no console o Status retornado pela requisição
            Console.WriteLine(WebResp.StatusCode + "  " + WebResp.Server);
            using (Stream stream = WebResp.GetResponseStream())   //modified from your code since the using statement disposes the stream automatically when done
            {
                // Transforma a leitura para o formato em que um humano entende
                StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                //Coloca a informação lida(string) na variavel informação
                information = reader.ReadToEnd();
            }            
        }
    }
}

