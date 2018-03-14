using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace ConnectorOutlookRecast
{
    public class RecastContact
    {

        public static string RecastBot = @"https://run.recast.ai/sunquan-hackathonaxeparis";
        public RecastContact()
        {

        }

        public ReponseRecast sendText(string Text)
        {
            ReponseRecast respond;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(RecastBot);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            httpWebRequest.MaximumResponseHeadersLength = int.MaxValue;
            httpWebRequest.MaximumAutomaticRedirections = int.MaxValue;
            httpWebRequest.Timeout = int.MaxValue;
   
            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"text\":\"" + Text + "\", \"language\":\"fr\"}";
                streamWriter.Write(json);
                streamWriter.Flush();
                streamWriter.Close();
            }

            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                //   reponse = streamReader.ReadToEnd();
                JavaScriptSerializer js = new JavaScriptSerializer();
                var objText = streamReader.ReadToEnd();
                respond = (ReponseRecast)js.Deserialize(objText, typeof(ReponseRecast));
            }
            return respond;
        }
    }
}
