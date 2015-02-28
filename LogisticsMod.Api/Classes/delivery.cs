using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LogisticsMod.Api;
using Codeplex.Data;

namespace LogisticsMod.Api
{
    public class delivery
    {
        
        private string _cargoId;
        private int _userId;
        private int _Id;

        public delivery(user userId, Dictionary<string, string> deliveryData)
        {
            _userId = userId.userId();

            
            dynamic results = addDelivery(deliveryData);
            _Id = int.Parse(results.insertedId);
        }

        public int getId()
        {
            return _Id;
        }

        private dynamic addDelivery(Dictionary<string, string> data)
        {
            Uri address = new Uri("http://logisticsmod.co.uk/development/api/v1/adddelivery");
            string request = LogisticsMod.Api.Helpers.Helpers.webRequest(address, data);
            var arrayJson = DynamicJson.Parse(request);
            return arrayJson;
        }
    }
}
