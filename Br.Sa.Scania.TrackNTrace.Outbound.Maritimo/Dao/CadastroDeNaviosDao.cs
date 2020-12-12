
using Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.ModelBinding;

namespace Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Dao
{
    public class CadastroDeNaviosDao
    {
        private readonly OutboundTrackNTraceContext _context;
        public CadastroDeNaviosDao(OutboundTrackNTraceContext context)
        {
            this._context = context;
        }

        public String SetVesselDataInDb(object receivedInformation)
        {
            // Verifica se o dado não veio vazio
            if (receivedInformation != null)
            {
                try
                {
                    // Converte a informação recebida de Objecto para String
                    string information = Convert.ToString(receivedInformation);
                    // Quebra a String e coloca em um array de strings
                    string[] informationSplit = information.Split(new string[] { ":", "\"" }, 45, StringSplitOptions.RemoveEmptyEntries);
                    //Cria um modelo de vessel e coloca as informações obtidas em variaveis
                    VesselData vessel = new VesselData();
                    vessel.Name = informationSplit[3];
                    vessel.Imo = informationSplit[7];
                    vessel.Mmsi = informationSplit[11];
                    vessel.Indicative = informationSplit[15];
                    vessel.Flag = informationSplit[19];
                    vessel.AisVesselType = informationSplit[23];
                    vessel.Capacity = informationSplit[27];
                    vessel.VesselSize = informationSplit[31];
                    vessel.Year = informationSplit[35];
                    vessel.State = informationSplit[39];
                    //Grava no banco de dados a variavel "vessel" e grava
                    _context.VesselData.Add(vessel);
                    _context.SaveChanges();
                    //retorna a mensagem cadastrado quando dá tudo ok
                    return "Cadastrado!";
                }
                catch (Exception)
                {

                    return "Não cadastrado!";
                }
              
            }
            else
            {
                // retorna "Não Cadastrado quando existir algum problema"
                return "Não cadastrado!";
            }
           
        }
        public String GetAllVesselInDB()
        {
            return JsonConvert.SerializeObject(_context.VesselData.ToList());
        }




    }
}
