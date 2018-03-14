using ConnectorOutlookRecast;
using OpenPop.Mime;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication2.Business;

namespace WebApplication2.Models
{
    public class IndexModel
    {
        public string SubjectEmail { get; set; }
        public string ContentEmail { get; set; }
        public string SendFrom { get; set; }


        public List<ReponseRecast> ReponseRecast { get; set; }

        public ReponseWatson ReponseWatson { get; set; }

        public ReponseWatson ReponseWatsonClassifier { get; set; }

        public Result Result { get; set; }

        public List<Business.Entities> Entities { get; set; }

        public static IndexModel Create()
        {

            Email email = new Email();
            var count = email.emailBox.GetMessageCount();
            Message message = email.emailBox.GetMessage(count);


            string subjectEmail = message.Headers.Subject;
            string contentEmail = message.MessagePart.MessageParts[0].GetBodyAsText();
            string sendfrom = message.Headers.To[0].Address;
            List<ReponseRecast> reponseRecast = new List<ConnectorOutlookRecast.ReponseRecast>();

            // --------------------------TRAITEMENT RECAST
            // découpage en phrase pour recast

            string textClean = cleanText(contentEmail);
            string[] textSplit = splitSentence(textClean);

            foreach (var item in textSplit)
            {
                RecastContact bot = new RecastContact();
                ReponseRecast reponse = bot.sendText(item);
                reponse.sentence = item;
                reponseRecast.Add(reponse);
            }


            List<Business.Entities> entities = new List<Business.Entities>();

            List<entity> card = new List<entity>();
            List<entity> location = new List<entity>();
            List<entity> adresse = new List<entity>();
            List<entity> contrat = new List<entity>();
            List<entity> numero_de_secu = new List<entity>();
            List<entity> numero_carte_adherent = new List<entity>();
            foreach (var item in reponseRecast)
            {
                card = card.Concat(item.entities.card)
                                    .ToList();
                location = location.Concat(item.entities.location)
                                     .ToList();
                adresse = adresse.Concat(item.entities.adresse)
                                     .ToList();
                contrat = contrat.Concat(item.entities.contrat)
                                     .ToList();
                numero_de_secu = numero_de_secu.Concat(item.entities.numero_de_secu)
                                     .ToList();
                numero_carte_adherent = numero_carte_adherent.Concat(item.entities.numero_carte_adherent)
                                     .ToList();
            }
            var Card = new Business.Entities() { name = "Card", number = card.Count };

            foreach (var item in card)
            {
                Card.rawAdd(item.raw, item.confidence);
            }
            entities.Add(Card);

            var Location = new Business.Entities() { name = "Location", number = location.Count };
            foreach (var item in location)
            {
                Location.rawAdd(item.raw, item.confidence);
            }
            entities.Add(Location);


            var Adresse =  new Business.Entities() { name = "adresse", number = adresse.Count };
            foreach (var item in adresse)
            {
                Adresse.rawAdd(item.raw, item.confidence);
            }
            entities.Add(Adresse);

            var Contrat = new Business.Entities() { name = "contrat", number = contrat.Count };
            foreach (var item in contrat)
            {
                Contrat.rawAdd(item.raw, item.confidence);
            }
            entities.Add(Contrat);

            var Numero_de_secu  = new Business.Entities() { name = "numero_de_secu", number = numero_de_secu.Count };
            foreach (var item in numero_de_secu)
            {
                Numero_de_secu.rawAdd(item.raw, item.confidence);
            }
            entities.Add(Numero_de_secu);

            var Numero_carte_adherent = new Business.Entities() { name = "numero_carte_adherent", number = numero_carte_adherent.Count };
            foreach (var item in numero_carte_adherent)
            {
                Numero_carte_adherent.rawAdd(item.raw, item.confidence);
            }
            entities.Add(Numero_carte_adherent);


            // --------------------------TRAITEMENT WATSON
            //WatsonContact watson = new WatsonContact();
            //ReponseWatson reponseWatsonConvers =  watson.sendTexConversation(textClean);

            WatsonContact watttt = new WatsonContact();
            ReponseWatson reponseWatsonClassifier = watttt.sendTextOrganised(textClean);

            // calcul Final
            Result result = new Result(reponseRecast, reponseWatsonClassifier);


            return new IndexModel
            {
                SubjectEmail = subjectEmail,
                SendFrom = sendfrom,
                ContentEmail = contentEmail,
                ReponseRecast = reponseRecast,
                //  ReponseWatson = reponseWatsonConvers,
                ReponseWatsonClassifier = reponseWatsonClassifier,
                Result = result,
                Entities = entities
            };
        }

        private static string cleanText(string Text)
        {
            if (Text.ToLower().IndexOf("cordialement") > 0)
            {
                Text = Text.Remove(Text.ToLower().IndexOf("cordialement"));
            }

            if (Text.ToLower().IndexOf("_______________") > 0)
            {
                Text = Text.Remove(Text.ToLower().IndexOf("_______________"));
            }

            Text = Text.Replace("\n", "");
            Text = Text.Replace("\r", "");
            Text = Text.Replace("\"", "\\\"");
            return Text;
        }

        private static string[] splitSentence(string Text)
        {
            Text = Text.Replace(";", ".");
            Text = Text.Replace("!", ".");
            Text = Text.Replace("?", ".");
            return Text.Split('.');

        }

        public static string convertDecimalView(string Text)
        {
            if (string.IsNullOrEmpty(Text))
            {
                return "0 %";
            }
            decimal value = Convert.ToDecimal(Text.Replace('.', ','));
            return value.ToString("0.## %");
        }

        public static string convertDecimalView(decimal value)
        {
            return value.ToString("0.## %");
        }
    }
}