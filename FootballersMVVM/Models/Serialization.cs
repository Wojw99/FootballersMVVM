using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FootballersMVVM.Models
{
    public static class FootballerSerialization
    {
        public static void SaveTo(string filename, List<FootballerModel> footballers)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<FootballerModel>));

            using (TextWriter textWriter = new StreamWriter(filename))
            {
                serializer.Serialize(textWriter, footballers);
            }
        }

        public static List<FootballerModel> LoadFrom(string filename)
        {
            List<FootballerModel> footballers = new List<FootballerModel>();

            XmlSerializer serializer = new XmlSerializer(typeof(List<FootballerModel>));

            using (TextReader textWriter = new StreamReader(filename))
            {
                footballers = serializer.Deserialize(textWriter) as List<FootballerModel>;
            }

            return footballers;
        }
    }
}
