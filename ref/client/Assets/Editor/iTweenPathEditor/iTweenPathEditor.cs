// Copyright (c) 2010 Bob Berkebile
// Please direct any bugs/comments/suggestions to http://www.pixelplacement.com
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files (the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.

using UnityEngine;
using UnityEditor;
using System.Collections;

[CustomEditor(typeof(iTweenPath))]
public class iTweenPathEditor : Editor
{
	iTweenPath _target;
	private static GUIStyle style = new GUIStyle();
	public static int count = 0;
	
	void OnEnable(){
		//i like bold handle labels since I'm getting old:
		style.fontStyle = FontStyle.Bold;
		style.normal.textColor = Color.white;
		_target = (iTweenPath)target;
		
		//lock in a default path name:
		if(!_target.initialized){
			_target.initialized = true;
			_target.pathName = "New Path " + ++count;
			_target.initialName = _target.pathName;
		}
	}
	
	public override void OnInspectorGUI(){		
		//draw the path?
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Path Visible");
		_target.pathVisible = EditorGUILayout.Toggle(_target.pathVisible);
		EditorGUILayout.EndHorizontal();
		
		//path name:
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Path Name");
		_target.pathName = EditorGUILayout.TextField(_target.pathName);
		EditorGUILayout.EndHorizontal();
		
		if(_target.pathName == ""){
			_target.pathName = _target.initialName;
		}
		
		//path color:
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Path Color");
		_target.pathColor = EditorGUILayout.ColorField(_target.pathColor);
		EditorGUILayout.EndHorizontal();
		
		//path control:
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.PrefixLabel("Control points gizmo");
		_target.controlRadius = EditorGUILayout.FloatField(_target.controlRadius);
		EditorGUILayout.EndHorizontal();
		
		//exploration segment count control:
		EditorGUILayout.BeginHorizontal();
		//EditorGUILayout.PrefixLabel("Node Count");
		_target.nodeCount = Mathf.Max(2, EditorGUILayout.IntField("Node Count", _target.nodeCount));
		//_target.nodeCount =  Mathf.Clamp(EditorGUILayout.IntSlider(_target.nodeCount, 0, 10), 2,100);
		EditorGUILayout.EndHorizontal();
		
		//add node?
		if(_target.nodeCount > _target.nodes.Count){
			
			for (int i = 0; i < _target.nodeCount - _target.nodes.Count; i++) {
				_target.nodes.Add(Vector3.zero);	
			}
		}
	
		//remove node?
		if(_target.nodeCount < _target.nodes.Count){
			if(EditorUtility.DisplayDialog("Remove path node?","Shortening the node list will permantently destory parts of your path. This operation cannot be undone.", "OK", "Cancel")){
				int removeCount = _target.nodes.Count - _target.nodeCount;
				_target.nodes.RemoveRange(_target.nodes.Count-removeCount,removeCount);
			}else{
				_target.nodeCount = _target.nodes.Count;	
			}
		}
				
		//node display:
		EditorGUI.indentLevel = 4;
		for (int i = 0; i < _target.nodes.Count; i++) {
			_target.nodes[i] = EditorGUILayout.Vector3Field("Node " + (i+1), _target.nodes[i] + _target.transform.position) - _target.transform.position;
		}
		
		//make path cycle color:
		EditorGUILayout.Separator();
		EditorGUILayout.Separator();
		
		EditorGUILayout.BeginHorizontal();
		if ( GUILayout.Button("Cycle Path", GUILayout.ExpandWidth(true)) )
		{
			_target.nodes[_target.nodeCount - 1] = _target.nodes[0];
		}
		EditorGUILayout.EndHorizontal();
	
		//update and redraw:
		if(GUI.changed){
			EditorUtility.SetDirty(_target);			
		}
		
		if (Event.current.type == EventType.ExecuteCommand)
		{
			Undo.CreateSnapshot();
			Undo.RegisterSnapshot();
		}
	}
	
	/*
	[DrawGizmo( GizmoType.NotSelected | GizmoType.SelectedOrChild  )]
	static void DrawPathLabel(Transform transform, GizmoType gizmoType)
	{   
		iTweenPath target = transform.GetComponent("iTweenPath") as iTweenPath;
		if( target != null )
		{
	    	Handles.Label(target.nodes[0] + target.transform.position, "'" + target.pathName + "' Begin", style);
			Handles.Label(target.nodes[target.nodes.Count-1] + target.transform.position, "'" + target.pathName + "' End", style);
		}
	}
	*/
	 
	void OnSceneGUI(){
		if(_target.pathVisible){			
			if(_target.nodes.Count > 0){
				//allow path adjustment undo:
				Undo.SetSnapshotTarget(_target,"Adjust iTween Path");
				
				//path begin and end labels:
				Handles.Label(_target.nodes[0] + _target.transform.position, "'" + _target.pathName + "' Begin", style);
				Handles.Label(_target.nodes[_target.nodes.Count-1] + _target.transform.position, "'" + _target.pathName + "' End", style);
				
				//node handle display:
				for (int i = 0; i < _target.nodes.Count; i++) {
					_target.nodes[i] = Handles.PositionHandle( _target.transform.position + _target.nodes[i], Quaternion.identity) - _target.transform.position;
				}
				
				/*
				Handles.DrawCapFunction capFunc;
				capFunc = Handles.SphereCap;
				float capSizeMultiplier = 2.0f;
				//node handle display:
				for (int i = 0; i < _target.nodes.Count; i++) {
					_target.nodes[i] = 
						Handles.FreeMoveHandle( _target.transform.position +_target.nodes[i], Quaternion.identity,
						HandleUtility.GetHandleSize(_target.nodes[i]) * 0.075f * capSizeMultiplier,
						Vector3.zero,
						capFunc) - _target.transform.position;
				}
				*/	
			}	
		}
	}
}