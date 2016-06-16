using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
public static class AnimationEditor
{
	public class AnimModelSet : AssetPostprocessor
	{
	    void OnPreprocessModel()
	    {
			//ModelImporter modelImporter = assetImporter as ModelImporter;
			//modelImporter.animationType = ModelImporterAnimationType.Legacy;
	    }
	}
	
	public class ClipSplitter
	{
	    private List<ModelImporterClipAnimation> clipList = new List<ModelImporterClipAnimation>();
	    public void AddClip(string name, int firstFrame, int lastFrame, WrapMode wrapMode)
	    {
	        ModelImporterClipAnimation tempClip = new ModelImporterClipAnimation();
	        tempClip.name = name;
	        tempClip.firstFrame = firstFrame;
	        tempClip.lastFrame = lastFrame;
	        tempClip.wrapMode = wrapMode;
	        clipList.Add(tempClip);            
	    }
	
	    public ModelImporterClipAnimation[] getArray()
	    {
	        return clipList.ToArray();
	    }
	}
	
	[MenuItem("Animation Editor/Animation clip splitter")]
	static public void SplitAnimation ()
	{ 
		if (Selection.activeObject == null)
			return;
		Debug.Log ("当前选择:" + Selection.activeObject);
		
		GameObject fbxFile = Selection.activeObject as GameObject;
		if( fbxFile == null )
			return;
		
		string targetAnimPath = AssetDatabase.GetAssetPath(Selection.activeObject);
		
		AssetImporter assetImporter = AssetImporter.GetAtPath(targetAnimPath);
		ModelImporter modelImporter = assetImporter as ModelImporter;
		if( modelImporter == null )
		{
			EditorUtility.DisplayDialog("AnimationEditor", "找不到角色，请先选中角色","Ok");
			return;
		}
		
		if( modelImporter.animationType != ModelImporterAnimationType.Legacy )
		{
			if (EditorUtility.DisplayDialog ("AnimationEditor", "是否修改为Legacy动画?", "Yes", "No"))
				modelImporter.animationType = ModelImporterAnimationType.Legacy;
		}
		
		string dir = Path.GetDirectoryName(targetAnimPath);
		string configPath = EditorUtility.OpenFilePanel("动画配置文件", dir, "animConfig");
		if( configPath.Length == 0 )
			return;
		
		List<AnimConfigItem> configs;
		if( !PlayerActConfig.Instance.ReadConfig(configPath, out configs) )
		{
			EditorUtility.DisplayDialog("AnimationEditor", "读取动画配置文件失败","Ok");
			return;
		}
		
		ClipSplitter splitter = new ClipSplitter();
		foreach( AnimConfigItem item in configs )
	        splitter.AddClip( string.Format("{0}", item.name ), item.startFrame, item.endFrame, item.wrapMode);
		
		//append
		/*
		List<ModelImporterClipAnimation> clips = new List<ModelImporterClipAnimation>();
		foreach( ModelImporterClipAnimation destClip in modelImporter.clipAnimations )
			clips.Add( destClip );
		ModelImporterClipAnimation[] srcClips = splitter.getArray();
		foreach( ModelImporterClipAnimation clip in srcClips )
		{
			int idx = clips.FindIndex( delegate(ModelImporterClipAnimation inClip) { return inClip.name == clip.name; } );
			if( idx != -1 )
				clips[idx] = clip;
			else
				clips.Add( clip );
		}
		*/
	
		/*
		AnimationClip[] clipsInFbx = AnimationUtility.GetAnimationClips(fbxFile.animation);
		if( clipsInFbx.Length == 0 )
		{
			EditorUtility.DisplayDialog ("AnimationEditor", "FBX文件没有动画导入，请检查fbx Animations是否已勾选import animation", "Ok");
			return;
		}
		*/
		modelImporter.importAnimation = true;

		//remove
		modelImporter.clipAnimations = splitter.getArray();
		AssetDatabase.Refresh();
		
		AssetDatabase.ImportAsset(targetAnimPath);
		
		string animPackPath = Path.Combine( Path.GetDirectoryName(targetAnimPath), "animation" );
		if( Directory.Exists( animPackPath ) )
		{
			if (EditorUtility.DisplayDialog ("AnimationEditor", "是否清空已有动画?", "Yes", "No"))
			{
				string[] files = Directory.GetFiles( animPackPath );
				foreach( string file in files )
					AssetDatabase.DeleteAsset(file);
			}
		}
		else
			AssetDatabase.CreateFolder(Path.GetDirectoryName(targetAnimPath), "animation");
		
		//depulicated animation
		AnimationClip[] clipsInFbx = AnimationUtility.GetAnimationClips(fbxFile.GetComponent<Animation>());
		foreach( AnimationClip sourceClip in clipsInFbx )
		{
			string path = Path.Combine(animPackPath, sourceClip.name) + ".anim";
            string newPath = AssetDatabase.GenerateUniqueAssetPath (path);
            AnimationClip newClip = new AnimationClip();
            EditorUtility.CopySerialized(sourceClip, newClip);
			AnimConfigItem item = configs.Find( delegate(AnimConfigItem inClip) { return inClip.name == newClip.name; } );
			if( item != null && item.animEvents.Count != 0 )
			{
				AnimationEvent[] animEvent = new AnimationEvent[item.animEvents.Count];
				for( int idx = 0; idx != item.animEvents.Count; idx++ ) 
				{
					AnimConfigItem.AnimEvent eventItem = item.animEvents[idx];
					animEvent[idx] = new AnimationEvent();
					animEvent[idx].time = Mathf.Approximately((float)( item.endFrame - item.startFrame ), 0.0f)? 0.0f : (float)(eventItem.keyFrame - item.startFrame) / (float)(item.endFrame - item.startFrame) * newClip.length;
					animEvent[idx].functionName = eventItem.eventFunctionName;
					animEvent[idx].stringParameter = eventItem.stringParameter;

					//Dictionary<string, string> param = AnimConfigItem.ParseParam(animEvent[idx].stringParameter);
					//foreach( KeyValuePair<string, string> keyValue in param )
					//	Debug.Log( keyValue.Key + " " + keyValue.Value );
				}
				
				AnimationUtility.SetAnimationEvents(newClip, animEvent);
			}
            AssetDatabase.CreateAsset(newClip, newPath);
		}
	}

	//[MenuItem("Animation Editor/Create prefab")]
	static public void CreatePrefab ()
	{
		UnityEngine.Object fbxFile = Selection.activeObject;
		if( fbxFile == null ) 
		{
			EditorUtility.DisplayDialog("AnimationEditor", "找不到角色，请先选中角色","Ok");
			return;
		}

		string targetAnimPath = AssetDatabase.GetAssetPath(fbxFile);
		AssetImporter assetImporter = AssetImporter.GetAtPath(targetAnimPath);
		ModelImporter modelImporter = assetImporter as ModelImporter;
		if( modelImporter == null )
		{
			EditorUtility.DisplayDialog("AnimationEditor", "找不到角色，请先选中角色","Ok");
			return;
		}
		modelImporter.importAnimation = false;

		string prefabPath = string.Format("Assets/Resources/Player/{0}.prefab", Path.GetFileNameWithoutExtension(targetAnimPath) );
		if( File.Exists(prefabPath) )
			AssetDatabase.DeleteAsset(prefabPath);

		GameObject newCharacter = PrefabUtility.CreatePrefab(prefabPath, fbxFile as GameObject);
		Animation animComp = newCharacter.GetComponent<Animation>();
		if( animComp == null )
			animComp = newCharacter.AddComponent<Animation>();
		animComp.playAutomatically = false;

		string animPackPath = Path.Combine( Path.GetDirectoryName(targetAnimPath), "animation" );
		if( !Directory.Exists( animPackPath ) )
		{
			EditorUtility.DisplayDialog("AnimationEditor", "不存在动作文件，请先切割生成","Ok");
			return;
		}

		string[] files = Directory.GetFiles( animPackPath );
		foreach( string file in files )
		{
            GameObject goClip = ResourceLoadManager.Instance.GetResources(file) as GameObject;
			if( goClip == null || goClip.GetComponent<Animation>().clip == null )
				continue;
			AnimationClip clip = goClip.GetComponent<Animation>().clip;
			animComp.AddClip(clip, clip.name);
		}
	}

	[MenuItem("Animation Editor/Create prefab")]
	static public void CreatePrefab1 ()
	{
		if( Selection.activeObject == null )
		{
			EditorUtility.DisplayDialog("AnimationEditor", "请先选择动画文件包","Ok");
			return;
		}

		string animPackPath = AssetDatabase.GetAssetPath(Selection.activeObject); 
		if (!Directory.Exists(animPackPath)) 
		{
			EditorUtility.DisplayDialog("AnimationEditor", "请先选择动画文件包","Ok");
			return;
		}

		string strResourceTag = "Resources";
		int idxResource = animPackPath.IndexOf(strResourceTag);
		if(idxResource == -1)
			return;
		animPackPath = animPackPath.Substring(idxResource + strResourceTag.Length + 1);

		Object[] clips = Resources.LoadAll(animPackPath);

		Object[] prefabAssets = Resources.LoadAll("Prefab/Player", typeof(GameObject));
		foreach( Object res in prefabAssets )
		{
			GameObject player = GameObject.Instantiate(res) as GameObject;
			Animation animComp = player.GetComponent<Animation>();
			if( animComp != null )
				Component.DestroyImmediate(animComp);
			animComp = player.AddComponent<Animation>();

			foreach( Object clip in clips )
			{
				if( clip == null) 
					continue;
				animComp.AddClip(clip as AnimationClip, clip.name);
			}
			PrefabUtility.CreatePrefab(AssetDatabase.GetAssetPath(res), player);
			GameObject.DestroyImmediate(player);
		}
	}

	static bool _BakeClip (AnimationClip targetClip, out AnimationClip destClip, int startTime, int endTime)
	{
		destClip = new AnimationClip();
		
		if (targetClip == null)
			return false;
		
		AnimationClipCurveData[] tempCurves = AnimationUtility.GetAllCurves (targetClip);
		foreach (AnimationClipCurveData curveData in tempCurves)
		{
			AnimationCurve tempCurve;
			if( !_BakeCurve( curveData.curve, out tempCurve, startTime, endTime, targetClip.frameRate, destClip.frameRate ) )
				continue;
			//Assign the curve to the current clip
			destClip.SetCurve (curveData.path, typeof(Transform), curveData.propertyName, tempCurve);
		}
		
		return true;
	}
	
	static bool _BakeCurve( AnimationCurve targetCurve, out AnimationCurve destCurve, int targetStartFrame, int targetEndFrame, float targetFrameRate, float destFrameRate )
	{
		destCurve = null;
		if( targetCurve == null )
			return false;
		
		float fTargetStartTime = (float)targetStartFrame / targetFrameRate;
		float fTargetEndTime = (float)targetEndFrame / targetFrameRate;
		
		float fDestStartTime = 0.0f;
		float fDestEndTime  = (targetEndFrame - targetStartFrame) / destFrameRate;
		
		Keyframe startKeyFrame = new Keyframe(fDestStartTime, targetCurve.Evaluate(fTargetStartTime));
		Keyframe endKeyFrame = new Keyframe(fDestEndTime, targetCurve.Evaluate(fTargetEndTime));
		destCurve = new AnimationCurve(startKeyFrame, endKeyFrame);
		
		float fComp = targetFrameRate / destFrameRate;
		
		foreach( Keyframe kf in targetCurve.keys )
		{
			if( kf.time < fTargetStartTime )
				continue;
			if( kf.time > fTargetEndTime )
				break;
			destCurve.AddKey((kf.time - fTargetStartTime)*fComp, kf.value);
		}
		destCurve.preWrapMode = targetCurve.preWrapMode;
		destCurve.postWrapMode = targetCurve.postWrapMode;
		
		return destCurve.keys.Length != 0;
	}
}
