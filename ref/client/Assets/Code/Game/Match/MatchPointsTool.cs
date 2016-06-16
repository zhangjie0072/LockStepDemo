using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Xml;
using System.IO;

public static class MatchPointsTool{

    private static string DIR_PREFAB = "Prefab/DynObject/MatchPoints/";

    private static string[] prefebName = { "ThreePTCenter", "BeginPos", "BlockStorm_Pos", "FreeThrowCenter", "GrabPoint_Pos", "GrabZone_Pos", "MassBall_Pos", "PractiseMove_Pos", "ReboundStorm_Pos", "TipOffPos", "TwoDefender_Pos"};
    [MenuItem("MatchPoints/生成配置", false, 120)]
    public static void DoConfig()
    {
        EditorUtility.DisplayDialog("MatchPoints", "生成中..", "OK");
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
            XmlElement root = doc.CreateElement("data");
            root.SetAttribute("Version", GlobalConst.VERSION_NUMBER.ToString());
            PariseTransformss(trans, doc, root);
            doc.AppendChild(root);
            doc.Save(saveName);
        }
    }

    private static void PariseTransformss(Transform trans, XmlDocument doc, XmlElement parent)
    {
        if (trans.childCount > 0)
        {
            for (int i = 0; i < trans.childCount; i++)
            {
                Transform temp = trans.GetChild(i);
                if (temp.childCount > 1)
                {
                    XmlElement tempparent = doc.CreateElement(temp.name);
                    parent.AppendChild(tempparent);
                    PariseTransformss(temp, doc, tempparent);
                }
                else
                {
                    PariseTransformToXML(temp, doc, parent);
                }
            }
        }
        else
        {
            PariseTransformToXML(trans, doc, parent);
        }
    }

    private static void PariseTransformToXML(Transform trans, XmlDocument doc, XmlElement parent)
    {
        XmlElement item = doc.CreateElement(trans.name);
        XmlElement pointNode = doc.CreateElement("Point");
        XmlElement rotationNode = doc.CreateElement("Rotation");
        IM.Vector3 point = IM.Vector3.ToIMVector3(trans.localPosition);
        pointNode.InnerText = point.ToString();
        IM.Quaternion rotation = IM.Quaternion.ToIMQuaternion(trans.localRotation);
        rotationNode.InnerText = rotation.ToString();
        item.AppendChild(pointNode);
        item.AppendChild(rotationNode);
        parent.AppendChild(item);
    }

}
