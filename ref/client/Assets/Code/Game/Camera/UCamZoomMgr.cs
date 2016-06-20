using UnityEngine;
using System.Collections;

public enum ZoomType
{
	eMatch = 0,
	eGoal,
	ePlayerCloseUp,
	eMax = 3
}

[System.Serializable]
public class ZoomParam
{
	public float	m_fZoomSpeed = 3.0f;
	public float	m_fYDistToTarget = 5.0f;
	public float	m_fZDistToTarget = 7.0f;
	public float	m_fTargetAngle = 30.0f;
	public float    m_fTimeDelay = 0f;
	public float	m_fTimeZoomStay = 1.5f;
}

public class UCamZoomMgr : MonoBehaviour 
{
	[HideInInspector]
	public 	bool		m_bZoomState{ get; private set;}

	public	ZoomParam[]	m_zoomParam;

	private Transform	m_zoomTarget;
	private float		m_fDeg;

	private float		m_fXSpeed;
	private float		m_fYSpeed;
	private float		m_fZSpeed;

	private ZoomParam 	m_curZoomParam;
    private GameUtils.Timer4View m_timerDelay;
    private GameUtils.Timer4View m_timerStay;

	public void Awake()
	{
		m_bZoomState = false;
	}

	public void SetZoom( Transform target, ZoomType type )
	{
		m_zoomTarget = target;
		m_curZoomParam = m_zoomParam[(int)type];

		if( m_timerDelay != null )
		{
			m_timerDelay.stop = true;
			m_timerDelay.Reset();
		}
		m_timerDelay = new GameUtils.Timer4View(m_curZoomParam.m_fTimeDelay, Init, 1);
	}

	public void ReleaseZoom()
	{
		m_bZoomState = false;
	}

	void Init()
	{
		m_timerDelay.stop = true;

		m_bZoomState = true;

		if( m_timerStay != null )
		{
			m_timerStay.stop = true;
			m_timerStay.Reset();
		}
		m_timerStay = new GameUtils.Timer4View(m_curZoomParam.m_fTimeZoomStay, ReleaseZoom, 1);

		float fDistToMoveZ = Mathf.Abs(m_zoomTarget.position.z - m_curZoomParam.m_fZDistToTarget - transform.position.z);

		float fZoomTime = fDistToMoveZ / m_curZoomParam.m_fZoomSpeed;

		float fDistToMoveY = Mathf.Abs(m_curZoomParam.m_fYDistToTarget - transform.position.y);
		m_fYSpeed = fDistToMoveY / fZoomTime;

		float fCurAngle = Vector3.Angle(Vector3.forward, transform.forward);
		m_fDeg = (m_curZoomParam.m_fTargetAngle - fCurAngle) / fZoomTime;
	}

	void Update()
	{
		if( m_timerDelay != null )
			m_timerDelay.Update(Time.deltaTime);
	}
	
	public void OnUpdate (float fDeltaTime) 
	{
		if( !m_bZoomState )
			return;

		if( m_timerStay != null )
			m_timerStay.Update(fDeltaTime);

		float fZPosition = transform.position.z;
		float fDistToMoveZ = m_zoomTarget.position.z - m_curZoomParam.m_fZDistToTarget - transform.position.z;
		if( fDistToMoveZ > 0.0f )
			fZPosition += m_curZoomParam.m_fZoomSpeed * fDeltaTime;
		else
		{
			if( m_timerStay != null )
				m_timerStay.stop = false;
		}

		float fYPosition = transform.position.y;
		float fDistToMoveY = transform.position.y - m_curZoomParam.m_fYDistToTarget;
		if( fDistToMoveY > 0.0f )
			fYPosition -= m_fYSpeed * fDeltaTime;
		//float fXPosition = Mathf.Lerp(transform.position.x, m_zoomTarget.transform.position.x, 0.5f);
		float fXPosition = Mathf.MoveTowards(transform.position.x, m_zoomTarget.transform.position.x, 0.1f);
		transform.position = new Vector3( fXPosition, fYPosition, fZPosition );

		float fCurAngle = Vector3.Angle(Vector3.forward, transform.forward);
		float fNextAngle = fCurAngle + m_fDeg * fDeltaTime;
		float fDeltaAngle = (fNextAngle - m_curZoomParam.m_fTargetAngle);

		if( fDeltaAngle > 0.0f )
			transform.rotation = Quaternion.Euler(fNextAngle, 0.0f, 0.0f);
	}
}