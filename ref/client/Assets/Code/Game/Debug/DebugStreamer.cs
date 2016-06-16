/*	DebugStreamer in c#
	路 displays on screen a history of 'numberOfLines' of whatever text is sent to 'message'
	路 'showLineMovement' adds a rotating mark at the end of the lines of text, so repetitive message can be seen to be moving

	路 to use, add this script to a game object, then from another script simply add "DebugStreamer.message = "text to display";
	This script is the c# version of the debug streamer provided by Jamie McCarter.
*/
using UnityEngine;
using System.Collections;

public class DebugStreamer : MonoBehaviour
{
	public string message;
	public bool showLineMovement;
	public TextAnchor anchorAt = TextAnchor.LowerLeft;
	public int numberOfLines = 5;
	public int pixelOffset = 5;

	private GameObject guiObj;
	private GUIText guiTxt;
	private TextAnchor _anchorAt;
	private float _pixelOffset;
	private bool _showLineMovement;
	private string	_message;
	private ArrayList messageHistory = new ArrayList ();
	private int messageHistoryLength;
	private string	displayText;
	private int	patternIndex = 0;
	private string[] pattern = new string[] {"-", "\\", "|", "/"};
	// Use this for initialization
	void Awake ()
	{
		DontDestroyOnLoad(this);

		guiObj = gameObject;
		guiObj.AddComponent <GUIText>();
		guiObj.transform.position = Vector3.zero;
		guiObj.transform.localScale = new Vector3 (0, 0, 1);
		guiObj.name = "Debug Streamer";
		guiTxt = guiObj.GetComponent<GUIText>();
		_anchorAt = anchorAt;
		_message = message;
		SetPosition ();
	}

	void Start ()
	{
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		//	if anchorAt or pixelOffset has changed while running, update the text position
		if (_anchorAt != anchorAt || _pixelOffset != pixelOffset) 
		{
			_anchorAt = anchorAt;
			_pixelOffset = pixelOffset;
			SetPosition ();
		}
		
		//	if the message has changed, update the display
		if (_message != message) 
		{
			_message = message;
			if (showLineMovement) 
			{
				messageHistory.Insert (0, message + "\t" + pattern [patternIndex]);
				messageHistoryLength = messageHistory.Count;
				//messageHistoryLength = messageHistory.Unshift(message + "\t" + pattern[patternIndex]);
			} 
			else
				messageHistory.Insert (0, message);

			messageHistoryLength = messageHistory.Count;
			//messageHistoryLength = messageHistory.Unshift(message);
		
			patternIndex = (patternIndex + 1) % 4;
			while (messageHistoryLength>numberOfLines) {
				//messageHistory.Pop();
				messageHistory.RemoveAt (messageHistory.Count - 1);
				messageHistoryLength = messageHistory.Count;
			}
			//	create the multi-line text to display
			displayText = "";
			for (int i = 0; i<messageHistory.Count; i++) 
			{
				if (i == 0)
					displayText = messageHistory [i] as string;
				else
					displayText = (messageHistory [i] as string) + "\n" + displayText;
			}
		
			guiTxt.text = displayText;
		}
	}

	public void SetPosition ()
	{
		switch (anchorAt) {
		case TextAnchor.UpperLeft:
			guiObj.transform.position = new Vector3 (0.0f, 1.0f, 0.0f);
			guiTxt.anchor = anchorAt;
			guiTxt.alignment = TextAlignment.Left;
			guiTxt.pixelOffset = new Vector2 (pixelOffset, -pixelOffset);
			break;
		case TextAnchor.UpperCenter:
			guiObj.transform.position = new Vector3 (0.5f, 1.0f, 0.0f);
			guiTxt.anchor = anchorAt;
			guiTxt.alignment = TextAlignment.Center;
			guiTxt.pixelOffset = new Vector2 (0, -pixelOffset);
			break;
		case TextAnchor.UpperRight:
			guiObj.transform.position = new Vector3 (1.0f, 1.0f, 0.0f);
			guiTxt.anchor = anchorAt;
			guiTxt.alignment = TextAlignment.Right;
			guiTxt.pixelOffset = new Vector2 (-pixelOffset, -pixelOffset);
			break;
		case TextAnchor.MiddleLeft:
			guiObj.transform.position = new Vector3 (0.0f, 0.5f, 0.0f);
			guiTxt.anchor = anchorAt;
			guiTxt.alignment = TextAlignment.Left;
			guiTxt.pixelOffset = new Vector2 (pixelOffset, 0.0f);
			break;
		case TextAnchor.MiddleCenter:
			guiObj.transform.position = new Vector3 (0.5f, 0.5f, 0.0f);
			guiTxt.anchor = anchorAt;
			guiTxt.alignment = TextAlignment.Center;
			guiTxt.pixelOffset = new Vector2 (0, 0);
			break;
		case TextAnchor.MiddleRight:
			guiObj.transform.position = new Vector3 (1.0f, 0.5f, 0.0f);
			guiTxt.anchor = anchorAt;
			guiTxt.alignment = TextAlignment.Right;
			guiTxt.pixelOffset = new Vector2 (-pixelOffset, 0.0f);
			break;
		case TextAnchor.LowerLeft:
			guiObj.transform.position = new Vector3 (0.0f, 0.0f, 0.0f);
			guiTxt.anchor = anchorAt;
			guiTxt.alignment = TextAlignment.Left;
			guiTxt.pixelOffset = new Vector2 (pixelOffset, pixelOffset);
			break;
		case TextAnchor.LowerCenter:
			guiObj.transform.position = new Vector3 (0.5f, 0.0f, 0.0f);
			guiTxt.anchor = anchorAt;
			guiTxt.alignment = TextAlignment.Center;
			guiTxt.pixelOffset = new Vector2 (0, pixelOffset);
			break;
		case TextAnchor.LowerRight:
			guiObj.transform.position = new Vector3 (1.0f, 0.0f, 0.0f);
			guiTxt.anchor = anchorAt;
			guiTxt.alignment = TextAlignment.Right;
			guiTxt.pixelOffset = new Vector2 (-pixelOffset, pixelOffset);
			break;
		}
	}
}
