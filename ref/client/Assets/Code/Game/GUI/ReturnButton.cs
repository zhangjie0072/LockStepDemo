using UnityEngine;
using System.Collections;

public class ReturnButton : MonoBehaviour {

    UISprite _sprite;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
    void Awake()
    {
        _sprite = transform.GetComponent<UISprite>();
    }
	void Update () {
	
	}

    public void ButtonPressed(bool isPress)
    {
        if (isPress)
            _sprite.spriteName = "return_pressed";
        else
            _sprite.spriteName = "return";
    }
}
