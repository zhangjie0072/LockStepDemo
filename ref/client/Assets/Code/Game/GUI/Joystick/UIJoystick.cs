using UnityEngine;
using System.Collections;

/// <summary>
/// Allows dragging of the specified target object by mouse or touch, optionally limiting it to be within the UIPanel's clipped rectangle.
/// </summary>
public class UIJoystick : MonoBehaviour
{
    /// <summary>
    /// Target object that will be dragged.
    /// </summary>
    public Transform target;
    public Vector3 scale = Vector3.one;
    public float radius = 40.0f;								// the radius for the joystick to move
    bool mPressed = false;

    public bool centerOnPress = true;
    Vector3 userInitTouchPos;

    //Joystick vars
    public int tapCount;
    public bool analogOutput = false;
    public bool normalize = false; 							// Normalize output after the dead-zone?
    public Vector2 position; 								// [-1, 1] in x,y
	public float range = 0.3f;
    public float deadZone = 2f;								// Control when position is output
    public float fadeOutAlpha = 0.2f;
    public float fadeOutDelay = 1f;
    public UIWidget[] widgetsToFade;						// UIWidgets that should fadeIn/Out when centerOnPress = true
    public Transform[] widgetsToCenter;						// GameObjects to Center under users thumb when centerOnPress = true
    private float lastTapTime = 0f;
    public float doubleTapTimeWindow = 0.5f;				// time in Seconds to recognize a double tab
    public GameObject doubleTapMessageTarget;
    public string doubleTabMethodeName;
	public UISprite circleSpeedUp;

	private Color32 colSpeedHighlight = new Color32(0xf5, 0xfc, 0x7e, 0xff);
	private Color32 colWhite = new Color32(0xff, 0xff, 0xff, 0xff);

	private	Vector3	originPos = Vector3.zero;

    void Awake() 
	{ userInitTouchPos = Vector3.zero; }

    void Start()
    {
        if (centerOnPress) { StartCoroutine(fadeOutJoystick()); }
    }

    IEnumerator fadeOutJoystick()
    {
        yield return new WaitForSeconds(fadeOutDelay);
        foreach (UIWidget widget in widgetsToFade)
        {
            Color lastColor = widget.color;
            Color newColor = lastColor;
            newColor.a = fadeOutAlpha;
            //TweenColor.Begin(widget.gameObject, 0.5f, newColor).method = UITweener.Method.EaseOut;
        }
    }

	void _ConstraintToBound(Vector3 curPos, out Vector3 newPos)
	{
		newPos = curPos;
		Vector3 offset = curPos - originPos;
		if( offset.magnitude > range )
		{
			Vector3 dirTargetToOrigin = (curPos - originPos).normalized;
			newPos = originPos + dirTargetToOrigin * range;
			userInitTouchPos = newPos;
		}
	}

    /// <summary>
    /// Create a plane on which we will be performing the dragging.
    /// </summary>
    public void OnPress(bool pressed)
    {
        if (target != null)
        {
			if (mPressed == pressed)
				return;
            mPressed = pressed;

            if (pressed)
            {
                StopAllCoroutines();
                if (Time.time < lastTapTime + doubleTapTimeWindow)
                {

                    if (doubleTapMessageTarget != null && doubleTabMethodeName != "")
                    {
                        doubleTapMessageTarget.SendMessage(doubleTabMethodeName, SendMessageOptions.DontRequireReceiver);
                        tapCount++;
                    }
                    else
                    {
                        Logger.LogWarning("Double Tab on Joystick but no Receiver or MethodName available");
                    }
                }
                else
                {
                    tapCount = 1;
                }
                lastTapTime = Time.time;

                //set Joystick to fingertouchposition
                Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.lastTouchPosition);
                float dist = 0f;

                Vector3 currentPos = ray.GetPoint(dist);
                currentPos.z = 0;

				Vector3 newPos;
				_ConstraintToBound(currentPos, out newPos);

                if (centerOnPress)
                {
					userInitTouchPos = newPos;
                    foreach (UIWidget widget in widgetsToFade)
                    {
                        //TweenColor.Begin(widget.gameObject, 0.1f, Color.white).method = UITweener.Method.EaseIn;
                    }
                    foreach (Transform widgetTF in widgetsToCenter)
                    {
                        widgetTF.position = userInitTouchPos;
                    }
					OnDrag(Vector2.zero);
                }
                else
                {
                    userInitTouchPos = target.position;
                    OnDrag(Vector2.zero);
                }
            }
            else
            {
                ResetJoystick();
            }
        }
    }

    /// <summary>
    /// Drag the object along the plane.
    /// </summary>
    void OnDrag(Vector2 delta)
    {
		if (!mPressed)
			return;
        Ray ray = UICamera.currentCamera.ScreenPointToRay(UICamera.lastTouchPosition);
        float dist = 0f;

        Vector3 currentPos = ray.GetPoint(dist);
        Vector3 offset = currentPos - userInitTouchPos;

        if (offset.x != 0f || offset.y != 0f)
        {
            offset = target.InverseTransformDirection(offset);
            offset.Scale(scale);
            offset = target.TransformDirection(offset);
            offset.z = 0f;
        }

        target.position = userInitTouchPos + offset;

        Vector3 zeroZpos = target.position;
        zeroZpos.z = 0f;
        target.position = zeroZpos;

        // Calculate the length. This involves a squareroot operation,
        // so it's slightly expensive. We re-use this length for multiple
        // things below to avoid doing the square-root more than one.
        float length = target.localPosition.magnitude;

        if (length < deadZone)
        {
            // If the length of the vector is smaller than the deadZone radius,
            // set the position to the origin.
            position = Vector2.zero;
            target.localPosition = position;
        }
        else// if (length > radius)
        {
            target.localPosition = Vector3.ClampMagnitude(target.localPosition, radius);
            position = target.localPosition; // Note: This is bugged use the below unless you only want the position to update when the stick is at the edge (maximum)
        }

        // NOTE: If analogOutput always update the position even with tiny values
        if (analogOutput)
        {
            // NOTE: Update the position the bounds are done, (Not to big and not to small)
            position = target.localPosition;
        }


        if (normalize)
        {
            // Normalize the vector and multiply it with the length adjusted
            // to compensate for the deadZone radius.
            // This prevents the position from snapping from zero to the deadZone radius.
            position = position / radius * Mathf.InverseLerp(radius, deadZone, 1);
        }
    }
	
	void Update()
	{
		if( originPos == Vector3.zero )
			originPos = transform.position;

		if( circleSpeedUp != null )
			circleSpeedUp.color = position.magnitude / radius > GlobalConst.CONTROLLER_RAD ? colSpeedHighlight : colWhite;
	}
	
    void ResetJoystick()
    {
        // Release the finger control and set the joystick back to the default position
        tapCount = 0;
        position = Vector2.zero;
        target.position = userInitTouchPos;
        if (centerOnPress)
        {
            StartCoroutine(fadeOutJoystick());
			// Return to original pos
			foreach (Transform widgetTF in widgetsToCenter)
            	widgetTF.position = originPos;
        }
    }

    public void Disable()
    {
        gameObject.active = false;
    }
}

