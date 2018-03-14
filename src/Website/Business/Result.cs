using ConnectorOutlookRecast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Business
{
    public class Result
    {
        public string statut = "Fail";
        public string intent;
        public decimal confidence;
        public List<ResultMerge> ListResultMerge;

        public Result(List<ReponseRecast> recastContact, ReponseWatson reponseWatson)
        {
            List<ResultMerge> listResultMerge = new List<ResultMerge>();
            foreach (var intentWatson in reponseWatson.classes)
            {
                ResultMerge resultMerge = new ResultMerge();

                resultMerge.intent = intentWatson.class_name;


                decimal confiencetotalRecast = 0;
                int elementRecat = 0;
                foreach (ReponseRecast item in recastContact)
                {
                    if (item.intent.slug != "fournir-informations" && item.intent.slug != null )
                    {
                        elementRecat++;
                    }
                    if (item.intent.slug == resultMerge.intent)
                    {
                        confiencetotalRecast = confiencetotalRecast + Convert.ToDecimal(item.intent.confidence.Replace('.', ','));
                    }
                }

                if (elementRecat == 0)
                {
                    resultMerge.confidence = 0;
                    break;
                }


                resultMerge.confidence = confiencetotalRecast / elementRecat;
                resultMerge.confidence = (resultMerge.confidence * (decimal)0.80 + Convert.ToDecimal(intentWatson.confidence.Replace('.', ',')) * (decimal)1.20) / 2;
                //resultMerge.confidence = (resultMerge.confidence  + Convert.ToDecimal(intentWatson.confidence.Replace('.', ','))) / 2;
                listResultMerge.Add(resultMerge);                
            }
            ListResultMerge = listResultMerge;

            decimal maxValue = listResultMerge.Max(x => x.confidence);
            ResultMerge FirstResultMerge = listResultMerge.First(x => x.confidence == maxValue);

            this.intent = FirstResultMerge.intent;
            this.confidence = FirstResultMerge.confidence;
            if (confidence > (decimal) 0.50)
            {
                statut = "Success";
            }
        }
    }

    public class ResultMerge
    {
       public string intent;
       public decimal confidence;
    }
}