using System;
using UnityEngine;

[RequireComponent(typeof(UIPanel))]
public class ParticleClipper : MonoBehaviour
{
    const string ShaderName = "Custom/ParticleClipperS";
    const float ClipInterval = 0.5f;

    UIPanel m_targetPanel;
    Shader m_shader;

    void Start()
    {
        // find panel
        m_targetPanel = GetComponent<UIPanel>();

        if (m_targetPanel == null)
            throw new ArgumentNullException("Cann't find the right UIPanel");
        if (m_targetPanel.clipping != UIDrawCall.Clipping.SoftClip)
            throw new InvalidOperationException("Don't need to clip");

        m_shader = Shader.Find(ShaderName);

        if (!IsInvoking("Clip"))
            InvokeRepeating("Clip", 0, ClipInterval);
    }

    Vector4 CalcClipArea()
    {
        var clipRegion = m_targetPanel.finalClipRegion;
        Vector4 nguiArea = new Vector4()
        {
            x = clipRegion.x - clipRegion.z / 2,
            y = clipRegion.y - clipRegion.w / 2,
            z = clipRegion.x + clipRegion.z / 2,
            w = clipRegion.y + clipRegion.w / 2
        };

        var uiRoot = GameObject.Find("UIRoot(Clone)").GetComponent<UIRoot>();
        var pos = m_targetPanel.transform.position - uiRoot.transform.position;
        float h = 2;
        float temp = h / uiRoot.manualHeight;

        return new Vector4()
        {
            x = pos.x + nguiArea.x * temp,
            y = pos.y + nguiArea.y * temp,
            z = pos.x + nguiArea.z * temp,
            w = pos.y + nguiArea.w * temp
        };
    }

    void Clip()
    {
        Vector4 clipArea = CalcClipArea();
        var particleSystems = this.GetComponentsInChildren<ParticleSystem>();

        for (int i = 0; i < particleSystems.Length; i++)
        {
            var ps = particleSystems[i];
            var mat = ps.GetComponent<Renderer>().sharedMaterial;

            if (mat.shader.name != ShaderName)
                mat.shader = m_shader;

            mat.SetVector("_Area", clipArea);
        }
    }

    void OnDestroy()
    {
        CancelInvoke("Clip");
    }
}