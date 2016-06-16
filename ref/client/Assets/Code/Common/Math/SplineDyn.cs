using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//Spline which needs to add control point dynamically
//points are added from Start (index 0), and remove expired points from Tail
public class SplineDyn {
	
	//Q: Why use array not list? List handles runtime modification more effeciently
	//A: 用Array虽然修改的开销多了，但是免去了List[]的开销，而且方便回朔Index，总的来说不太亏
	Vector3[] arPos;
	
	int PointNumMax = 10;
	int PointNumCur = 0;
	
	int PointPushHistory = 0;

	public bool IsOK { get { 
			if( arPos == null )return false;
			return true;
		} 
	}
	
	public int PointNum { get {return PointNumCur;}}
	public int PointNumHistory { get {return PointPushHistory;}}
	
	// Clears current points
	public void SetPointNumMax( int n ){
		PointNumMax = n;
		arPos = new Vector3[n];
		PointNumCur = 0;
	}
	
	//依次移位
	public void PushFront( Vector3 v ){
		PointPushHistory++;
		
		if( PointNumCur == 0 ){
			arPos[0] = v;
			PointNumCur = 1;
			return;
		}
			
		if( PointNumCur < PointNumMax ){
			PointNumCur += 1;
		}
		
		for( int i=PointNumCur-1; i>0; i-- ){
			arPos[i] = arPos[i-1];
		}
		arPos[0] = v;
	}
	
	//输入，节点相对位置，0~~PointNum-1，输出3D位置
	public Vector3 GetPoint( float fLinePos ) {
		fLinePos = Mathf.Clamp( fLinePos, 0, (float)arPos.Length-1 );
		int nCurIndex = Mathf.FloorToInt(fLinePos);
		int nNextIndex = nCurIndex + 1;
		
		if( nCurIndex >= arPos.Length-1 )
			return arPos[arPos.Length-1];
		
		Vector3 v3NextPt = arPos[nNextIndex];
		Vector3 v3CurPt = arPos[nCurIndex];
		
		float t = fLinePos - nCurIndex;		//相对时间
		
		float t2 = t * t;
		float t3 = t2 * t;

		const float tension = 0.8f;	// 0.5 equivale a catmull-rom
		//求T1和T2
		Vector3 T1;
		{
			if( nCurIndex == 0 )
				T1 = tension * ( v3NextPt - v3CurPt );
			else{
				T1 = tension * ( v3NextPt - arPos[nCurIndex - 1] );
			}
		}
		
		Vector3 T2;
		{
			if( nNextIndex >= arPos.Length - 1 )
				T2 = tension * ( v3NextPt - v3CurPt );
			else{
				T2 = tension * ( arPos[nNextIndex+1] - v3CurPt );
			}
		}
		
		float Blend1 = 2 * t3 - 3 * t2 + 1;
		float Blend2 = -2 * t3 + 3 * t2;
		float Blend3 = t3 - 2 * t2 + t;
		float Blend4 = t3 - t2;

		return Blend1 * v3CurPt + Blend2 * v3NextPt + Blend3 * T1 + Blend4 * T2;
	}
}
