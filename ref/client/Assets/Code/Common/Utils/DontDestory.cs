using UnityEngine;
using System.Collections;
public class DontDestory : MonoBehaviour 
{
	// Use this for initialization
	void Start ()
    {
        DontDestroyOnLoad(gameObject);
	}
}
