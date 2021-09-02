using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml;
using System.Xml.Serialization;


namespace EmilWallin_Assignment7.SavingLoading
{
    class SavePersistent
    {
        //Save Text File Method
        public static void SaveTextFile(string filePath, List<string> saveObject)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fileStream))
                {
                    foreach (string line in saveObject)
                    {
                        sw.WriteLine(line);
                    }
                }
            }
        }


        //Save Binary method
        public static void SaveBinary<T>(string filePath, T saveObject)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(fileStream, saveObject);
            }
        }


        //Save XML method especially for list as types
        public static void SaveXML<T>(string filePath, T saveObject, Type[] types)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T), types);
                xmlSerializer.Serialize(fileStream, saveObject);
            }
        }
        //Overload for not having types (simple, one-object saving to XML)
        public static void SaveXML<T>(string filePath, T saveObject)
        {
            Type[] types = null;
            SaveXML<T>(filePath, saveObject, types);
        }

    }
}
