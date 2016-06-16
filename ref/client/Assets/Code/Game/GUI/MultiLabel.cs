using UnityEngine;
using System.Collections;

public class MultiLabel : MonoBehaviour {

    public bool IsConstString;

    private UILabel mainBody;
    private UILabel shadow;

	// Use this for initialization
	void Start () {
        mainBody = transform.GetComponent<UILabel>();
        Transform shadowTransform = transform.FindChild("Shadow");
        if (shadowTransform)
        {
            shadow = shadowTransform.GetComponent<UILabel>();
        }   
        if (IsConstString)
        {
            mainBody.text = CommonFunction.GetConstString(mainBody.text);
        }
        if(shadow)
        {
            LetsShadow();
        }
   
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetText(string text)
    {
        if (mainBody == null)
        {
            mainBody = transform.GetComponent<UILabel>();
            shadow = transform.FindChild("Shadow").GetComponent<UILabel>();
        }
        mainBody.text = text;
        shadow.text = text;
    }

    public void SetColor(Color color)
    {
        if (mainBody == null)
        {
            mainBody = transform.GetComponent<UILabel>();
            shadow = transform.FindChild("Shadow").GetComponent<UILabel>();
        }
        mainBody.color = color;
    }


    private void LetsShadow()
    {
        //shadow.font = mainBody.font;
        shadow.trueTypeFont = mainBody.trueTypeFont;
        shadow.bitmapFont = mainBody.bitmapFont;
        shadow.fontSize = mainBody.fontSize;
        shadow.fontStyle = mainBody.fontStyle;
        shadow.text = mainBody.text;
        shadow.overflowMethod = mainBody.overflowMethod;
        shadow.alignment = mainBody.alignment;
        shadow.keepCrispWhenShrunk = mainBody.keepCrispWhenShrunk;
        shadow.applyGradient = mainBody.applyGradient;
        shadow.gradientTop = mainBody.gradientTop;
        shadow.gradientBottom = mainBody.gradientBottom;
        shadow.spacingX = mainBody.spacingX;
        shadow.spacingY = mainBody.spacingY;
        shadow.maxLineCount = mainBody.maxLineCount;

        //shadow.color = mainBody.color;
        shadow.alpha = mainBody.alpha;
        shadow.depth = mainBody.depth - 1;
        shadow.width = mainBody.width;
        shadow.height = mainBody.height;
    }
}
