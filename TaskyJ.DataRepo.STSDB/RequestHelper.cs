using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace TaskyJ.DataRepo
{
    public class RequestHelper
    {

        public static string sendRequest(string url, string data = null)
        {
            if (data == null)
                return Task.Run(() => sendRequestAsyncGET(url)).Result;
            else
                return Task.Run(() => sendRequestAsyncPOST(url, data)).Result;
        }

        public static async Task<string> sendRequestAsyncGET(string url)
        {
            try
            {
                WebRequest rqst = WebRequest.Create(url);
                HttpWebRequest rqstw = (HttpWebRequest)rqst;
                rqst.Credentials = CredentialCache.DefaultCredentials;

                //ignore Certificate validation failures (aka untrusted certificate + certificate chains)
                //ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

                rqst.Method = "GET";
                WebResponse wrsp = await rqst.GetResponseAsync();
                StreamReader rsps = new StreamReader(wrsp.GetResponseStream());
                string strRsps = rsps.ReadToEnd();
                return strRsps;
            }
            catch (Exception ex)
            {
                return "error " + ex.Message;
            }
        }

        public static async Task<string> sendRequestAsyncPOST(string url, string data)
        {
            try
            {
                WebRequest rqst = WebRequest.Create(url);
                HttpWebRequest rqstw = (HttpWebRequest)rqst;
                rqst.Credentials = CredentialCache.DefaultCredentials;

                // Ignore Certificate validation failures (aka untrusted certificate + certificate chains)
                //ServicePointManager.ServerCertificateValidationCallback = ((sender, certificate, chain, sslPolicyErrors) => true);

                rqst.Method = "POST";
                if (data != null)
                {
                    //rqst.ContentType = "application/xml";
                    rqst.ContentType = "application/x-www-form-urlencoded";

                    byte[] byteData = Encoding.UTF8.GetBytes(data);

                    using (Stream postStream = await rqst.GetRequestStreamAsync())
                    {
                        postStream.Write(byteData, 0, byteData.Length);
                    }
                }

                WebResponse wrsp = await rqst.GetResponseAsync();
                StreamReader rsps = new StreamReader(wrsp.GetResponseStream());
                string strRsps = rsps.ReadToEnd();
                return strRsps;
            }
            catch (Exception ex)
            {
                return "error " + ex.Message;
            }
        }
    }
}
