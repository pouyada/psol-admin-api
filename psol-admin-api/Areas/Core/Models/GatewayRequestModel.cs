using Newtonsoft.Json.Linq;
using PsolAdminApi.Areas.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsolAdminApi.V1.Models
{
    public class GatewayRequestModel
    {
        public string NomeComponente;
        public string NomeOperazione;
        public string Country;
        public string Token;
        public JObject JsonInput;

        public GatewayRequestModel(string component, string operation, string country, JObject requestData, UtilService utilService)
        {
            NomeComponente = component;
            NomeOperazione = operation;
            Country = utilService.GetCountryFromRequest();
            Token = string.Empty;
            JsonInput = requestData;
        }

    }
}
