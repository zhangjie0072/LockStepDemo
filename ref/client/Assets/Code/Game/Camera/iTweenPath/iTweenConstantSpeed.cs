/*
 * Developed by Felipe Teixeira
 * E-mail: felipetnh@gmail.com
 * 07/June/2012
**/

using UnityEngine;
using System.Collections.Generic;
public class iTweenConstantSpeed : MonoBehaviour {
	
	public int amount = 100;
	public float distance = 2;
	
	iTweenPath path;
	Vector3[] position;
	List<Vector3> nodes = new List<Vector3>(){Vector3.zero};

	
	void Start(){
		path = this.gameObject.GetComponent("iTweenPath") as iTweenPath;
		position = iTweenPath.GetPath(path.pathName);
		//ws nodes position to ls
		nodes[0] = position[0] - path.transform.position;
		int atual = 0;
		for( int i = 0; i < amount; i++ ){
			//
			if( Vector3.Distance(nodes[atual], iTween.PointOnPath(position,(float)i/amount)) > distance ){
				//ws nodes position to ls
				nodes.Add( iTween.PointOnPath(position,(float)i/amount) - path.transform.position );
				atual++;
			}
		}
		nodes.Add(position[position.Length-1] - path.transform.position);
		
		//ls nodes
		path.nodes = nodes;
		path.nodeCount = nodes.Count;
	}
}
