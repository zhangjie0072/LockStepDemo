using System.Xml;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

public class SampleData
{
    public IM.Number time;
    public Dictionary<SampleNode, SampleNodeData> nodes;
}

public class SampleNodeData
{
    public IM.Vector3 position;
    public IM.Number horiAngle;
}

public class AnimEventData
{
    public string funcName;
    public IM.Number time;
    public string stringParameter;
    public int intParameter;
    public IM.Number floatParameter;
}

public class AnimData
{
    public WrapMode wrapMode;
    public IM.Number frameRate;
    public IM.Number duration;
    public List<SampleData> sampleDatas;
    public List<AnimEventData> eventDatas;
}
public enum SampleNode
{
    Root,
    RHand,
    Pelvis,
    Ball,
    Count,
}

public class AnimationSampleManager : Singleton<AnimationSampleManager>
{
    static string[] NODE_NAMES = {"root", "Bip001 R Hand", "Bip001 Pelvis", "ball"};

    static IM.Number SAMPLE_INTERVAL = new IM.Number(0, 050);
    const string XML_ASSET_NAME = "Config/Player/AnimationSample";
    const string XML_FILE_NAME = "Assets/Resources/" + XML_ASSET_NAME + ".xml";

	uint count = 0;
	bool isLoadFinish = false;
	private object LockObject = new object();

    Dictionary<string, AnimData> datas = new Dictionary<string, AnimData>();

    public AnimData GetAnimData(string name)
    {
        AnimData data = null;
        if (!datas.TryGetValue(name, out data))
            Logger.LogError("AnimationSampleManager, no animation data for clip: " + name);
        return data;
    }

    public void Clear()
    {
        datas.Clear();
    }

#if UNITY_EDITOR
    public void Sample(GameObject go, AnimationClip clip)
    {
        //Logger.Log("Sample motion, Clip:" + clip.name + " GameObject:" + go.name);

        AnimData animData = new AnimData();

        animData.sampleDatas = new List<SampleData>();
        datas.Add(clip.name, animData);

        animData.frameRate = IM.Number.FromUnity(clip.frameRate);
        animData.wrapMode = clip.wrapMode;

        Dictionary<SampleNode, Transform> transforms = new Dictionary<SampleNode, Transform>();
        for (int i = 0; i < (int)SampleNode.Count; ++i)
        {
            string nodeName = NODE_NAMES[i];
            Transform transform = GameUtils.FindChildRecursive(go.transform, nodeName);
            transforms.Add((SampleNode)i, transform);
        }

        Transform root = transforms[SampleNode.Root];

        //Logger.Log(clip.averageDuration);
        for (IM.Number t = IM.Number.zero; ; t += SAMPLE_INTERVAL)
        {
            SampleData data = new SampleData();

            float time = Mathf.Min((float)t, clip.averageDuration);
            clip.SampleAnimation(go, time);
            data.time = IM.Number.FromUnity(time);

            data.nodes = new Dictionary<SampleNode, SampleNodeData>();
            for (int i = 0; i < (int)SampleNode.Count; ++i)
            {
                SampleNode node = (SampleNode)i;
                Transform transform = transforms[node];
                Vector3 curPos = transform.position;
                Quaternion curRot = transform.rotation;

                SampleNodeData nodeData = new SampleNodeData();
                if (node == SampleNode.Root)    //Root节点采样相对于Player的位置
                {
                    nodeData.position = IM.Vector3.FromUnity(curPos);
                    nodeData.horiAngle = IM.Number.FromUnity(curRot.eulerAngles.y);
                }
                else    //其他节点采样相对于Root的位置
                {
                    nodeData.position = IM.Vector3.FromUnity(root.transform.InverseTransformPoint(transform.position));
                }
                //Logger.Log(string.Format("Sample, clip:{0} node:{1} time:{2} pos:{3} angle:{4}",
                //    clip.name, node, time, nodeData.position, nodeData.horiAngle));

                data.nodes.Add(node, nodeData);
            }

            animData.sampleDatas.Add(data);

            if ((float)t >= clip.averageDuration)
                break;
        }


        //Animation event
        animData.eventDatas = new List<AnimEventData>();
        AnimationEvent[] events = AnimationUtility.GetAnimationEvents(clip);
        for (int i = 0; i < events.Length; ++i)
        {
            AnimationEvent evt = events[i];
            AnimEventData eventData = new AnimEventData();
            eventData.funcName = evt.functionName;
            eventData.time = IM.Number.FromUnity(evt.time);
            eventData.intParameter = evt.intParameter;
            eventData.floatParameter = IM.Number.FromUnity(evt.floatParameter);
            eventData.stringParameter = evt.stringParameter;
            animData.eventDatas.Add(eventData);
            evt.messageOptions = SendMessageOptions.DontRequireReceiver;
        }
        AnimationUtility.SetAnimationEvents(clip, events);
    }

    public void SaveToXml()
    {
        XmlDocument doc = new XmlDocument();
        XmlProcessingInstruction pi = doc.CreateProcessingInstruction("xml", "version=\"1.0\" encoding=\"UTF-8\"");
        doc.AppendChild(pi);
        XmlElement root = doc.CreateElement("AnimationSamples");
        doc.AppendChild(root);
        foreach (KeyValuePair<string, AnimData> pair in datas)
        {
            XmlElement motion = doc.CreateElement("Animation");
            root.AppendChild(motion);
            XmlAttribute name = doc.CreateAttribute("name");
            name.Value = pair.Key;
            motion.Attributes.Append(name);
            XmlAttribute wrapMode = doc.CreateAttribute("wrapMode");
            wrapMode.Value = ((int)pair.Value.wrapMode).ToString();
            motion.Attributes.Append(wrapMode);
            XmlAttribute frameRate = doc.CreateAttribute("frameRate");
            frameRate.Value = pair.Value.frameRate.ToString();
            motion.Attributes.Append(frameRate);
            List<SampleData> samples = pair.Value.sampleDatas;
            foreach (SampleData data in samples)
            {
                XmlElement sample = doc.CreateElement("Sample");
                motion.AppendChild(sample);
                XmlAttribute time = doc.CreateAttribute("time");
                time.Value = data.time.ToString();
                sample.Attributes.Append(time);
                foreach (KeyValuePair<SampleNode, SampleNodeData> nodeData in data.nodes)
                {
                    XmlElement node = doc.CreateElement("Node");
                    sample.AppendChild(node);
                    XmlAttribute nodeName = doc.CreateAttribute("name");
                    nodeName.Value = ((int)nodeData.Key).ToString();
                    node.Attributes.Append(nodeName);
                    if (nodeData.Value.position != IM.Vector3.zero)
                    {
                        XmlAttribute position = doc.CreateAttribute("position");
                        position.Value = nodeData.Value.position.ToString();
                        node.Attributes.Append(position);
                    }
                    if (nodeData.Value.horiAngle != IM.Number.zero)
                    {
                        XmlAttribute horiAngle = doc.CreateAttribute("hori_angle");
                        horiAngle.Value = nodeData.Value.horiAngle.ToString();
                        node.Attributes.Append(horiAngle);
                    }
                }
            }

            List<AnimEventData> events = pair.Value.eventDatas;
            foreach (AnimEventData eventData in events)
            {
                XmlElement evt = doc.CreateElement("Event");
                motion.AppendChild(evt);
                XmlAttribute time = doc.CreateAttribute("time");
                time.Value = eventData.time.ToString();
                evt.Attributes.Append(time);
                XmlAttribute func = doc.CreateAttribute("func");
                func.Value = eventData.funcName;
                evt.Attributes.Append(func);
                if (eventData.intParameter != 0)
                {
                    XmlAttribute intParam = doc.CreateAttribute("int_param");
                    intParam.Value = eventData.intParameter.ToString();
                    evt.Attributes.Append(intParam);
                }
                if (!string.IsNullOrEmpty(eventData.stringParameter))
                {
                    XmlAttribute stringParam = doc.CreateAttribute("string_param");
                    stringParam.Value = eventData.stringParameter;
                    evt.Attributes.Append(stringParam);
                }
                if (eventData.floatParameter != IM.Number.zero)
                {
                    XmlAttribute floatParam = doc.CreateAttribute("float_param");
                    floatParam.Value = eventData.floatParameter.ToString();
                    evt.Attributes.Append(floatParam);
                }
            }
        }

        doc.Save(XML_FILE_NAME);
    }
#endif

    public void LoadXml()
    {
		ResourceLoadManager.Instance.GetConfigResource(XML_ASSET_NAME, LoadFinish);
    }

	void LoadFinish(string vPath, object obj)
	{
		++count;
		if (count == 1)
		{
			isLoadFinish = true;
			lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
		}
	}

    public void ReadConfig()
    {
		if (isLoadFinish == false)
			return;
		isLoadFinish = false;
		lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }

		string text = ResourceLoadManager.Instance.GetConfigText(XML_ASSET_NAME);
		if (text == null)
		{
			ErrorDisplay.Instance.HandleLog("LoadConfig failed: " + XML_ASSET_NAME, "", LogType.Error);
			return;
		}

        datas.Clear();
        XmlDocument doc = new XmlDocument();
        doc.LoadXml(text);
        XmlNode root = doc.SelectSingleNode("AnimationSamples");
        foreach (XmlNode motion in root.SelectNodes("Animation"))
        {
            AnimData animData = new AnimData();
            string name = motion.Attributes["name"].Value;
            datas.Add(name, animData);

            animData.wrapMode = (WrapMode)(int.Parse(motion.Attributes["wrapMode"].Value));
            animData.frameRate = IM.Number.Parse(motion.Attributes["frameRate"].Value);

            animData.sampleDatas = new List<SampleData>();
            foreach (XmlNode sample in motion.SelectNodes("Sample"))
            {
                IM.Number time = IM.Number.Parse(sample.Attributes["time"].Value);
                SampleData data = new SampleData();
                data.time = time;
                animData.sampleDatas.Add(data);

                data.nodes = new Dictionary<SampleNode, SampleNodeData>();
                foreach (XmlNode node in sample.SelectNodes("Node"))
                {
                    SampleNodeData nodeData = new SampleNodeData();
                    SampleNode nodeName = (SampleNode)(int.Parse(node.Attributes["name"].Value));
                    XmlAttribute attrPosition = node.Attributes["position"];
                    if (attrPosition != null)
                        nodeData.position = IM.Vector3.Parse(attrPosition.Value);
                    XmlAttribute attrHoriAngle = node.Attributes["hori_angle"];
                    if (attrHoriAngle != null)
                        nodeData.horiAngle = IM.Number.Parse(attrHoriAngle.Value);
                    data.nodes.Add(nodeName, nodeData);
                }
            }
            animData.duration = animData.sampleDatas[animData.sampleDatas.Count - 1].time;

            animData.eventDatas = new List<AnimEventData>();
            foreach (XmlNode evt in motion.SelectNodes("Event"))
            {
                AnimEventData eventData = new AnimEventData();
                eventData.time = IM.Number.Parse(evt.Attributes["time"].Value);
                eventData.funcName = evt.Attributes["func"].Value;
                XmlAttribute attrIntParam = evt.Attributes["int_param"];
                if (attrIntParam != null)
                    eventData.intParameter = int.Parse(attrIntParam.Value);
                XmlAttribute attrStringParam = evt.Attributes["string_param"];
                if (attrStringParam != null)
                    eventData.stringParameter = attrStringParam.Value;
                XmlAttribute attrFloatParam = evt.Attributes["float_param"];
                if (attrFloatParam != null)
                    eventData.floatParameter = IM.Number.Parse(attrFloatParam.Value);
                animData.eventDatas.Add(eventData);
            }
            //按事件时间排序
            animData.eventDatas.Sort((d1, d2) => d1.time < d2.time ? -1 : (d1.time > d2.time ? 1 : 0));
        }
    }
}
