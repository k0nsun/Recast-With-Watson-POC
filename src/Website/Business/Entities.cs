using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication2.Business
{
    public class Entities
    {
        public string name;
        public int number = 0;
        public List<InfoEntity> raw = new List<InfoEntity>();

        public string EntitiesText()
        {
            return name + " (" + number + ") ";
        }

        public void rawAdd(string value, string confidence)
        {
            raw.Add(new InfoEntity()
            {
                value = value,
                confidence = confidence
            });
        }
    }

    public class InfoEntity
    {
        public string value;
        public string confidence;

    }
}