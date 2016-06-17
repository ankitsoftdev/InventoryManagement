using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml;
using System.Xml.Serialization;
namespace HotelManagementErp_Main.Helper
{
    public class Notification
    {
    

        //public static List<T> Deserialize<T>(string xml,string fname)
        //{
        //    var serializer = new XmlSerializer(typeof(List<T>));
        //    List<T> result;

        //    using (StreamReader reader = new StreamReader(xml))
        //    {
        //        result = (List<T>)serializer.Deserialize(new StringReader(xml));
        //    }
        //    return result ;

        //    //var xs = new XmlSerializer(typeof(T));
        //    //return (T)xs.Deserialize(new StringReader(xml));
        //}
       
        //public static String Serialize1<T>(T obj)
        //{
        //    var serializer = new XmlSerializer(typeof(T));
        //    String S = "";
        //    using ( XmlWriter strm =   XmlWriter.Create(new StreamWriter("D:\\BokedroomDetails.txt")))
        //    {
        //        serializer.Serialize(strm, obj);
             
        //    } 
           
          
        //    return S;
        //}

        //public static string Serialize2<T>(T obj,string filename)
        //{
        //    var serializer = new XmlSerializer(typeof(T));
        //    String S = "";
          
        //    if (File.Exists(filename))
        //    {
        //        using (StreamWriter strm = File.AppendText(filename))
        //        {
        //            strm.WriteLine(obj);
        //            serializer.Serialize(strm, obj);
        //            return S;
        //        }               
        //    }
        //    else
        //    {
        //        using (XmlWriter strm = XmlWriter.Create(new StreamWriter(filename))) 
        //        {
        //            serializer.Serialize(strm, obj);
        //            return S;
        //        } 
        //    } 
        //}
        public static string Serialize<T>(T obj, string path,bool IsAppend)
        {
            if (File.Exists(path))
                File.Delete(path);

            XmlSerializer serializer = new XmlSerializer(obj.GetType()); using (StreamWriter writer = new StreamWriter(path)) { serializer.Serialize(writer.BaseStream, obj); }
            return "";
         
        }
        public static T Deserialize<T>(string path)
        {
            T city;
            XmlSerializer serializer = new XmlSerializer(typeof(T)); using (StreamReader reader = new StreamReader(path)) { object deserialized = serializer.Deserialize(reader.BaseStream);
            var s = (T)deserialized;
            return s;
            }

            return city;
        }
        public static T Deserialize111<T>(string path)
        {
            T city;
            XmlSerializer serializer = new XmlSerializer(typeof(T));
           
                using (StreamReader reader = new StreamReader(path))
                {
                    object deserialized = serializer.Deserialize(reader.BaseStream);
                    var s = (T)deserialized;
                    return s;
                }
                return city;
           
            
        }
    }
}