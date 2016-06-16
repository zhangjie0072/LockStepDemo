using UnityEngine;
using System.Collections;

//u3d没有这个类，我这里加一个，手势需要用
//表示一个AA的2D椭圆
public class Ellipse2D {
	public Vector2 vPos = Vector2.zero;
	public Vector2 vExtends = new Vector2(2, 1);
	
	
	public void Set( Vector2 _vPos, Vector2 _vExtends ) {
		vPos = _vPos;
		vExtends = _vExtends;
	}
	
	public bool IsPointInside( Vector2 vPt ){
		Vector2 vDelta = vPt - vPos;
		Vector2 vTemp = new Vector2();
		vTemp.x = vDelta.x / vExtends.x;
		vTemp.y = vDelta.y / vExtends.y;
		
		return vTemp.sqrMagnitude < 1;
	}
	
	public Vector2 GetRandomPoint(){
		Vector2 vRand = Random.insideUnitCircle;
		Vector2 vRet = new Vector2();
		vRet.x = vRand.x * vExtends.x;
		vRet.y = vRand.y * vExtends.y;
		vRet += vPos;
		return vRet;
	}
	
	public Vector2 GetPointForDegree( int nDeg ) {
		float fDeg = nDeg;
		fDeg = Mathf.Repeat( fDeg, 360 );
		return vPos + new Vector2( Mathf.Cos( Mathf.Deg2Rad * fDeg ) * vExtends.x, Mathf.Sin( Mathf.Deg2Rad * fDeg ) * vExtends.y );
	}
	
	//判断与线段的碰撞
	public bool CheckCollision( Vector2 vLine1, Vector2 vLine2 ) {
		//先将vLine1, vLine2归一化（统统减去vPos除以vExtends）,再在新空间进行判定vLine1Norm--vLine2Norm是否与单位圆相交
		Vector2 vLine1Conv = new Vector2( (vLine1.x - vPos.x) / vExtends.x, (vLine1.y - vPos.y) / vExtends.y );
		Vector2 vLine2Conv = new Vector2( (vLine2.x - vPos.x) / vExtends.x, (vLine2.y - vPos.y) / vExtends.y );
		
		Vector2 vDelta12Norm = vLine1Conv - vLine2Conv;
		vDelta12Norm.Normalize();	//vDelta12Norm是线段的单位向量
		Vector2 vLineVertNorm = new Vector2( vDelta12Norm.y, -vDelta12Norm.x );	//vLineVertNorm是原点投影到线段的单位向量
		
		float fLineOffsetDist2Origin = Vector2.Dot( vLine1Conv, vLineVertNorm );
		if( fLineOffsetDist2Origin < 0 ){
			fLineOffsetDist2Origin = -fLineOffsetDist2Origin;
			vLineVertNorm *= -1;
		}
		//如果线段距离原点大于1，则绝对没有交点
		if( fLineOffsetDist2Origin >= 1 )
			return false;
				
		//判断点12分别在投影的两边，判定成功
		float fCross1 = GameUtils.Vector2Cross( vLine1Conv, vLineVertNorm );
		float fCross2 = GameUtils.Vector2Cross( vLine2Conv, vLineVertNorm );
		if( fCross1 * fCross2 < 0 )
			return true;
		
		//Cross同号，判断距离最近的点，再用最近的点判断与原点的距离
		if( Mathf.Abs(fCross1) > Mathf.Abs(fCross2) ) {
			return vLine2Conv.sqrMagnitude < 1;
		}
		else
			return vLine1Conv.sqrMagnitude < 1;
		
		return false;
	}
}
