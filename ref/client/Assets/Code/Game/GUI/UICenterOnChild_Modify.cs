using UnityEngine;

//这个单元大部分Copy UICenterOnChild
//目的是微调
/*
[AddComponentMenu("GameUI/UICenterOnChild_Modify")]
public class UICenterOnChild_Modify : MonoBehaviour
{
	/// <summary>
	/// The strength of the spring.
	/// </summary>

	public float springStrength = 8f;
	public float recenter_protection_time = 0.2f;
	public float max_momentum_offset = 2;
	
	/// <summary>
	/// Callback to be triggered when the centering operation completes.
	/// </summary>

	public SpringPanel.OnFinished onFinished;

	UIDraggablePanel mDrag;
	GameObject mCenteredObject;
	float fEllapsSinceLastRecenter = 999;

	/// <summary>
	/// Game object that the draggable panel is currently centered on.
	/// </summary>

	public GameObject centeredObject { get { return mCenteredObject; } }

	void OnEnable () { 
		Recenter(); 
	}
	void OnDragFinished () { 
		Recenter();
	}

	/// <summary>
	/// Recenter the draggable list on the center-most child.
	/// </summary>
	
	void Update()	{
		fEllapsSinceLastRecenter += Time.deltaTime;
	}
	
	public void Recenter ()
	{
		if( fEllapsSinceLastRecenter < recenter_protection_time )
			return;
		fEllapsSinceLastRecenter = 0;
		if (mDrag == null)
		{
			mDrag = NGUITools.FindInParents<UIDraggablePanel>(gameObject);

			if (mDrag == null)
			{
				Debug.LogWarning(GetType() + " requires " + typeof(UIDraggablePanel) + " on a parent object in order to work", this);
				enabled = false;
				return;
			}
			else
			{
				mDrag.onDragFinished = OnDragFinished;
				
				if (mDrag.horizontalScrollBar != null)
					mDrag.horizontalScrollBar.onDragFinished = OnDragFinished;

				if (mDrag.verticalScrollBar != null)
					mDrag.verticalScrollBar.onDragFinished = OnDragFinished;
			}
		}
		if (mDrag.panel == null) return;

		// Calculate the panel's center in world coordinates
		Vector4 clip = mDrag.panel.clipRange;
		Transform dt = mDrag.panel.cachedTransform;
		Vector3 center = dt.localPosition;
		center.x += clip.x;
		center.y += clip.y;
		center = dt.parent.TransformPoint(center);

		// Offset this value by the momentum
		Vector3 vmmtoffset = mDrag.currentMomentum * (mDrag.momentumAmount * 0.1f);
		{
			float fmmtoffset_len = vmmtoffset.magnitude;
			if( Mathf.Abs( fmmtoffset_len ) > max_momentum_offset )	{
				//需要Clamp MMT Offset
				vmmtoffset = vmmtoffset * ( max_momentum_offset / fmmtoffset_len );
			}
		}
		
		Vector3 offsetCenter = center - vmmtoffset;
		mDrag.currentMomentum = Vector3.zero;

		float min = float.MaxValue;
		Transform closest = null;
		Transform trans = transform;

		// Determine the closest child
		for (int i = 0, imax = trans.childCount; i < imax; ++i)
		{
			Transform t = trans.GetChild(i);
			float sqrDist = Vector3.SqrMagnitude(t.position - offsetCenter);

			if (sqrDist < min)
			{
				min = sqrDist;
				closest = t;
			}
		}

		if (closest != null)
		{
			mCenteredObject = closest.gameObject;
			
			// Figure out the difference between the chosen child and the panel's center in local coordinates
			Vector3 cp = dt.InverseTransformPoint(closest.position);
			Vector3 cc = dt.InverseTransformPoint(center);
			Vector3 offset = cp - cc;

			// Offset shouldn't occur if blocked by a zeroed-out scale
			if (mDrag.scale.x == 0f) offset.x = 0f;
			if (mDrag.scale.y == 0f) offset.y = 0f;
			if (mDrag.scale.z == 0f) offset.z = 0f;

			// Spring the panel to this calculated position
			SpringPanel.Begin(mDrag.gameObject, dt.localPosition - offset, springStrength).onFinished = onFinished;
		}
		else mCenteredObject = null;
	}
	
	public void RecenterToGameObject( GameObject go )	{
		//基本上抄写上面函数的代码...
		Vector4 clip = mDrag.panel.clipRange;
		Transform dt = mDrag.panel.cachedTransform;
		Vector3 center = dt.localPosition;
		center.x += clip.x;
		center.y += clip.y;
		center = dt.parent.TransformPoint(center);
		
		mCenteredObject = go;
		LuaMgr.LuaOutputMsg( "Centered:" + mCenteredObject.name );
		
		// Figure out the difference between the chosen child and the panel's center in local coordinates
		Vector3 cp = dt.InverseTransformPoint(go.transform.position);
		Vector3 cc = dt.InverseTransformPoint(center);
		Vector3 offset = cp - cc;

		// Offset shouldn't occur if blocked by a zeroed-out scale
		if (mDrag.scale.x == 0f) offset.x = 0f;
		if (mDrag.scale.y == 0f) offset.y = 0f;
		if (mDrag.scale.z == 0f) offset.z = 0f;

		// Spring the panel to this calculated position
		SpringPanel.Begin(mDrag.gameObject, dt.localPosition - offset, springStrength).onFinished = onFinished;
	}
}
*/