using UnityEngine;
using System.Collections;

//预先确定了节点的Spline曲线
[System.Serializable]
public class Spline {
	public Vector3[] arPos;
	
	public bool IsOK { get { 
			if( arPos == null )return false;
			if( arPos.Length < 2 ) return false;
			return true;
		} 
	}
	
	public int PointNum { get {return arPos.Length;}}
	
	public void SetSplinePoints( Vector3[] pts ) {
		arPos = pts;
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

		float tension = 0.5f;	// 0.5 equivale a catmull-rom
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
