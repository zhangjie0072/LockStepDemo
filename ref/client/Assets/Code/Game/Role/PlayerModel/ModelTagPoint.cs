using UnityEngine;
using System.Collections;

public class ModelTagPoint : MonoBehaviour {
	public string sName;
	
	void Awake(){
		if( sName == "" ){
			if( gameObject.name.StartsWith("Bip01 ") ){
				sName = gameObject.name.Substring( 6 );
			}
			
		}
	}
	
}
