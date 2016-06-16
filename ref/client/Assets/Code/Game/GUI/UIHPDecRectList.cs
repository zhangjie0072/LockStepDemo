
using UnityEngine;
using System.Collections.Generic;

//该特制控件专门用于血条的辅助显示，该控件功能是显示HP减少时的动态区间
/*
[ExecuteInEditMode]
[AddComponentMenu("NGUI/UI/HPDecRectList")]
public class UIHPDecRectList : UISprite
{
	const int HP_BAR_TYPE_COUNT = 5;	//一共5种颜色的HPBar 
	
	//继承与Sprite的原因：Sprite是最简单的控件，UIAtlas.Sprite，Atlas，UV等变量皆可重用
	
	//每一次Dec发生时，都会创建一个DecRect
	class DecRect {
		public float mMin;	//小值， 表示Bar中的相对位置
		public float mMax;	//大值， 表示Bar中的相对位置
		public float mMinFrac;	//小值， 0~~1， 表示Bar中的相对位置的小数部分，用于显示
		public float mMaxFrac;	//大值， 0~~1， 表示Bar中的相对位置的小数部分
		public float mTime;	//该Rect的时间
	}
	
	//该Rect的材质由mSprite指定，
	//在初始到mFadeTime的时间段中，该Rect的颜色从mColorBegin 线性变化到  mColorFadeEnd
	//随后的时间中，该Rect的宽度开始减少，减少速度为mSlipSpeed，至宽度0为止，此时，单个Rect的生命周期结束
	
	[SerializeField] bool mRightZero = false;
	Queue<DecRect> mListRect = new Queue<DecRect>();

	//配置
	public float mBeforeFadeTime = 0.1f;		//保留在最初颜色的时间，单位：秒
	public float mFadeTime = 0.6f;		//由白色消逝到浅色的时间段， 单位：秒
	public float mSlipSpeed = 1;	//滑动消逝的时间段，单位：管/秒
	public Color mColorBegin = Color.white;			//最初的颜色
	//public Color mColorFadeEnd = new Color( 1, 1, 1, 0.37f );	//Fade结束后的颜色	
	public Color[] mColorFadeEnd = new Color[]{ 
		new Color( 0.5f, 0.3f, 0.2f, 0.5f), new Color(0.5f, 0.4f, 0.2f, 0.5f), new Color(0.3f, 0.5f, 0.1f, 0.5f), 
		new Color(0.1f, 0.3f, 0.5f, 0.5f), new Color(0.5f, 0.1f, 0.6f, 0.5f) };
	
	public bool RightZero
	{
		get
		{
			return mRightZero;
		}
		set
		{
			if (mRightZero != value)
			{
				mRightZero = value;
				mChanged = true;
			}
		}
	}
	
	
	override protected void OnUpdate( ){
		mChanged = false;
		
		if( mListRect.Count <= 0 )
			return;
		
		foreach( DecRect dr in mListRect ){
			dr.mTime += Time.deltaTime;
		}
		
		DecRect drHead = mListRect.Peek();
		if( drHead.mTime > mFadeTime + mBeforeFadeTime ) {
			drHead.mMax -= Time.deltaTime * mSlipSpeed;
			if( drHead.mMax <= drHead.mMin ){
				mListRect.Dequeue();
			}
			else{
				drHead.mMaxFrac = drHead.mMax - Mathf.Floor(drHead.mMax);
			}
		}
		mChanged = true;
		
	}
	
	
	override public void OnFill (BetterList<Vector3> verts, BetterList<Vector2> uvs, BetterList<Color32> cols)
	{

		foreach( DecRect dr in mListRect ){
			float fColorLerpRatio = (dr.mTime - mBeforeFadeTime) / mFadeTime;
			int nBar = Mathf.FloorToInt( dr.mMin ) % mColorFadeEnd.Length;
			fColorLerpRatio = Mathf.Clamp( fColorLerpRatio, 0f, 1f );
			Color clLerp = Color.Lerp( mColorBegin, mColorFadeEnd[nBar], fColorLerpRatio );
			
			if( mRightZero ) {
				//右到左	
				float u1 = mOuterUV.xMin + (mOuterUV.xMax - mOuterUV.xMin) * dr.mMinFrac;
				float u0 = mOuterUV.xMin + (mOuterUV.xMax - mOuterUV.xMin) * dr.mMaxFrac;
			
				verts.Add(new Vector3(1f-dr.mMinFrac,  0f, 0f));
				verts.Add(new Vector3(1f-dr.mMinFrac, -1f, 0f));
				verts.Add(new Vector3(1f-dr.mMaxFrac, -1f, 0f));
				verts.Add(new Vector3(1f-dr.mMaxFrac,  0f, 0f));
				
				uvs.Add(new Vector2(u1, mOuterUV.yMax ) );
				uvs.Add(new Vector2(u1, mOuterUV.yMin ) );
				uvs.Add(new Vector2(u0, mOuterUV.yMin ));
				uvs.Add(new Vector2(u0, mOuterUV.yMax ));
				

				cols.Add(clLerp);
				cols.Add(clLerp);
				cols.Add(clLerp);
				cols.Add(clLerp);
			}
			else{
				//左到右	
				float u0 = mOuterUV.xMin + (mOuterUV.xMax - mOuterUV.xMin) * dr.mMinFrac;
				float u1 = mOuterUV.xMin + (mOuterUV.xMax - mOuterUV.xMin) * dr.mMaxFrac;
			
				verts.Add(new Vector3(dr.mMinFrac,  0f, 0f));
				verts.Add(new Vector3(dr.mMinFrac, -1f, 0f));
				verts.Add(new Vector3(dr.mMaxFrac, -1f, 0f));
				verts.Add(new Vector3(dr.mMaxFrac,  0f, 0f));
				
				uvs.Add(new Vector2(u1, mOuterUV.yMax ) );
				uvs.Add(new Vector2(u1, mOuterUV.yMin ) );
				uvs.Add(new Vector2(u0, mOuterUV.yMin ));
				uvs.Add(new Vector2(u0, mOuterUV.yMax ));
				
				cols.Add(clLerp);
				cols.Add(clLerp);
				cols.Add(clLerp);
				cols.Add(clLerp);
			}
		}
		
		return;
	}
	
	public void PushHPDecRect( float fMin, float fMax ) {
		if( fMin + 0.001f >= fMax ) return;	//不允许min大于max，不允许太小的Rect出现
		
		//判断是不是跨了行
		if( (int)fMin != (int)fMax ){
			PushHPDecRect( Mathf.Floor(fMax), fMax );
			PushHPDecRect( fMin, Mathf.Floor(fMax) - 0.001f );
			return;
		}
		
		DecRect AddDR = new DecRect();
		AddDR.mMin = fMin;
		AddDR.mMax = fMax;
		AddDR.mMinFrac = fMin - Mathf.Floor(fMin);
		AddDR.mMaxFrac = fMax - Mathf.Floor(fMax);
		AddDR.mTime = 0;
		
		mListRect.Enqueue( AddDR );
		
		mChanged = true;
	}
	
	public void Clear() {
		mListRect.Clear();	
	}
	
}
*/