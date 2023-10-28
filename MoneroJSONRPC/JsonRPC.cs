using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http.Headers;

namespace MoneroJSONRPC
{
    public static class JsonRPC
    {
        public static string Url = "http://127.0.0.1:18083/json_rpc";
        public static object InvokeMethod<T>(string method, string aparams)
        {
            var contentBase = "{\"jsonrpc\":\"2.0\",\"id\":\"0\",\"method\":\""+method+ "\"}";
            if (aparams.Length > 0)
            {
                contentBase = "{\"jsonrpc\":\"2.0\",\"id\":\"0\",\"method\":\"" + method + "\", \"params\":"+aparams+"}";
            }
            Console.WriteLine(contentBase);
            HttpClient client  = new HttpClient();
            client.BaseAddress = new Uri(Url);
            client.DefaultRequestHeaders
                  .Accept
                  .Add(new MediaTypeWithQualityHeaderValue("application/json-rpc"));//ACCEPT header

            //client.
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, Url);
            request.Content = new StringContent(contentBase, Encoding.UTF8, "application/json");//CONTENT-TYPE header

            //var response = client.Send(request);

            //var resp = JsonConvert.DeserializeObject<JObject>(response.Content.ToString());
            try
            {
                var resp = client.Send(request);
                using (Stream str = resp.Content.ReadAsStream())
                {
                    using (StreamReader sr = new StreamReader(str))
                    {
                        var respstring = sr.ReadToEnd();
                        JObject jo= JsonConvert.DeserializeObject<JObject>(respstring);
                        var res = jo["result"].ToString();
                        return JsonConvert.DeserializeObject<T>(res);
                    }
                }
            } catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new JObject("Error");
            }

        }
    }
}
