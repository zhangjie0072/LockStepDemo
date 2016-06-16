
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text; 

public class UICharAttachTagPoint : EditorWindow
{
	GameObject goTarget;
	
	string[] arBoneList = new string[0];
	

	void OnGUI ()
	{
		goTarget = EditorGUILayout.ObjectField( "角色", goTarget, typeof(GameObject), true ) as GameObject;
		
		//Presets
		GUILayout.Label("角色预设选择");
		GUILayout.BeginHorizontal();
		bool bHuman = GUILayout.Button("人形", GUILayout.Width(60f));
		GUILayout.EndHorizontal();
		
		if( bHuman ){
			arBoneList = new string[8];
			arBoneList[0] = "Bip01 Pelvis=Pelvis";
			arBoneList[1] = "Bip01 Spine2=Chest";
			arBoneList[2] = "Bip01 Head=Head";
			arBoneList[3] = "Bip01 L Foot=LFoot";
			arBoneList[4] = "Bip01 R Foot=RFoot";
			arBoneList[5] = "Bip01 L Hand=LHand";
			arBoneList[6] = "Bip01 R Hand=RHand";
			arBoneList[7] = "EfManager=RootEff";
		}
		else 	{
			string sBoneOld = "";
			foreach( string sb in arBoneList ){
				if( sb == "" )
					continue;
				sBoneOld += sb;
				sBoneOld += "\n";
			}
			
			string sBoneList = EditorGUILayout.TextArea( sBoneOld, GUI.skin.textArea );
			
			if( sBoneList != sBoneOld )	{
				arBoneList = sBoneList.Split( '\n' );
			}
		}
		
		bool bOK = GUILayout.Button("OK", GUILayout.Width(120f));
		if( bOK ){
			_DoAttachTagPoint( );
		}
	}
	
	void _DoAttachTagPoint()	{
		ModelTagPoint[] arExisting = goTarget.GetComponentsInChildren<ModelTagPoint>();
		foreach( ModelTagPoint tp in arExisting )	{
			Component.DestroyImmediate( tp );
		}
		
		foreach( string sb in arBoneList )	{
			string[] arSplit = sb.Split( '=' );
			if( arSplit.Length < 2 )
				continue;
			
			Transform tr = GameUtils.FindChildRecursive( goTarget.transform, arSplit[0] );
			if( tr ){
				ModelTagPoint mtp = tr.GetComponent<ModelTagPoint>();
				if( mtp == null ){
					mtp = tr.gameObject.AddComponent<ModelTagPoint>( );
				}
				mtp.sName = arSplit[1];
			}
		}
	}

	
}