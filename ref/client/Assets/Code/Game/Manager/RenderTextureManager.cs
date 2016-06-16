using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RenderTextureManager : MonoSingleton<RenderTextureManager> 
{
    public GameObject _cameraPrefab;
    private Dictionary<string, Camera> _modelGameObject = new Dictionary<string, Camera>();

    public void Initialize() 
    {
        //_cameraPrefab = ResourceLoadManager.Instance.LoadPrefab(GlobalConst.DIR_UI_CAMERAOBJ);
    }


    public RenderTexture GetRenderTexture(string name, GameObject vTarget, int width, int height, int depth, int antiAliasing = 2) 
    {
        RenderTexture tex = null;
        if (_modelGameObject.ContainsKey(name))
        {
            Camera _camera = null;
            _modelGameObject.TryGetValue(name, out _camera);
            if (_camera)
                tex = _camera.targetTexture;
        }
        else 
        {
            if(_cameraPrefab)
            {
                transform.position = new Vector3(100, 100, 100);
                GameObject vGO = CommonFunction.InstantiateObject(_cameraPrefab, transform);
                vGO.transform.localPosition = new Vector3(100.0F * _modelGameObject.Count,0.0F,0.0F);
                Camera _camera = vGO.GetComponent<Camera>();
                vTarget.transform.parent = vGO.transform.FindChild("ModelGameObject");
                vTarget.transform.localPosition = Vector3.zero;
                vTarget.transform.localScale = Vector3.one;
                vTarget.transform.localRotation = Quaternion.identity;
                RenderTexture CharacterTexture = new RenderTexture(width, height, depth);
                CharacterTexture.wrapMode = TextureWrapMode.Clamp;
                CharacterTexture.filterMode = FilterMode.Bilinear;
                CharacterTexture.isPowerOfTwo = false;
                CharacterTexture.antiAliasing = antiAliasing;
                _camera.targetTexture = CharacterTexture;
                
                tex = _camera.targetTexture;
                _modelGameObject.Add(name, _camera);
            }
        }
        return tex;
    }

    public void DestoryObj(string name)
    {
        if (_modelGameObject.ContainsKey(name))
        {
            Camera _camera = null;
            _modelGameObject.TryGetValue(name, out _camera);
            if (_camera)
            {
                GameObject.Destroy(_camera.targetTexture);
                _camera.targetTexture = null;
                GameObject.Destroy(_camera.gameObject);
            }
            _modelGameObject.Remove(name);
        }
    }


    public void Uninitialize() 
    {
        foreach (KeyValuePair<string, Camera> kvp in _modelGameObject) 
        {
            if (kvp.Value) 
            {
                GameObject.Destroy(kvp.Value.targetTexture);
                kvp.Value.targetTexture = null;
                GameObject.Destroy(kvp.Value.gameObject);
            }
        }
        _modelGameObject.Clear();
    }

}
