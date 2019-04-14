using ZipeCodeConsole.Repository;
using System.Net;
using System.Web.Script.Serialization;

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
                //http://viacep.com.br/ws/{0}/json
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
            return new JavaScriptSerializer().Deserialize<ZipeCode>(json);
        }

    }
}
