using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System;
using System.Text;

public class XMLUtility : MonoBehaviour
{
    public static void AddItemToXml(string sPath, string sName, string sItemPath, string sItemName, string MD5)
    {
        string path = string.Format("{0}/{1}.xml", sPath, sName);
        if (!File.Exists(path))
        {
            CreatXml(sPath, sName);
        }
        AddNodeToXml(sPath, sName, sItemPath, sItemName, MD5);
    }

    public static void CreatXml(string sPath, string sName)
    {
        XmlDocument doc = new XmlDocument();
        XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
        doc.AppendChild(dec);
        XmlElement root = doc.CreateElement("Items");
        root.SetAttribute("Version", GlobalConst.VERSION_NUMBER.ToString());
        doc.AppendChild(root);
        doc.Save(string.Format("{0}/{1}.xml", sPath, sName));
    }


    public static void AddNodeToXml(string sPath, string sName, string itempath, string itemname, string MD5)
    {
        string path = string.Format("{0}/{1}.xml", sPath, sName);
        XmlDocument doc = new XmlDocument();
        doc.Load(path);
        XmlNode root = doc.SelectSingleNode("Items");
        XmlElement xe = doc.CreateElement("Item");
        xe.SetAttribute("Name", itemname);
        xe.SetAttribute("Path", itempath);
        xe.SetAttribute("MD5", MD5);
        root.AppendChild(xe);
        doc.Save(path);
    }

    /// <summary>  
    /// 将类的对象序列化后保存到本地xml文件  
    /// </summary>  
    /// <param name="entityObj">一个对象</param>  
    /// <param name="fileName">本地xml文件路径</param>  
    public void XmlSerialize(object entityObj, string fileName)
    {
        using (System.IO.FileStream stream = new System.IO.FileStream(fileName, System.IO.FileMode.Create))
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(entityObj.GetType());
            serializer.Serialize(stream, entityObj);
        }
    }

    /// <summary>  
    /// 读取序列化后保存在本地的xml文件成一个对象  
    /// </summary>  
    /// <param name="filename"></param>  
    /// <param name="entityType"></param>  
    /// <returns></returns>  
    public object XmlDeserialize(string filename, Type entityType)
    {
        object obj = null;
        using (System.IO.FileStream stream = new System.IO.FileStream(filename, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.Read))
        {
            System.Xml.Serialization.XmlSerializer serializer = new System.Xml.Serialization.XmlSerializer(entityType);
            obj = serializer.Deserialize(stream);
        }
        return obj;
    }  


    /// <summary>  
    /// 将对象序列化成二进制文件保存到本地  
    /// </summary>  
    /// <param name="obj">对象</param>  
    /// <param name="fileName">二进制文件路径</param>  
    public static void BinSerialize(object obj, string fileName)
    {
        using (System.IO.Stream strm = new System.IO.FileStream(fileName, System.IO.FileMode.OpenOrCreate, System.IO.FileAccess.Write, System.IO.FileShare.None))
        {
            System.Runtime.Serialization.IFormatter fmt = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            fmt.Serialize(strm, obj);
            strm.Flush();
        }
    }

    /// <summary>  
    /// 将序列化的二进制文件反序列化成一个对象  
    /// </summary>  
    /// <param name="fileName"></param>  
    /// <returns></returns>  
    public static object BinDeserialize(string fileName)
    {
        object obj = null;
        using (System.IO.Stream strm = new System.IO.FileStream(fileName, System.IO.FileMode.Open, System.IO.FileAccess.Read, System.IO.FileShare.None))
        {
            System.Runtime.Serialization.IFormatter fmt = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
            obj = fmt.Deserialize(strm);
        }
        return obj;
    }


    //public static void XML2Bin()
    //{
    //    string[] xmlFiles = Directory.GetFiles(Application.dataPath + "/Resources/Config/", "*.xml", SearchOption.AllDirectories);
    //    foreach (string file in xmlFiles)
    //    {
    //        //string file = Application.dataPath + "/Resources/Config/ServerIpAndEndpoint.xml";
    //        string fileName = file.Substring(file.IndexOf("Config"), file.LastIndexOf('.') - file.IndexOf("Config"));
    //        fileName = fileName.Replace("\\", "/");
    //        TextAsset asset = ResourceLoadManager.Instance.GetResources(fileName) as TextAsset;
    //        XmlZip obj = new XmlZip();
    //        obj.Init(asset.text);
    //        byte[] bytes = obj.XmlToBytes(asset.text);

    //        /*
    //        FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read);
    //        BinaryReader reader = new BinaryReader(stream, Encoding.UTF8);
    //        byte[] bytes = reader.ReadBytes((int)stream.Length);
    //        stream.Close();
    //         * */

    //        string binPath = file.Substring(0, file.LastIndexOf('.')) + ".dat";
    //        if (File.Exists(binPath))
    //            File.Delete(binPath);
    //        FileStream streamW = new FileStream(binPath, FileMode.Create);
    //        BinaryWriter writer = new BinaryWriter(streamW, Encoding.UTF8);
    //        writer.Write(bytes);
    //        writer.Flush();
    //        writer.Close();
    //        streamW.Close();
    //    }
    //}

    public static string Bin2XML(string binFile)
    {
        //string[] xmlFiles = Directory.GetFiles(Application.dataPath + "/Resources/Config/", "*.dat", SearchOption.AllDirectories);
        //foreach (string file in xmlFiles)
        {
            string file = Application.dataPath + "/Resources/" + binFile + ".dat";
            FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read);
            BinaryReader reader = new BinaryReader(stream, Encoding.UTF8);
            byte[] bytes = reader.ReadBytes((int)stream.Length);
            stream.Close();

            XmlZip obj = new XmlZip();
            string stringText = obj.BytesToXml(bytes);
            return stringText;

            //string binPath = file.Substring(0, file.LastIndexOf('.')) + ".xml";
            //if (File.Exists(binPath))
            //    File.Delete(binPath);
            //FileStream streamW = new FileStream(binPath, FileMode.Create);
            //BinaryWriter writer = new BinaryWriter(streamW, Encoding.UTF8);
            //writer.Write(stringText);
            ////writer.Write(bytes);
            //writer.Flush();
            //writer.Close();
            //streamW.Close();
        }
    }

    //public static XmlReader Bin2XMLTest(string binFile)
    //{
    //    string file = Application.dataPath + "/Resources/" + binFile + ".dat";
    //    FileStream stream = new FileStream(file, FileMode.Open, FileAccess.Read);
    //    BinaryReader reader = new BinaryReader(stream, Encoding.UTF8);
    //    byte[] bytes = reader.ReadBytes((int)stream.Length);
    //    stream.Close();

    //    XmlZip obj = new XmlZip();
    //    return obj.BytesToXmlTest(bytes);
    //}

    /// <summary>
    /// 根据类型加载xml
    /// 类的字段必须和Excel中保持一致
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="dir_xml"></param>
    /// <returns></returns>
    public static Dictionary<uint, T> LoadXml<T>(string dir_xml) where T : new()
    {
        if (dir_xml == null)
            return null;

        var itemType = typeof(T);
        var filed_array = itemType.GetFields();

        //创建装内容的字典表
        Dictionary<uint, T> dic = new Dictionary<uint, T>();

        string text = ResourceLoadManager.Instance.GetConfigText(dir_xml);
        if (text == null)
        {
            Logger.LogError("LoadConfig failed: " + dir_xml);
            return null;
        }
        //从文件读取xml的内容
        XmlDocument xmlDoc = CommonFunction.LoadXmlConfig(dir_xml, text);

        //获取第一个xml节点
        XmlElement Node = (XmlElement)xmlDoc.DocumentElement.FirstChild;
        XmlNode comment = null;
        for (; Node != null; Node = (XmlElement)Node.NextSibling)
        {
            comment = Node.SelectSingleNode(GlobalConst.CONFIG_SWITCH_COLUMN);
            if (comment != null && comment.InnerText == GlobalConst.CONFIG_SWITCH)
                continue;

            T obj = new T();
            //获取对象ID
            comment = Node.SelectSingleNode("ID");

            uint ID = 0;
            if (!(comment != null && uint.TryParse(comment.InnerText, out ID)))
            {
                Logger.LogError(string.Format("LoadXml Error : {0} ID={1}",
                        itemType.Name, comment.InnerText ));
                continue;
            }
             

            #region 给obj每个字段赋值
            foreach (var filed in filed_array)
            {
                //数据字段名，在xml中存在才进行数据数据获取
                //根据反射得到的字段信息，去xml中获取内容
                comment = Node.SelectSingleNode(filed.Name);
                if (comment == null)
                {
                    Logger.LogError(string.Format("LoadXml Error : {0} not find filed {1} type {2}",
                            itemType.Name, filed.Name, filed.FieldType.ToString()));
                }

                var value = comment.InnerText;
                if (filed.FieldType == typeof(int))
                {
                    filed.SetValue(obj, int.Parse(value));
                }
                else if (filed.FieldType == typeof(uint))
                {
                    filed.SetValue(obj, uint.Parse(value));
                }
                else if (filed.FieldType == typeof(string))
                {
                    filed.SetValue(obj, value);
                }
            }
            #endregion

            dic.Add(ID, obj);
        }

        return dic;
    }
}
