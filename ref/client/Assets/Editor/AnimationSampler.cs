using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

public static class AnimationSampler
{
    const string PREFAB_BODY = "Object/Player/body/Manbody1";
    const string PREFAB_HEAD = "Object/Player/head/Skin_Manhead1/Skin_Manhead1";
    const string ANIMATION_DIR = "Object/Player/tongyong-nv/animation";
    const string SPECIAL_ANIMATION_DIR = "Object/Player/SpecialAction";

    [MenuItem("Animation Editor/Sample Animation", false, 120)]
    public static void DoSample()
    {
        EditorUtility.DisplayDialog("提示", "开始对所有动作采样，将会花费一些时间，请耐心等待。", "确定");
        Logger.Log("Begin to sample animation, wait for a moment.");
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

        Logger.Log("Animation sampling finished.");
        EditorUtility.DisplayDialog("提示", "所有动作采样完成", "确定");
    }

    static void SampleClips(Animation animation, string dir)
    {
        AnimationClip[] clips = Resources.LoadAll<AnimationClip>(dir);
        foreach (AnimationClip clip in clips)
        {
            animation.AddClip(clip, clip.name);
            AnimationSampleManager.Instance.Sample(animation.gameObject, clip);
        }
    }
}
