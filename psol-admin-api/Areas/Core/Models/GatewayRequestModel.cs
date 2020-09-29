using Newtonsoft.Json.Linq;

namespace PsolAdminApi.Areas.Core.Models
{
    public class GatewayRequestModel
    {
        public string NomeComponente;
        public string NomeOperazione;
        public string Token;
        public JObject JsonInput;
        public string Country { get; set; }

        /// <summary>
        /// Used to determine the main key of response's body object. this is because each gateway method has a different key
        /// </summary>
        public string DataField { get; set; }

        /// <summary>
        /// Used to determine if the gateway request must return a single entity or a collection
        /// Default = true
        /// </summary>
        public bool FetchEntity { get; set; }

        public GatewayRequestModel(string component, string operation, JObject requestData)
        {
            NomeComponente = component;
            NomeOperazione = operation;
            Token = string.Empty;
            JsonInput = requestData;
            FetchEntity = true;
        }

    }
}
