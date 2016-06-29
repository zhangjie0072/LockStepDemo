using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class AnimationSampler
{
    const string PREFAB_BODY = "Object/Player/body/Manbody1";
    const string PREFAB_HEAD = "Object/Player/head/Skin_Manhead1/Skin_Manhead1";
    const string ANIMATION_DIR = "Object/Player/tongyong-nv/animation";
    const string SPECIAL_ANIMATION_DIR = "Object/Player/SpecialAction";

    static IM.Number SAMPLE_INTERVAL = new IM.Number(0, 050);
    static string[] NODE_NAMES = {"root", "Bip001 R Hand", "Bip001 Pelvis", "ball"};


    [MenuItem("Animation Editor/Sample Animation", false, 120)]
    public static void DoSample()
    {
        EditorUtility.DisplayDialog("提示", "开始对所有动作采样，将会花费一些时间，请耐心等待。", "确定");
        Debug.Log("Begin to sample animation, wait for a moment.");
        //Create model
        GameObject prefab = Resources.Load<GameObject>(PREFAB_BODY);
        GameObject goPlayer = GameObject.Instantiate(prefab) as GameObject;
        prefab = Resources.Load<GameObject>(PREFAB_HEAD);
        GameObject goHead = GameObject.Instantiate(prefab) as GameObject;
        GameUtils.ReSkinning(goHead, goPlayer);
        GameObject.DestroyImmediate(goHead);

        //Load animation
        AnimationSampleManager.Instance.Clear();
        Animation animation = goPlayer.GetComponent<Animation>();
        SampleClips(animation, ANIMATION_DIR);
        SampleClips(animation, SPECIAL_ANIMATION_DIR);
        AnimationSampleManager.Instance.SaveToXml();

        GameObject.DestroyImmediate(goPlayer);

        Debug.Log("Animation sampling finished.");
        EditorUtility.DisplayDialog("提示", "所有动作采样完成", "确定");
    }

    static void SampleClips(Animation animation, string dir)
    {
        AnimationClip[] clips = Resources.LoadAll<AnimationClip>(dir);
        foreach (AnimationClip clip in clips)
        {
            animation.AddClip(clip, clip.name);
            AnimData data = SampleClip(animation.gameObject, clip);
            AnimationSampleManager.Instance.AddAnimData(clip.name, data);
        }
    }

    static AnimData SampleClip(GameObject go, AnimationClip clip)
    {
        //Debug.Log("Sample motion, Clip:" + clip.name + " GameObject:" + go.name);

        AnimData animData = new AnimData();

        animData.sampleDatas = new List<SampleData>();

        animData.frameRate = IM.Editor.Tools.Convert(clip.frameRate);
        animData.wrapMode = clip.wrapMode;

        Dictionary<SampleNode, Transform> transforms = new Dictionary<SampleNode, Transform>();
        for (int i = 0; i < (int)SampleNode.Count; ++i)
        {
            string nodeName = NODE_NAMES[i];
            Transform transform = GameUtils.FindChildRecursive(go.transform, nodeName);
            transforms.Add((SampleNode)i, transform);
        }

        Transform root = transforms[SampleNode.Root];

        //Debug.Log(clip.averageDuration);
        for (IM.Number t = IM.Number.zero; ; t += SAMPLE_INTERVAL)
        {
            SampleData data = new SampleData();

            float time = Mathf.Min((float)t, clip.averageDuration);
            clip.SampleAnimation(go, time);
            data.time = IM.Editor.Tools.Convert(time);

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
                    nodeData.position = IM.Editor.Tools.Convert(curPos);
                    nodeData.horiAngle = IM.Editor.Tools.Convert(curRot.eulerAngles.y);
                }
                else    //其他节点采样相对于Root的位置
                {
                    nodeData.position = IM.Editor.Tools.Convert(root.transform.InverseTransformPoint(transform.position));
                }
                //Debug.Log(string.Format("Sample, clip:{0} node:{1} time:{2} pos:{3} angle:{4}",
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
            eventData.time = IM.Editor.Tools.Convert(evt.time);
            eventData.intParameter = evt.intParameter;
            eventData.floatParameter = IM.Editor.Tools.Convert(evt.floatParameter);
            eventData.stringParameter = evt.stringParameter;
            animData.eventDatas.Add(eventData);
            evt.messageOptions = SendMessageOptions.DontRequireReceiver;
        }
        AnimationUtility.SetAnimationEvents(clip, events);

        return animData;
    }
}
