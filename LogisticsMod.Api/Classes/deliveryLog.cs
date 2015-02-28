using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Codeplex.Data;

namespace LogisticsMod.Api
{
    public class deliveryLog
    {
        private int _userId;
        private int _Id;

        public deliveryLog(Dictionary<string, string> deliveryData)
        {            
            dynamic results = addDeliveryLog(deliveryData);
            _Id = int.Parse(results.insertedId);
        }

        public int getId()
        {
            return _Id;
        }

        private dynamic addDeliveryLog(Dictionary<string, string> data)
        {
            Uri address = new Uri("http://logisticsmod.co.uk/development/api/v1/adddeliverylog");
            string request = LogisticsMod.Api.Helpers.Helpers.webRequest(address, data);
            var arrayJson = DynamicJson.Parse(request);
            return arrayJson;
        }
    }
}
