using System.Collections;
using UnityEngine;

public class Loading : MonoBehaviour
{
	[HideInInspector]
	public string scene;
	public string textureFile;

	void Start()
	{
        UILabel Tip = transform.FindChild("Tips").GetComponent<UILabel>();
		UITexture texture = transform.FindChild("Loading").GetComponent<UITexture>();
        if (!string.IsNullOrEmpty(textureFile))
            texture.mainTexture = ResourceLoadManager.Instance.GetResources(textureFile) as Texture;
        if (textureFile == "Texture/LoadShow")
        {
            int id = Random.Range(1, 26);
            string str = "STR_LOADING_TIPS_" + id;
            Tip.text = CommonFunction.GetConstString(str);
        }
		StartCoroutine(LoadScene());
	}

    IEnumerator LoadScene()
    {
        yield return new WaitForEndOfFrame();//<strong>加上这么一句就可以先显示加载画面然后再进行加载</strong>  
        AsyncOperation asyncLoad = Application.LoadLevelAsync(scene);
        ResourceLoadManager.Instance.UnloadDependAB();

        //读取完毕后返回， 系统会自动进入已加载场景  
        yield return asyncLoad;
    }
}
