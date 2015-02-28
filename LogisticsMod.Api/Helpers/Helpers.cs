using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace LogisticsMod.Api.Helpers
{
    class Helpers
    {
        public static string webRequest(Uri address, Dictionary<string, string> postParams)
        {
            string parameters = "?";
            foreach (KeyValuePair<string, string> postParam in postParams)
            {
                if (parameters == "?")
                {
                    parameters += postParam.Key + "=" + postParam.Value;
                }
                else
                {
                    parameters += "&" + postParam.Key + "=" + postParam.Value;
                }

            }
            address = new Uri(address, parameters);
            Console.WriteLine(address);
            return (new WebClient()).DownloadString(address);
        }
    }
}
