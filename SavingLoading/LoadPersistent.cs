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
    class LoadPersistent
    {
        //Load Text file method
        public static string[] LoadTextFile(string filePath)
        {
            string[] outStringArray;
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fileStream))
                {
                    List<string> stringList = new List<string>();
                    string line;
                    while((line = sr.ReadLine()) != null)               //Read textlines to list until end
                    {
                        stringList.Add(line);
                    }
                    outStringArray = stringList.ToArray();
                }
            }
            return outStringArray;
        }

       
        //Load Binary method
        public static T LoadBinary<T>(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                return (T)formatter.Deserialize(fileStream);
            }
        }

        //Save XML method
        public static T LoadXML<T>(string filePath)
        {
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                return (T)xmlSerializer.Deserialize(fileStream);
            }
        }
    }
}
