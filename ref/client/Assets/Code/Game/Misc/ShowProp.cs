using UnityEngine;
using System.Collections;

public class ShowProp : MonoBehaviour {

	public int intProp = 0;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		intProp = gameObject.GetComponent<Renderer>().material.renderQueue;
	}
}
