using CargaDados.Model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CargaDados
{
    public class ReadJson
    {
        public void Read(string jsonFilePath)
        {
            string json = File.ReadAllText(jsonFilePath);
            Dictionary<string, object> values = JsonConvert.DeserializeObject<Dictionary<string, object>>(json);

            var newJson = JsonConvert.SerializeObject(values);

            JObject jObject = JObject.Parse(newJson);

            foreach (JProperty parsedProperty in jObject.Properties())
            {
                string propertyName = parsedProperty.Name;
                if (propertyName.Equals("Dados"))
                {
                    var propertyValue = parsedProperty.Value;


                    Console.WriteLine("Name: {0}, Value: {1}", propertyName, propertyValue);
                }
            }
        }
    }
}
