using UnityEngine;
using System.Collections;

public class SpecialEffectManager : Singleton<SpecialEffectManager>
{
    public GameObject ShowSpecialEffect(string effectname,Vector3 effectposition,Quaternion effectrotation,Transform effectparent)
    {
        GameObject effectPrefab =
            ResourceLoadManager.Instance.GetResources(GlobalConst.DIR_EFFECT + effectname, false) as GameObject;
        GameObject se = GameObject.Instantiate(effectPrefab)as GameObject;

        se.transform.parent = effectparent;
        se.transform.localPosition = effectposition;
        se.transform.localRotation = effectrotation;
        se.transform.localScale = Vector3.one;

        return se;
    }

    public GameObject ShowSpecialEffect(string effectname, Transform effectparent)
    {
        GameObject effectPrefab =
            ResourceLoadManager.Instance.GetResources(GlobalConst.DIR_EFFECT + effectname, false) as GameObject;
        GameObject se = GameObject.Instantiate(effectPrefab) as GameObject;

        se.transform.parent = effectparent;
        se.transform.localPosition = Vector3.zero;
        se.transform.localRotation = Quaternion.identity;
        se.transform.localScale = Vector3.one;

        return se;
    }

    public GameObject ShowSpecialEffect(GameObject effectPrefab, Vector3 effectposition, Quaternion effectrotation, Transform effectparent)
    {
        GameObject se = GameObject.Instantiate(effectPrefab) as GameObject;

        se.transform.parent = effectparent;
        se.transform.localPosition = effectposition;
        se.transform.localRotation = effectrotation;
        se.transform.localScale = Vector3.one;

        return se;
    }

    public GameObject ShowSpecialEffect(GameObject effectPrefab, Transform effectparent)
    {
        GameObject se = GameObject.Instantiate(effectPrefab) as GameObject;

        se.transform.parent = effectparent;
        se.transform.localPosition = Vector3.zero;
        se.transform.localRotation = Quaternion.identity;
        se.transform.localScale = Vector3.one;

        return se;
    }

    public void CloseSpecialEffect(GameObject effect)
    {
        GameObject.Destroy(effect);
    }
}
