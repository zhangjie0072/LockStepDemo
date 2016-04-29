using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class MotionSampler
{
    [MenuItem("MotionSampler/Sample")]
    public static void DoSample()
    {
        GameObject go = Selection.activeGameObject;
        string rootNodeName =  "Root/Hips";
        AnimationClip[] clips = Resources.LoadAll<AnimationClip>("Animation");
        foreach (AnimationClip clip in clips)
        {
            MotionSampleManager.Instance.Sample(go, clip, rootNodeName);
        }
        MotionSampleManager.Instance.SaveToXml();
    }
}
