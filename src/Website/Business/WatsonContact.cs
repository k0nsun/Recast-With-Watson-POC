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
    public class WatsonContact
    {
        public static string WatsonBotConversation = @"https://gateway.watsonplatform.net/conversation/api/v1/workspaces/ad860efb-5e29-4ecf-9c27-ebe3c8cba8f8/message";
        public static string version = @"2017-06-23";
        public static string usernameConversation = @"XXXX";
        public static string passwordConversation = @"XXXX";
        public static string workspace_id = @"XXXX";
        public static string version_date = @"2017-06-23";


        public ReponseWatson sendTexConversation(string Text)
        {
            ReponseWatson respond;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(WatsonBotConversation + "?version=" + version);
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(usernameConversation + ":" + passwordConversation));
            httpWebRequest.Headers.Add("Authorization", "Basic " + encoded);

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";
            
            httpWebRequest.MaximumResponseHeadersLength = int.MaxValue;
            httpWebRequest.MaximumAutomaticRedirections = int.MaxValue;
            httpWebRequest.Timeout = int.MaxValue;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{"+
                            "\"username\": \""+ usernameConversation + "\"," +
                            "\"password\": \"" + passwordConversation + "\"," +
                            "\"workspace_id\": \"" + workspace_id + "\"," +
                            "\"version_date\": \"" + version_date + "\"," +
                            "\"input\" : { \"text\": \"" + Text + "\" }" +
                            "}";
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
                respond = (ReponseWatson)js.Deserialize(objText, typeof(ReponseWatson));
            }
            return respond;
        }


        public static string WatsonBotOrganiser = @"https://gateway.watsonplatform.net/natural-language-classifier/api/v1/classifiers/67a480x203-nlc-67407/classify";
        public static string usernameOrganiser = @"XXXXX";
        public static string passwordOrganiser = @"XXXX";

        public ReponseWatson sendTextOrganised(string Text)
        {
            ReponseWatson respond;
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(WatsonBotOrganiser);
            String encoded = System.Convert.ToBase64String(System.Text.Encoding.GetEncoding("ISO-8859-1").GetBytes(usernameOrganiser + ":" + passwordOrganiser));
            httpWebRequest.Headers.Add("Authorization", "Basic " + encoded);

            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "POST";

            httpWebRequest.MaximumResponseHeadersLength = int.MaxValue;
            httpWebRequest.MaximumAutomaticRedirections = int.MaxValue;
            httpWebRequest.Timeout = int.MaxValue;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                string json = "{\"text\":\"" + Text + "\"}";
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
                respond = (ReponseWatson)js.Deserialize(objText, typeof(ReponseWatson));
            }
            return respond;
        }
    }
}
