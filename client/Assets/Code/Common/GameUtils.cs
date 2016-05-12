using UnityEngine;
using System.Xml;
using System.Globalization;
using System.Collections.Generic;

using fogs.proto.msg;

public enum EDirection
{
    eForward = 1,
    eForwardRight = 2,
    eRight = 3,
    eBackRight = 4,
    eBack = 5,
    eBackLeft = 6,
    eLeft = 7,
    eForwardLeft = 8,
    eNone = 9,
}

public class GameUtils{
	
	public static Color red 	 = new Color(1.0f, 0.0f, 0.0f, 1.0f);
	public static Color yellow = new Color(1.0f, 0.85f, 0.0f, 1.0f);
	public static Color green  = new Color(0.02f, 0.94f, 0.1f, 1.0f);

	static public float HorizonalDistance( Vector3 lhs, Vector3 rhs )
	{
		Vector3 vDist = lhs - rhs;
		Vector3 hDist = new Vector3(vDist.x, 0.0f, vDist.z);
		return hDist.magnitude; 
	}

	static public Vector3 Convert(SVector3 val)
	{
		return new Vector3( (float)val.x, (float)val.y, (float)val.z );
	}

	static public SVector3 Convert(IM.Vector3 val)
	{
		SVector3 ret = new SVector3();
		ret.x = val.x.raw;
		ret.y = val.y.raw;
		ret.z = val.z.raw;
		return ret;
	}


	static public float String2Float( string s )	{
		float f = 0;
		float.TryParse( s, NumberStyles.Float, CultureInfo.InvariantCulture, out f );
	
		return f;
	}
	
	static public float[] String2FloatList( string s, char token = ','){
		if( string.IsNullOrEmpty( s ) )
			return new float[0];
		
		string[] v = s.Split(token);
		
		float[] f = new float[v.Length];
		
		for( int i=0; i<v.Length; i++){
			f[i] = 0;
			f[i] = float.Parse(v[i]); //CultureInfo.InvariantCulture);
		}
		
		return f;
	}
	
	static public int[] String2IntList( string s, char token = ','){
		if( string.IsNullOrEmpty( s ) )
			return new int[0];
		
		string[] v = s.Split( token );
		
		int[] f = new int[v.Length];
		
		for( int i=0; i<v.Length; i++){
			f[i] = 0;
			f[i] = int.Parse(v[i], CultureInfo.InvariantCulture);
		}
		
		return f;
	}
	
	static public Vector2 String2Vector2( string s, char token = ',' ) {
		float[] v = String2FloatList(s,token);
		
		if( v.Length != 2 )
			return Vector2.zero;
		
		return new Vector2( v[0], v[1] );
	}
	
	static public Vector3 String2Vector3( string s, char token = ',' ) {
		float[] v = String2FloatList(s, token);
		
		if( v.Length != 3 )
			return Vector3.zero;
		
		return new Vector3( v[0], v[1], v[2] );
	}
	
	static public string Vector22String( Vector2 v, char token = ',' ){
		return string.Format( "{0:f3}{1}{2:f3}", v.x, token, v.y );
	}
	static public string Vector32String( Vector3 v, char token = ',' ){
		return string.Format( "{0:f3}{1}{2:f3}{3}{4:f3}", v.x, token, v.y, token, v.z );
	}
	
	
	static public float Vector2Cross( Vector2 v1, Vector2 v2 ){
		return v1.x * v2.y - v1.y * v2.x;
	}
	
	static public Vector2 StripV3Y( Vector3 v )
	{
		return new Vector2(v.x, v.z);
	}

	static public Vector3 DummyV2Y( Vector2 v )
	{
		return new Vector3(v.x, 0.0f ,v.y);
	}
	
	static public Transform FindChildRecursive( Transform tr, string name ){
		if( tr.name == name) 
			return tr;
 
        for (int i = 0; i < tr.childCount; ++i)
        {
            Transform ret = FindChildRecursive( tr.GetChild(i), name);
            if (ret) 
				return ret;
        }
 
        return null;
	}
	
	static public void SetLayerRecursive( Transform tr, int iLayer )
	{
 		if( tr == null )
			return;
		tr.gameObject.layer = iLayer;
        for (int i = 0; i < tr.childCount; ++i)
           SetLayerRecursive( tr.GetChild(i), iLayer);
	}
	
	static public Bounds CreateBounds( Vector3 max, Vector3 min )
	{
		return new Bounds( (max + min)/2.0f, max - min);
	}
	
	static public T GetComponentInChild <T> ( Transform tr, string path )	where T : Component
	{
		Transform trchild = tr.Find( path );
		if( trchild == null )
			return null;
		
		return trchild.GetComponent<T>();
	}
	
	public class Timer
	{
		public float time { get { return m_timer; } }
		private	float m_timer = 0.0f;
		private float m_counter = 0.0f;
		
		public bool stop = false;
		
		public delegate void OnTimer();
		private OnTimer m_onTimer;

		private int		m_loopTime = 0;
		private int 	_loop = 0;
		private bool 	m_endless = false;

		public Timer(float interval, OnTimer timer, int loopTime = -1)
		{
			m_timer = interval;
			m_onTimer = timer;
			m_loopTime = loopTime;

			if( loopTime == -1 )
				m_endless = true;
			else
				m_loopTime = loopTime;
		}
		
		public void Reset()
		{
			m_counter = 0.0f;
			_loop = 0;
		}
		
		public void SetTimer(float time)
		{
			Reset();
			m_timer = time;
		}

		public float Remaining()
		{
			return m_timer - m_counter;
		}
		
		public void Update(float fDeltaTime)
		{
			if( stop )
				return;
			
			m_counter += fDeltaTime;
			if( m_counter > m_timer )
			{
				m_counter = 0.0f;
				m_onTimer();

				if( !m_endless )
				{
					_loop++;
					if( _loop >= m_loopTime )
						stop = true;
				}
			}
		}
	}

	public static Vector3 HorizonalNormalized( Vector3 lhs, Vector3 rhs )
	{
		Vector3 value = lhs - rhs;
		value.y = 0.0f;
		return value.normalized;
	}

	public static Vector3 RotateTowards(Vector3 from, Vector3 to, float step, bool bClockWise, Vector3 StartPos)
	{
		Vector3 vRotAxis = Vector3.up;
		vRotAxis *= bClockWise ? 1.0f : -1.0f;
		Quaternion rot = Quaternion.AngleAxis(step * Mathf.Rad2Deg, vRotAxis.normalized);
		Vector3 destTo = rot * from;
		{
			//StartPos.y = 1.0f;
			//Debug.DrawLine(StartPos, StartPos + destTo * 3.0f, Color.red);
			//Logger.Log("vRotAxis: " + vRotAxis );
		}
		return destTo;
	}

	public static void DrawSectors( Vector3 center, List<float> angleList, List<float> distanceList, int nonArcNum, Color color)
	{
		center.y = 0;
		
		Gizmos.color = color;
		if( angleList.Count == 0 || distanceList.Count == 0 )
			return;
		
		float radius = distanceList[1] * distanceList.Count;
		float nonArcDist = radius * Mathf.Cos(angleList[nonArcNum] * Mathf.Deg2Rad);

		// Draw ray
		int iCount = angleList.Count;
		for (int idx = 0; idx != nonArcNum; ++idx )
		{
			float angle = angleList[idx];
			float dist = nonArcDist / Mathf.Cos(angle * Mathf.Deg2Rad);
			Gizmos.DrawLine(center, center + Quaternion.AngleAxis(angle, Vector3.up) * Vector3.right * dist);
		}
		for (int idx = nonArcNum; idx != iCount - nonArcNum; idx++)
		{
			float angle = angleList[idx];
			Gizmos.DrawLine(center, center + Quaternion.AngleAxis(angle, Vector3.up) * Vector3.right * radius);
		}
		for (int idx = iCount - nonArcNum; idx <= iCount; ++idx )
		{
			float angle = idx == iCount ? 180f : angleList[idx];
			float dist = nonArcDist / Mathf.Cos((180 - angle) * Mathf.Deg2Rad);
			Gizmos.DrawLine(center, center + Quaternion.AngleAxis(angle, Vector3.up) * Vector3.right * dist);
		}

		// Draw arc
		Vector3 fromDir = Quaternion.AngleAxis(angleList[nonArcNum], Vector3.up) * Vector3.right;
		float angleRange = angleList[iCount - nonArcNum] - angleList[nonArcNum];
		iCount = distanceList.Count;
		for( int idx = 0; idx <= iCount; idx++ )
		{
			float distance = idx == iCount ? radius : distanceList[idx];
			DrawWireArc(center, Vector3.up, fromDir, angleRange, distance, color);
			float distHori = distance * Mathf.Cos(angleList[nonArcNum] * Mathf.Deg2Rad);
			Vector3 from = center + Vector3.right * distHori;
			Vector3 to = from - Vector3.forward * distHori * Mathf.Tan(angleList[nonArcNum] * Mathf.Deg2Rad);
			Gizmos.DrawLine(from, to);
			from.x = -from.x;
			to.x = -to.x;
			Gizmos.DrawLine(from, to);
		}
	}
	
	public static void DrawWireArc(Vector3 center, Vector3 axis, Vector3 from, float angle, float radius, Color color)
	{
		float fStep = 5.0f;
		int sectors = (int)(angle / fStep);
		fStep = angle / sectors;
		Vector3 newPos;
		
		List<Vector3> arcPoints = new List<Vector3>();
		for( int idx = 0; idx <= sectors; ++idx )
		{
			newPos = center + Quaternion.AngleAxis(fStep * idx, axis) * from * radius;
			arcPoints.Add(newPos);
		}
		Gizmos.color = color;
		int pointCnt = arcPoints.Count;
		for( int idx = 0; idx != pointCnt - 1; idx++ )
			Gizmos.DrawLine( arcPoints[idx], arcPoints[idx+1] );
	}

	public static void ReSkinning( GameObject goSkin, GameObject targetSkeleton )
	{
		SkinnedMeshRenderer[] skinMeshes = goSkin.GetComponentsInChildren<SkinnedMeshRenderer>();
		List<Transform> trBones = new List<Transform>();
        foreach( SkinnedMeshRenderer skin in skinMeshes )
		{
			skin.updateWhenOffscreen = true;
			skin.transform.parent = targetSkeleton.transform;

			foreach( Transform tr in skin.bones )
			{
				Transform trBoneOnRoot = GameUtils.FindChildRecursive(targetSkeleton.transform, tr.gameObject.name);
				if( trBoneOnRoot == null )
				{
					Logger.LogError("No bone: " + tr.gameObject.name + " matchted on item: " + skin.name);
					continue;
				}
				trBones.Add(trBoneOnRoot);
			}
			skin.bones = trBones.ToArray();
			trBones.Clear();
		}
	}



	public static bool CalcSolutionQuadraticFunc(out float x1, out float x2, float a, float b, float c)
	{
		x1 = x2 = 0.0f;

		if( Mathf.Approximately(a, 0.0f) )
		{
			if( Mathf.Approximately(b, 0.0f) )		
				return false;			// no solution but a point
			x1 = x2 = - c / b;
			return true;
		}
		else
		{
			float fDiscriminant = b * b - 4.0f * a * c;
			if(fDiscriminant < 0.0f)		
				return false;
			
			x1 = (-b - (float)Mathf.Sqrt(fDiscriminant)) / (2.0f * a);
			x2 = (-b + (float)Mathf.Sqrt(fDiscriminant)) / (2.0f * a);
			
			if(x1 > x2)
			{
				float t_X = x1;
				x1 = x2;
				x2 = t_X;
			}
			return true;
		}
	}

	public static Transform CombineSkin(Transform root)
	{
		float startTime = Time.realtimeSinceStartup;

		SkinnedMeshRenderer[] characterSkins = root.gameObject.GetComponentsInChildren<SkinnedMeshRenderer>();
		foreach( SkinnedMeshRenderer skin in characterSkins )
		{
			if( !skin.renderer.enabled )
				GameObject.DestroyImmediate(skin.gameObject);
		}

		// The SkinnedMeshRenderers that will make up a character will be
		// combined into one SkinnedMeshRenderers using one material.
		// This will speed up rendering the resulting character.
		// note:each SkinnedMeshRenderer must share a same material
		List<CombineInstance> combineInstances = new List<CombineInstance>();
		Material material = null;
		List<Transform> bones = new List<Transform>();
		Transform[] transforms = root.GetComponentsInChildren<Transform>();
		List<Texture2D> textures = new List<Texture2D>();
		int width = 0;
		int height = 0;
		
		int uvCount = 0;
		
		List<Vector2[]> uvList = new List<Vector2[]>();

		SkinnedMeshRenderer[] meshRenders = root.GetComponentsInChildren<SkinnedMeshRenderer>();
		foreach (SkinnedMeshRenderer smr in meshRenders)
		{
			if (material == null)
				material = GameObject.Instantiate(smr.sharedMaterial) as Material;
			for (int sub = 0; sub < smr.sharedMesh.subMeshCount; sub++)
			{
				CombineInstance ci = new CombineInstance();
				ci.mesh = smr.sharedMesh;
				ci.subMeshIndex = sub;
				combineInstances.Add(ci);
			}
			
			uvList.Add(smr.sharedMesh.uv);
			uvCount += smr.sharedMesh.uv.Length;
			
			if (smr.material.mainTexture != null)
			{
				textures.Add(smr.renderer.material.mainTexture as Texture2D);
				width += smr.renderer.material.mainTexture.width;
				height += smr.renderer.material.mainTexture.height;
			}
			
			// we need to recollect references to the bones we are using
			foreach (Transform bone in smr.bones)
			{
				foreach (Transform transform in transforms)
				{
					if (transform.name != bone.name) continue;
					bones.Add(transform);
					break;
				}
			}
			Object.Destroy(smr.gameObject);
		}
		
		// Obtain and configure the SkinnedMeshRenderer attached to
		// the character base.
		SkinnedMeshRenderer r = root.gameObject.GetComponent<SkinnedMeshRenderer>();
		if (!r)
			r = root.gameObject.AddComponent<SkinnedMeshRenderer>();
		
		r.sharedMesh = new Mesh();
		
		//only set mergeSubMeshes true will combine meshs into single submesh
		r.sharedMesh.CombineMeshes(combineInstances.ToArray(), true, false);
		r.bones = bones.ToArray();
		r.material = material;

		//int POT_W = Mathf.NextPowerOfTwo(width);
		//int POT_H = Mathf.NextPowerOfTwo(height);

		Texture2D skinnedMeshAtlas = new Texture2D(512, 512);
		Rect[] packingResult = skinnedMeshAtlas.PackTextures(textures.ToArray(), 0);
		Vector2[] atlasUVs = new Vector2[uvCount];
		
		//as combine textures into single texture,so need recalculate uvs
		
		int j = 0;        
		for (int i = 0; i < uvList.Count; i++)
		{
			foreach (Vector2 uv in uvList[i])
			{
				atlasUVs[j].x = Mathf.Lerp(packingResult[i].xMin, packingResult[i].xMax, uv.x);
				atlasUVs[j].y = Mathf.Lerp(packingResult[i].yMin, packingResult[i].yMax, uv.y);
				j++;
			}
		}
		
		r.material.mainTexture = skinnedMeshAtlas;
		r.sharedMesh.uv = atlasUVs;

		r.updateWhenOffscreen = true;
		
		Logger.Log("combine meshes takes : " + (Time.realtimeSinceStartup - startTime) * 1000 + " ms");
		return root;
	}

	static public void AngleToDir(float fAngle, out int dir, out Vector3 vel)
	{
		dir = 0;
		vel = Vector3.zero;

		fAngle = Mathf.Abs(fAngle);
		dir = (int)(fAngle / GlobalConst.ROTATE_ANGLE_SEC);
		vel	= Quaternion.Euler(0.0f, dir * GlobalConst.ROTATE_ANGLE_SEC, 0.0f) * Vector3.forward;
	}

	static public void AngleToDir(float fAngle, out EDirection dir, out Vector3 vel)
	{
		dir = EDirection.eNone;
		vel = Vector3.zero;

		fAngle = Mathf.Abs(fAngle);
		if( fAngle > 337.5f || fAngle <= 22.5f )
		{
			vel = Vector3.forward;
			dir = EDirection.eForward;
		}
		else if( fAngle > 22.5f && fAngle <= 67.5f )
		{
			vel = Quaternion.Euler(0.0f, 45.0f, 0.0f) * Vector3.forward;
			dir = EDirection.eForwardRight;
		}
		else if( fAngle > 67.5f && fAngle <= 112.5f )
		{
			vel = Vector3.right;
			dir = EDirection.eRight;
		}
		else if( fAngle > 112.5f && fAngle <= 157.5f )
		{
			vel = Quaternion.Euler(0.0f, 135.0f, 0.0f) * Vector3.forward;
			dir = EDirection.eBackRight;
		}
		else if( fAngle > 157.5f && fAngle <= 202.5f )
		{
			vel = Vector3.back;
			dir = EDirection.eBack;
		}
		else if( fAngle > 202.5f && fAngle <= 247.5f )
		{
			vel = Quaternion.Euler(0.0f, 225.0f, 0.0f) * Vector3.forward;
			dir = EDirection.eBackLeft;
		}
		else if( fAngle > 247.5f && fAngle <= 292.5f )
		{
			vel = Vector3.left;
			dir = EDirection.eLeft;
		}
		else if( fAngle > 292.5f && fAngle <= 337.5f )
		{
			vel = Quaternion.Euler(0.0f, 315.0f, 0.0f) * Vector3.forward;
			dir = EDirection.eForwardLeft;
		}
	}
	static public void DirToAngle(EDirection dir, out Vector3 vel)
	{
		vel = Vector3.zero;
		switch(dir)
		{
		case EDirection.eForward:
			vel = Vector3.forward;
			break;
		case EDirection.eForwardRight:
			vel = Quaternion.Euler(0.0f, 45.0f, 0.0f) * Vector3.forward;
			break;
		case EDirection.eRight:
			vel = Vector3.right;
			break;
		case EDirection.eBackRight:
			vel = Quaternion.Euler(0.0f, 135.0f, 0.0f) * Vector3.forward;
			break;
		case EDirection.eBack:
			vel = Vector3.back;
			break;
		case EDirection.eBackLeft:
			vel = Quaternion.Euler(0.0f, 225.0f, 0.0f) * Vector3.forward;
			break;
		case EDirection.eLeft:
			vel = Vector3.left;
			break;
		case EDirection.eForwardLeft:
			vel = Quaternion.Euler(0.0f, 315.0f, 0.0f) * Vector3.forward;
			break;
		default:
			break;
		}
	}
    //Lua string.len 不能准确测出中文长度
    static public int GetStringLength(string str) 
    {
        return str.Length;
    }
}

public class XmlUtils{
		
	static public int XmlGetAttr_Int( XmlElement el, string sAttrName, int nDefault ){
		string sRet = el.GetAttribute(sAttrName);
		if( string.IsNullOrEmpty( sRet ) )
			return nDefault;
		return int.Parse(sRet);
	}
	static public int XmlGetAttr_Int( XmlElement el, string sAttrName ){ return XmlGetAttr_Int(el,sAttrName,0); }
	
	static public string XmlGetAttr_String( XmlElement el, string sAttrName, string strDefault ){
		string sRet = el.GetAttribute(sAttrName);
		if( string.IsNullOrEmpty( sRet ) )
			return strDefault;
		return sRet;
	}
	static public string XmlGetAttr_String( XmlElement el, string sAttrName ){ return XmlGetAttr_String(el,sAttrName,""); }
	
	static public float XmlGetAttr_Float( XmlElement el, string sAttrName, float fDefault ){
		string sRet = el.GetAttribute(sAttrName);
		if( string.IsNullOrEmpty( sRet ) )
			return fDefault;
		float ret = float.Parse( sRet, CultureInfo.InvariantCulture );
		return ret;
	}
	static public float XmlGetAttr_Float( XmlElement el, string sAttrName ){ return XmlGetAttr_Float(el,sAttrName,0); }
	
	static public Vector2 XmlGetAttr_Vec2( XmlElement el ){
		return new Vector2(XmlGetAttr_Float(el,"x"), XmlGetAttr_Float(el,"y"));
	}
	
	static public Vector3 XmlGetAttr_Vec3( XmlElement el ){
		Vector2 vec2 = XmlGetAttr_Vec2(el);
		return new Vector3(vec2.x, vec2.y, XmlGetAttr_Float(el,"z"));
	}
}


public struct ValueRange	{
	public float v1;
	public float v2;
	
	public void Set( float v1_ )	{
		v1 = v1_;
		v2 = v1_;
	}
	public void Set( float v1_, float v2_ )	{
		v1 = v1_;
		v2 = v2_;
	}
	
	
	public float RandValue( )	{
		return Random.Range( v1, v2 );
	}
	
	static public ValueRange Parse( string s, float fDefault )	{
		ValueRange ret = new ValueRange();
		float[] arF = GameUtils.String2FloatList( s );
		if( arF.Length == 1 )	{
			ret.Set( arF[0] );
		}
		else if( arF.Length >=2 )	{
			ret.Set( arF[0], arF[1] );
		}
		else 
			ret.Set( fDefault );
		
		return ret;
	}
}

/*
public class UIUtils	{
	public static void DraggablePanelShowChild( GameObject goContainer, GameObject goChild, float fChildSize )	{
		UIDraggablePanel mDrag = NGUITools.FindInParents<UIDraggablePanel>( goContainer );
		Vector4 clip = mDrag.panel.clipRange;
		Transform dt = mDrag.panel.cachedTransform;
		Vector3 center = dt.localPosition;
		center.x += clip.x;
		center.y += clip.y;
		center = dt.parent.TransformPoint(center);
		
		LuaMgr.LuaOutputMsg( "DraggablePanelShowChild:" + goChild.name );
		
		// Figure out the difference between the chosen child and the panel's center in local coordinates
		Vector3 cp = dt.InverseTransformPoint( goChild.transform.position);
		Vector3 cc = dt.InverseTransformPoint( center );
		Vector3 offset = cp - cc;

		// Offset shouldn't occur if blocked by a zeroed-out scale
		if (mDrag.scale.x == 0f) offset.x = 0f;
		if (mDrag.scale.y == 0f) offset.y = 0f;
		if (mDrag.scale.z == 0f) offset.z = 0f;
		
		if( offset.x > 0 )	{
			offset.x = Mathf.Max( 0, offset.x - clip.z * 0.5f + fChildSize );
		}
		else if( offset.x < 0 )	{
			offset.x = Mathf.Min( 0, offset.x + clip.z * 0.5f - fChildSize );
		}
		
		if( offset.y > 0 )	{
			offset.y = Mathf.Max( 0, offset.y - clip.w * 0.5f + fChildSize );
		}
		else if( offset.y < 0 )	{
			offset.y = Mathf.Min( 0, offset.y + clip.w * 0.5f - fChildSize );
		}
		
		// Spring the panel to this calculated position
		SpringPanel.Begin(mDrag.gameObject, dt.localPosition - offset, 8 );
	}
	
	public static float StandardPix2ActualPix( float pix )	{
		return pix / 640f * Screen.height;
	}
}
*/	
