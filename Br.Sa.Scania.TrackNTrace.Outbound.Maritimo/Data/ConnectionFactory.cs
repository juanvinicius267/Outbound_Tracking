using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Br.Sa.Scania.TrackNTrace.Outbound.Maritimo.Data
{
    public class ConnectionFactory
    {
       
            public static SqlConnection CriaConexãoAberta()
            {
                var builder = new ConfigurationBuilder()
                              .SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                              .AddEnvironmentVariables();
                IConfiguration configuration = builder.Build();
                string stringConexão = configuration.GetConnectionString("OutboundTrackNTrace");
                SqlConnection conexao = new SqlConnection(stringConexão);
                conexao.Open();

                return conexao;
            }


    }
}
