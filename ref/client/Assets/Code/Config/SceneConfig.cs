using System.Collections.Generic;
using System.Xml;
using UnityEngine;
using System.Collections;

using fogs.proto.msg;

public class SceneConfig
{
    string name = GlobalConst.DIR_XML_SCENE;
    bool isLoadFinish = false;
    private object LockObject = new object();

	public Dictionary<uint, Scene> configs = new Dictionary<uint, Scene>();
	
	public SceneConfig()
	{
        ResourceLoadManager.Instance.GetConfigResource(name, LoadFinish);
		//ReadConfig();
	}
    void LoadFinish(string vPath, object obj)
    {
        isLoadFinish = true;
        lock (LockObject) { GameSystem.Instance.loadConfigCnt += 1; }
    }
	public Scene GetConfig(uint scene_id)
	{
		Scene data = null;
		configs.TryGetValue(scene_id, out data);
		return data;
	}

    public void ReadConfig()
    {
        if (isLoadFinish == false)
            return;
        isLoadFinish = false;
        lock (LockObject) { GameSystem.Instance.readConfigCnt += 1; }

		Debug.Log("Config reading " + name);
        string text = ResourceLoadManager.Instance.GetConfigText(name);
        if (text == null)
        {
            Debug.LogError("LoadConfig failed: " + name);
            return;
        }
        
		//TextAsset asset = ResourceLoadManager.Instance.GetResources(GlobalConst.DIR_XML_SCENE) as TextAsset;
        XmlDocument doc = CommonFunction.LoadXmlConfig(name, text);
		
		XmlNode root = doc.SelectSingleNode("Scenes");
		foreach (XmlNode sceneNode in root.SelectNodes("Scene"))
		{
			XmlElement sceneElem = sceneNode as XmlElement;

			Scene scene = new Scene();
			scene.id = (uint)XmlUtils.XmlGetAttr_Int(sceneElem, "id");

			XmlNode resIdNode = sceneNode.SelectSingleNode("ResId");
			if( resIdNode != null )
				scene.resourceId = resIdNode.InnerText;

			XmlElement bgSoundElem = sceneNode.SelectSingleNode("BgSound") as XmlElement;
			if( bgSoundElem != null )
			{
				scene.bgSound = new BgSound();
				scene.bgSound.interval = (uint)XmlUtils.XmlGetAttr_Int(bgSoundElem, "interval");
				foreach (XmlNode trackNode in bgSoundElem.SelectNodes("Track"))
					scene.bgSound.trackSrc.Add(trackNode.InnerText);
			}

			XmlElement audiences = sceneNode.SelectSingleNode("Audiences") as XmlElement;
			if( audiences != null )
			{
				foreach (XmlNode audience in audiences.SelectNodes("Audience"))
				{
					XmlElement audiElem = audience as XmlElement;
					Audience audi = new Audience();
					uint id, pointId;
					if( !uint.TryParse( audience.SelectSingleNode("Id").InnerText, out id ) )
						continue;
					if( !uint.TryParse( audience.SelectSingleNode("Point").InnerText, out pointId ) )
						continue;
					audi.id = id;
					audi.pointId = pointId;

					scene.audiences.Add(audi);
				}
			}
			configs.Add(scene.id, scene);
		}

		
	}
}
