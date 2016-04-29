using System.Xml;
using System.Collections.Generic;
using UnityEngine;

public class SampleData
{
    public float time;
    public Vector3 position;
    public float horiAngle;
}

public class MotionSampleManager : Singleton<MotionSampleManager>
{
    const float SAMPLE_INTERVAL = 0.1f;
    const string XML_ASSET_NAME = "Animation/MotionSample";
    const string XML_FILE_NAME = "Assets/Resources/" + XML_ASSET_NAME + ".xml";

    Dictionary<string, List<SampleData>> datas = new Dictionary<string, List<SampleData>>();

#if UNITY_EDITOR
    public void Sample(GameObject go, AnimationClip clip, string node)
    {
        Logger.Log("Sample motion, Clip:" + clip.name + " GameObject:" + go.name);

        List<SampleData> samples = new List<SampleData>();
        Transform root = go.transform.FindChild(node);
        go.SampleAnimation(clip, 0);
        Vector3 oriPos = root.localPosition;
        Quaternion oriRot = root.localRotation;
        Logger.Log("Origin: " + oriPos + " " + oriRot);

        Logger.Log(clip.averageDuration);
        for (float t = 0f; ; t += SAMPLE_INTERVAL)
        {
            float time = Mathf.Min(t, clip.averageDuration);
            go.SampleAnimation(clip, time);
            Vector3 curPos = root.localPosition;
            Quaternion curRot = root.localRotation;

            Vector3 deltaPos = curPos - oriPos;
            float deltaAngle = Mathf.DeltaAngle(curRot.eulerAngles.y, oriRot.eulerAngles.y);
            Logger.Log("Sample: " + time + " " + deltaPos + " " + deltaAngle);

            SampleData data = new SampleData();
            data.time = time;
            data.position = deltaPos;
            data.horiAngle = deltaAngle;
            samples.Add(data);

            if (t >= clip.averageDuration)
                break;
        }

        datas.Add(clip.name, samples);
    }

    public void SaveToXml()
    {
        XmlDocument doc = new XmlDocument();
        XmlProcessingInstruction pi = doc.CreateProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
        doc.AppendChild(pi);
        XmlElement root = doc.CreateElement("MotionSamples");
        doc.AppendChild(root);
        foreach (KeyValuePair<string, List<SampleData>> pair in datas)
        {
            XmlElement motion = doc.CreateElement("Motion");
            XmlAttribute name = doc.CreateAttribute("name");
            name.Value = pair.Key;
            motion.Attributes.Append(name);
            List<SampleData> samples = pair.Value;
            foreach (SampleData data in samples)
            {
                XmlElement sample = doc.CreateElement("Sample");
                XmlAttribute time = doc.CreateAttribute("time");
                time.Value = data.time.ToString();
                sample.Attributes.Append(time);
                XmlAttribute position = doc.CreateAttribute("position");
                position.Value = data.position.ToString();
                sample.Attributes.Append(position);
                XmlAttribute horiAngle = doc.CreateAttribute("hori_angle");
                horiAngle.Value = data.horiAngle.ToString();
                sample.Attributes.Append(horiAngle);
                motion.AppendChild(sample);
            }
            root.AppendChild(motion);
        }

        doc.Save(XML_FILE_NAME);
    }
#endif

    public void LoadFromXml()
    {
        datas.Clear();
        TextAsset file = Resources.Load<TextAsset>(XML_ASSET_NAME);
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(file.text);
        XmlNode root = doc.SelectSingleNode("MotionSamples");
        foreach (XmlNode motion in root.SelectNodes("Motion"))
        {
            List<SampleData> samples = new List<SampleData>();
            string name = motion.Attributes["name"].Value;
            foreach (XmlNode sample in motion.SelectNodes("Sample"))
            {
                float time = float.Parse(sample.Attributes["time"].Value);
                string strPos = sample.Attributes["position"].Value;
                string[] tokens = strPos.Substring(1, strPos.Length - 2).Split(',');
                Vector3 position = new Vector3(float.Parse(tokens[0]), float.Parse(tokens[1]), float.Parse(tokens[2]));
                float horiAngle = float.Parse(sample.Attributes["hori_angle"].Value);
                SampleData data = new SampleData();
                data.time = time;
                data.position = position;
                data.horiAngle = horiAngle;
                samples.Add(data);
            }
            datas.Add(name, samples);
        }
    }
}
