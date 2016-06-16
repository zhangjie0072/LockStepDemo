
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InputManager 
{
	public interface Listener	{
		void OnPress( int nTouchID, Vector2 vScreenPt, bool bDown, out bool bPassThrough );
		void OnClick( int nTouchID, Vector2 vScreenPt, out bool bPassThrough );
		void OnDrag( int nTouchID, Vector2 vScreenPtCur, Vector2 vScreenPtDelta, out bool bPassThrough );
		void OnDragEnd( int nTouchID );
	}
	
	public Vector2 mHVDirection {get; set;}
	
	public UIJoystick mJoystick;
    public bool isNGDS = false;
    public bool sendShock = false;
	
	public bool m_CmdBtn1Click = false;
	public bool m_CmdBtn1Released = false;
	public bool m_CmdBtn2Click = false;
	public bool m_CmdBtn2Released = false;
	public bool m_CmdBtn3Click = false;
	public bool m_CmdBtn4Click = false;
	public bool m_CmdBtn5Click = false;

	public bool m_CmdBtnTestClick = false;

	public bool m_ShiftClick = false;
	
	public float MinDragPixOffset = 10;
	
	const int PROC_TOUCH_COUNT = 4;
	
	List<Listener> lstListener = new List<Listener>();
	
	struct InputTouchStatus{
		public Vector2 touchbegin;
		public Vector2 lasttouch;
		public bool dragging;
		
		public void Reset( ){
			dragging = false;
		}
	}
	InputTouchStatus[] arTouchBegin = new InputTouchStatus[PROC_TOUCH_COUNT];//Index = touchID 记录Touch开始的点
	
	public InputManager()
	{
		Reset();
	}
	
	public void Reset()
	{
		mHVDirection = Vector2.zero;
		m_CmdBtn1Click = m_CmdBtn2Click = m_CmdBtn3Click = m_CmdBtn4Click = m_CmdBtn5Click = m_CmdBtnTestClick =false;
	}
	
	public void Update( )
	{
		float horizontal, vertical = 0.0f;
		
#if !UNITY_IPHONE && !UNITY_ANDROID
		//Windows 和 编辑器	
		Vector2 vMousePos = Input.mousePosition;
		
		if( Input.GetMouseButtonDown( 0 ) ){
			_InvokePress( 0, true, vMousePos );
			arTouchBegin[0].touchbegin = vMousePos;
		}
		
		if( Input.GetMouseButtonUp( 0 ) ){
			_InvokePress( 0, false, vMousePos );
			
			if( arTouchBegin[0].dragging == false )	{
				_InvokeClick( 0, vMousePos );
			}
			else {
				_InvokeDragEnd( 0 );
			}
			arTouchBegin[0].Reset();
		}
		
		if( Input.GetMouseButton( 0 ) )	{
			//从非Drag跳转到Drag
			if( arTouchBegin[0].dragging == false )	{
				Vector2 vDelta = vMousePos - arTouchBegin[0].touchbegin;
				if( vDelta.sqrMagnitude > MinDragPixOffset*MinDragPixOffset )	{
					arTouchBegin[0].dragging = true;
				} 
			}
			
			if( arTouchBegin[0].dragging )	{
				Vector2 vLastTouch = arTouchBegin[0].lasttouch;
				arTouchBegin[0].lasttouch = vMousePos;
				_InvokeDrag( 0, vMousePos, vMousePos - vLastTouch );
			}

			
		}
		mHVDirection = new Vector2( Input.GetAxis("Horizontal"), Input.GetAxis("Vertical") );

        bool state_J = Input.GetKey(KeyCode.J);
        if (m_CmdBtn1Click && !state_J)
        {
            m_CmdBtn1Released = true;
        }
		m_CmdBtn1Click = state_J;
		bool state_K = Input.GetKey(KeyCode.K);
		if (m_CmdBtn2Click && !state_K)
		{
			m_CmdBtn2Released = true;
		}
		m_CmdBtn2Click = state_K;
		m_CmdBtn3Click = Input.GetKey(KeyCode.L);
		m_CmdBtn4Click = Input.GetKey(KeyCode.I);
		m_CmdBtn5Click = Input.GetKey(KeyCode.O);

		m_CmdBtnTestClick = Input.GetKey(KeyCode.M);

		//m_ShiftClick = Input.GetKey(KeyCode.LeftShift);
#else
        if (mJoystick != null && !isNGDS)
			mHVDirection = mJoystick.position / mJoystick.radius;

        //移动平台
        for (int i = 0; i < Mathf.Min(PROC_TOUCH_COUNT, Input.touchCount); i++)
        {
            Touch tTemp = Input.GetTouch(i);

            if (tTemp.phase == TouchPhase.Began)
            {
                _InvokePress(tTemp.fingerId, true, tTemp.position);
                arTouchBegin[tTemp.fingerId].touchbegin = tTemp.position;
            }

            if (tTemp.phase == TouchPhase.Ended)
            {
                _InvokePress(tTemp.fingerId, false, tTemp.position);

                if (arTouchBegin[tTemp.fingerId].dragging == false)
                {
                    _InvokeClick(tTemp.fingerId, tTemp.position);
                }
                else
                {
                    _InvokeDragEnd(tTemp.fingerId);
                }
                arTouchBegin[tTemp.fingerId].Reset();
                continue;
            }

            if (arTouchBegin[tTemp.fingerId].dragging == false)
            {
                Vector2 vDelta = tTemp.position - arTouchBegin[tTemp.fingerId].touchbegin;
                if (vDelta.sqrMagnitude > MinDragPixOffset * MinDragPixOffset)
                {
                    arTouchBegin[tTemp.fingerId].dragging = true;
                }
            }

            if (arTouchBegin[tTemp.fingerId].dragging)
            {
                Vector2 vLastTouch = arTouchBegin[tTemp.fingerId].lasttouch;
                arTouchBegin[tTemp.fingerId].lasttouch = tTemp.position;
                _InvokeDrag(tTemp.fingerId, tTemp.position, tTemp.position - vLastTouch);
            }
        }
#endif
		
	}
	
	void _InvokeClick( int nTouchID, Vector2 vPos )	{
		bool bPassThrough = true;
		foreach( Listener lsn in lstListener )	{
			lsn.OnClick( nTouchID, vPos, out bPassThrough );
			if( bPassThrough == false )
				break;
		}
	}
	
	void _InvokePress( int nTouchID, bool bDown, Vector2 vPos )	{
		bool bPassThrough = true;
		foreach( Listener lsn in lstListener )	{
			lsn.OnPress( nTouchID, vPos, bDown, out bPassThrough );
			if( bPassThrough == false )
				break;
		}
	}

	void _InvokeDrag( int nTouchID, Vector2 vPos, Vector2 vDelta )	{
		bool bPassThrough = true;
		foreach( Listener lsn in lstListener )	{
			lsn.OnDrag( nTouchID, vPos, vDelta, out bPassThrough );
			if( bPassThrough == false )
				break;
		}
	}

	void _InvokeDragEnd( int nTouchID )	{
		foreach( Listener lsn in lstListener )	{
			lsn.OnDragEnd( nTouchID );
		}
	}

	
	public void AddListener( Listener ls )	{
		if( lstListener.Contains( ls ) == false )
			lstListener.Add( ls );
	}
	
	public void RemoveListener( Listener ls )	{
		lstListener.Remove( ls );
	}
	
	public void RemoveAllListener( )	{
		lstListener.Clear();
	}
}