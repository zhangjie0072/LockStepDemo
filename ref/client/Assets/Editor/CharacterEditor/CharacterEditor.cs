using UnityEngine;
using UnityEditor;

static public class CharacterEditor 
{
	[MenuItem("Animation Editor/Character Attach Tag Point", false, 100)]
	static public void CharAttachTagPoint()
	{
		EditorWindow.GetWindow<UICharAttachTagPoint>(false, "Attach Tag Point", true);
	}
	
}