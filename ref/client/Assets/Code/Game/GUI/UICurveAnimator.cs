using UnityEngine;
using System.Collections;

public class UICurveAnimator : MonoBehaviour {
	
	float mTime = 0;
	float mTotalTime = 1;
	
	public bool mEnableTranslate = true;
	public AnimationCurve mCurveX = new AnimationCurve( new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 0f) } );
	public AnimationCurve mCurveY = new AnimationCurve( new Keyframe[] { new Keyframe(0f, 0f), new Keyframe(1f, 0f) } );

	public bool mEnableSize = true;
	public AnimationCurve mCurveSizeX = new AnimationCurve( new Keyframe[] { new Keyframe(0f, 1f), new Keyframe(1f, 1f) } );
	public AnimationCurve mCurveSizeY = new AnimationCurve( new Keyframe[] { new Keyframe(0f, 1f), new Keyframe(1f, 1f) } );

	public bool mEnableAlpha = true;
	public AnimationCurve mCurveAlpha = new AnimationCurve( new Keyframe[] { new Keyframe(0f, 1f), new Keyframe(1f, 1f) } );

	public GameObject	mRoot;

	UIWidget 	mUIWidget;
	UILabel		mUILabel;

	void Start()
	{
		mCurveX.postWrapMode = WrapMode.Clamp;
		mCurveY.postWrapMode = WrapMode.Clamp;
		mCurveSizeX.postWrapMode = WrapMode.Clamp;
		mCurveSizeY.postWrapMode = WrapMode.Clamp;
		mCurveAlpha.postWrapMode = WrapMode.Clamp;

		if( mEnableTranslate )
		{
			mTotalTime = Mathf.Max( mTotalTime,mCurveX.keys[mCurveX.length-1].time );
			mTotalTime = Mathf.Max( mTotalTime,mCurveY.keys[mCurveY.length-1].time );
		}
		
		if( mEnableSize )
		{
			mTotalTime = Mathf.Max( mTotalTime,mCurveSizeX.keys[mCurveSizeX.length-1].time );
			mTotalTime = Mathf.Max( mTotalTime,mCurveSizeY.keys[mCurveSizeY.length-1].time );
		}
		
		if( mEnableAlpha )
			mTotalTime = Mathf.Max( mTotalTime,mCurveAlpha.keys[mCurveAlpha.length-1].time );
		
		if( mUIWidget == null )
			mUIWidget = GetComponentInChildren<UIWidget>();

		if( mUILabel == null )
			mUILabel = GetComponentInChildren<UILabel>();
	}
	
	void Update() {
		mTime += Time.deltaTime;
		if( mTime > mTotalTime ){
			//gameObject.SetActive( false );
			if( mRoot != null )
				GameObject.Destroy( mRoot );
			else
				GameObject.Destroy( gameObject );

			return;
		}
		
		if( mEnableTranslate )
		{
			transform.localPosition = new Vector3( mCurveX.Evaluate(mTime), mCurveY.Evaluate(mTime), 0 );
		}
		
		if(mEnableSize)
		{
			transform.localScale = new Vector3( mCurveSizeX.Evaluate(mTime), mCurveSizeY.Evaluate(mTime), 1 );
		}
		
		if(mEnableAlpha)
		{
			Color clOld = mUIWidget.color;
			mUIWidget.color = new Color(clOld.r, clOld.g, clOld.b, mCurveAlpha.Evaluate(mTime) );

			if( mUILabel != null )
			{
				Color clOldEffect = mUILabel.effectColor;
				mUILabel.effectColor = new Color(clOldEffect.r, clOldEffect.g, clOldEffect.b, mCurveAlpha.Evaluate(mTime) );
			}
		}
	}
	
	public void SetCurTime( float t ){ mTime = t;}
	public float GetCurTime() {return mTime;}
}
