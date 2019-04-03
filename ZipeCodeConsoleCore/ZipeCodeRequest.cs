using ZipeCodeConsoleCore.Repository;
using System.Net;
using System.Threading;

namespace ZipeCodeConsole
{
    public static class ZipeCodeRequest
    {

        public static ZipeCode GetZipeCodeInfo(string zipeCode)
        {
            var jsonZipecode = RequestingJsonZipeCode(zipeCode);
            if (jsonZipecode == null) return null;
            return JsonCepToZipeCodeModel(ref jsonZipecode);
        }

        public static string RequestingJsonZipeCode(string zipeCode)
        {
            try
            {
                Thread.Sleep(5000);
                var accessToken = "Token token=09cadce3330c25fc7de26238042a9df9";
                var url = "http://www.cepaberto.com/api/v2/ceps.json?cep={0}";
                var client = new WebClient { Encoding = System.Text.Encoding.UTF8 };
                client.Headers.Add(HttpRequestHeader.Authorization, accessToken);
                var result = client.DownloadString(string.Format(url, zipeCode));
                return result;
            }
            catch
            {
                return null;
            }
        }

        public static ZipeCode JsonCepToZipeCodeModel(ref string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<ZipeCode>(json);
        }

    }
}