using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AutoFilledIn
{
    class XmlHelper
    {
        public static string Serialize(ObservableCollection<Student> collection)
        {
            var serializer = new XmlSerializer(typeof(ObservableCollection<Student>));
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, collection);
                return writer.ToString();
            }
        }
        public static ObservableCollection<T> Deserialize<T>(string xmlString)
        {
            var serializer = new XmlSerializer(typeof(ObservableCollection<Student>));
            using (var reader = new StringReader(xmlString))
            {
                return (ObservableCollection<T>)serializer.Deserialize(reader);
            }
        }
    }
}
