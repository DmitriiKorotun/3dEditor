using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace GraphicsProject
{
    static class GraphicsProjectIO
    {
        public static void WriteToXmlFile<T>(string filePath, T objectToWrite, bool append = false)
        {
            //FileStream writer = new FileStream(filePath, FileMode.Create);
            //DataContractSerializer ser =
            //    new DataContractSerializer(typeof(T));
            //ser.WriteObject(writer, p1);
            //writer.Close();

            var ser = new DataContractSerializer(typeof(T));

            using (XmlWriter xw = XmlWriter.Create(filePath))
            {
                ser.WriteObject(xw, objectToWrite);
            }

            //TextWriter writer = null;
            //try
            //{
            //    var serializer = new XmlSerializer(typeof(T));
            //    writer = new StreamWriter(filePath, append);
            //    serializer.Serialize(writer, objectToWrite);
            //}
            //finally
            //{
            //    if (writer != null)
            //        writer.Close();
            //}
        }

        public static T ReadFromXmlFile<T>(string filePath)
        {
            FileStream fs = new FileStream(filePath,
            FileMode.Open);
            XmlDictionaryReader reader =
                XmlDictionaryReader.CreateTextReader(fs, new XmlDictionaryReaderQuotas());
            DataContractSerializer ser = new DataContractSerializer(typeof(T));

            // Deserialize the data and read it from the instance.
            T deserializedPerson =
                (T)ser.ReadObject(reader, true);
            reader.Close();
            fs.Close();

            return deserializedPerson;
        }
    }
}
