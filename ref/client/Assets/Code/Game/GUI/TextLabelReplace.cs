using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextLabelReplace : MonoBehaviour {

    public bool IsConstString = false;

    private Text text;

	// Use this for initialization
	void Start () {
        text = transform.FindChild("Text").GetComponent<Text>();
        if (IsConstString)
            text.text = CommonFunction.GetConstString(text.text);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetText(string value)
    {
        if (text == null) {
            text = transform.FindChild("Text").GetComponent<Text>();
        }
        text.text = value;
    }

    public string GetText()
    {
        if (text == null) {
            text = transform.FindChild("Text").GetComponent<Text>();
        }
        return text.text;
    }
}
