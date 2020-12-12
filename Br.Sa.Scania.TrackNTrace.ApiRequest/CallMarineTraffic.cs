using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Br.Sa.Scania.TrackNTrace.ApiRequest
{
    public class CallMarineTraffic
    {
        public String[] GetVesselLocation(List<VesselData> list)
        {
            //Declaração de variaveis
            List<VesselData> listOfMmsi = list;
            String[] listOfCoordinates = new String[1000];
            string information;
            string compara = "Longitude";
            string compara2 = "Latitude";
            int indice;
            string coordenadas;
            List<VesselData> ErroVessel = new List<VesselData>();
            int iList = 0;
            //Roda a rotina de requisição do HTML pela quantidade de Mmsi passada 
            for (int i = 0; i < listOfMmsi.Count; i++)
            {
                //Tenta requisitar e filtrar as informações
                try
                {

                    //Cria a requisição HTTP ao site
                    HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://www.myshiptracking.com/vessels/mmsi-" + listOfMmsi[i].Mmsi));
                    //Define o metodo de requisição HTTP
                    WebReq.Method = "GET";
                    //Recebe a resposta do site e coloca em uma variavel WebResp
                    Thread.Sleep(TimeSpan.FromSeconds(10));
                    HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
                    //Imprime no console o Status retornado pela requisição
                    Thread.Sleep(TimeSpan.FromSeconds(10));
                    Console.WriteLine(WebResp.StatusCode + "  " + WebResp.Server);
                    Thread.Sleep(TimeSpan.FromSeconds(10));

                    using (Stream stream = WebResp.GetResponseStream())   //modified from your code since the using statement disposes the stream automatically when done
                    {
                        // Transforma a leitura para o formato em que um humano entende
                        StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                        //Coloca a informação lida(string) na variavel informação
                        information = reader.ReadToEnd();
                    }
                    //Procura o valor da Tag compara e retorna do valor de onde ela esta
                    indice = information.IndexOf(compara);
                    //Quebra a string onde esta as coordenadas
                    coordenadas = information.Substring(indice + 1, 47);
                    //Divide as informações filtrandos por "Mmsi", ",", ":", "\\" , coloca o limite do Array e seta as opções de Split
                    var informacoesFiltradas = coordenadas.Split(new string[] { ">", "&deg;", "°", "/ ", "&de" }, 5, StringSplitOptions.RemoveEmptyEntries);

                    //Procura o valor da Tag compara e retorna do valor de onde ela esta
                    indice = information.IndexOf(compara2);
                    //Quebra a string onde esta as coordenadas
                    coordenadas = information.Substring(indice + 1, 47);
                    //Divide as informações filtrandos por "Mmsi", ",", ":", "\\" , coloca o limite do Array e seta as opções de Split
                    var informacoesFiltradas2 = coordenadas.Split(new string[] { ">", "&deg;", "°", "/ ", "&de" }, 5, StringSplitOptions.RemoveEmptyEntries);






                    //Imprimi as coordenadas com o MMSI
                    Console.WriteLine("MMSI: " + i + " Lat: " + informacoesFiltradas2[2] + " Lon: " + informacoesFiltradas[2]);

                    string mmsi = Convert.ToString(listOfMmsi[i].Mmsi);
                    string lat = informacoesFiltradas2[2];
                    string lon = informacoesFiltradas[2];

                    SendingData sendingData = new SendingData();
                    sendingData.EnviaRequisicaoPOST(mmsi, lat, lon);
                    // Acada 5 minutos consulta a localização de um navio.
                    Thread.Sleep(TimeSpan.FromMinutes(1));
                }
                //Pega se existir alguma uma excessão
                catch (Exception)
                {
                    Console.WriteLine("Erro na aquisição dos dados do navio" + listOfMmsi[i].Mmsi);
                    iList++;
                    ErroVessel[iList] = listOfMmsi[i];
                    
                    try
                    {
                        //Cria a requisição HTTP ao site
                        HttpWebRequest WebReq = (HttpWebRequest)WebRequest.Create(string.Format("https://www.myshiptracking.com/vessels/mmsi-" + listOfMmsi[i].Mmsi));
                        //Define o metodo de requisição HTTP
                        WebReq.Method = "GET";
                        //Recebe a resposta do site e coloca em uma variavel WebResp
                        Thread.Sleep(TimeSpan.FromSeconds(20));
                        HttpWebResponse WebResp = (HttpWebResponse)WebReq.GetResponse();
                        //Imprime no console o Status retornado pela requisição
                        Thread.Sleep(TimeSpan.FromSeconds(20));
                        Console.WriteLine(WebResp.StatusCode + "  " + WebResp.Server);
                        Thread.Sleep(TimeSpan.FromSeconds(20));

                        using (Stream stream = WebResp.GetResponseStream())   //modified from your code since the using statement disposes the stream automatically when done
                        {
                            // Transforma a leitura para o formato em que um humano entende
                            StreamReader reader = new StreamReader(stream, System.Text.Encoding.UTF8);
                            //Coloca a informação lida(string) na variavel informação
                            information = reader.ReadToEnd();
                        }
                        //Procura o valor da Tag compara e retorna do valor de onde ela esta
                        indice = information.IndexOf(compara);
                        //Quebra a string onde esta as coordenadas
                        coordenadas = information.Substring(indice + 1, 47);
                        //Divide as informações filtrandos por "Mmsi", ",", ":", "\\" , coloca o limite do Array e seta as opções de Split
                        var informacoesFiltradas = coordenadas.Split(new string[] { ">", "&deg;", "°", "/ ", "&de" }, 5, StringSplitOptions.RemoveEmptyEntries);

                        //Procura o valor da Tag compara e retorna do valor de onde ela esta
                        indice = information.IndexOf(compara2);
                        //Quebra a string onde esta as coordenadas
                        coordenadas = information.Substring(indice + 1, 47);
                        //Divide as informações filtrandos por "Mmsi", ",", ":", "\\" , coloca o limite do Array e seta as opções de Split
                        var informacoesFiltradas2 = coordenadas.Split(new string[] { ">", "&deg;", "°", "/ ", "&de" }, 5, StringSplitOptions.RemoveEmptyEntries);






                        //Imprimi as coordenadas com o MMSI
                        Console.WriteLine("MMSI: " + i + " Lat: " + informacoesFiltradas2[2] + " Lon: " + informacoesFiltradas[2]);

                        string mmsi = Convert.ToString(listOfMmsi[i].Mmsi);
                        string lat = informacoesFiltradas2[2];
                        string lon = informacoesFiltradas[2];

                        SendingData sendingData = new SendingData();
                        sendingData.EnviaRequisicaoPOST(mmsi, lat, lon);
                        // Acada 5 minutos consulta a localização de um navio.
                        Thread.Sleep(TimeSpan.FromMinutes(2));
                    }
                    catch (Exception)
                    {

                        
                    }
                    
                    
                }
            }
            
            return listOfCoordinates;
        }
    }
}
