using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Xml;
using System.IO;

public static class MatchPointsTool{

    private static string DIR_PREFAB = "Prefab/DynObject/MatchPoints/";

    private static string[] prefebName = { 
        "ThreePTCenter",
        "BeginPos",
        "BlockStorm_Pos",
        "FreeThrowCenter",
        "GrabPoint_Pos",
        "GrabZone_Pos",
        "MassBall_Pos",
        "PractiseMove_Pos",
        "ReboundStorm_Pos",
        "TipOffPos",
        "TwoDefender_Pos"
    };

    [MenuItem("MatchPoints/生成配置", false, 120)]
    public static void DoConfig()
    {
        int configLen = prefebName.Length;
        for(int i=0;i<configLen;i++)
        {
            GameObject prefebGo = ResourceLoadManager.Instance.LoadPrefab(DIR_PREFAB + prefebName[i]) as GameObject;
            if (prefebGo)
            {
                CreateXmlForTransform(prefebGo.transform);
            }
        }
        EditorUtility.DisplayDialog("MatchPoints", "生成完成", "OK");
    }

    static void CreateXmlForTransform(Transform trans)
    {
        string path = Application.dataPath + "/Resources/Config/MatchPoints";
        string name = trans.name;
        CreatXmlFunc(path, name, trans);
    }

    private static void CreatXmlFunc(string sPath, string sName, Transform trans)
    {
        string saveName = string.Format("{0}/{1}.xml", sPath, sName);
        File.Delete(saveName);
        if (!File.Exists(saveName))
        {
            XmlDocument doc = new XmlDocument();
            XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(dec);
            ParseTransformss(trans, doc, doc);
            doc.Save(saveName);
        }
    }

    private static void ParseTransformss(Transform trans, XmlDocument doc, XmlNode parent)
    {
        if (trans.childCount > 0)
        {
            XmlElement tempparent = doc.CreateElement(trans.name);
            parent.AppendChild(tempparent);

            for (int i = 0; i < trans.childCount; i++)
            {
                Transform child = trans.GetChild(i);
                ParseTransformss(child, doc, tempparent);
            }
        }
        else
        {
            ParseTransformToXML(trans, doc, parent);
        }
    }

    private static void ParseTransformToXML(Transform trans, XmlDocument doc, XmlNode parent)
    {
        XmlElement item = doc.CreateElement(trans.name);
        XmlElement pointNode = doc.CreateElement("Point");
        XmlElement rotationNode = doc.CreateElement("Rotation");
        IM.Vector3 point = IM.Vector3.FromUnity(trans.localPosition);
        pointNode.InnerText = point.ToString();
        IM.Quaternion rotation = IM.Quaternion.FromUnity(trans.localRotation);
        rotationNode.InnerText = rotation.ToString();
        item.AppendChild(pointNode);
        item.AppendChild(rotationNode);
        parent.AppendChild(item);
    }

}
