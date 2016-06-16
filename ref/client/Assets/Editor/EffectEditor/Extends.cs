using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using System.IO;

public static class Extends
{
    public static Object GetPrefabParent(GameObject o)
    {
        Object prefabRoot = PrefabUtility.FindPrefabRoot(o);
        if (prefabRoot != null && prefabRoot.GetInstanceID() == o.GetInstanceID())
        {
            Object prefabParent = PrefabUtility.GetPrefabParent(o);
            if (prefabParent != null)
            {
                string strPath = AssetDatabase.GetAssetPath(prefabParent);
                if (strPath != null && strPath.Contains("Resource/prefab/") && strPath.EndsWith(".prefab"))
                {
                    return prefabParent;
                }
            }
        }
        return null;
    }
    static List<UnityEngine.Object> ListSelected = new List<UnityEngine.Object>();
    static void SelectRenderObject(GameObject o)
    {
        Object prefabRoot = PrefabUtility.FindPrefabRoot(o);
        if (prefabRoot != null && prefabRoot.GetInstanceID() == o.GetInstanceID())
        {
            Object prefabParent = PrefabUtility.GetPrefabParent(o);
            if (prefabParent != null)
            {
                int nColor = o.name.IndexOf("COLOR");
                if (nColor == -1)
                {
                    nColor = o.name.Length;
                }
                for (int i = 0; i < nColor; ++i)
                {
                    if (o.name[i] >= 'A' && o.name[i] <= 'Z')
                    {
                        ListSelected.Add(o);
                        return;
                    }
                }
                string strPath = AssetDatabase.GetAssetPath(prefabParent);
                if (strPath.Contains("Resource/prefab/") && strPath.EndsWith(".prefab"))
                {
                    for (int i = 0; i < o.transform.childCount; ++i)
                    {
                        SelectRenderObject(o.transform.GetChild(i).gameObject);
                    }
                }
                else
                {
                    ListSelected.Add(o);
                }
            }
        }
    }


    [MenuItem("Animation Editor/动画控制窗口",false, 1)]
    static public void HoldAnimation()
    {
        EditorWindow.GetWindow(typeof(MultObjectAnimControl));

    }
}


