using UnityEngine;
using System.Collections;

public class RootMotionComputer
{
	public		float		m_scale;
	public		Vector3		m_dirMove;

	private 	Vector3 	m_lastRootLocalPos = Vector3.zero;
	private 	Quaternion 	m_lastRootLocalRot = Quaternion.identity;
	private 	Quaternion 	m_initRootLocalRot = Quaternion.identity;
	private 	Quaternion 	m_initRotation = Quaternion.identity;
	private 	Quaternion 	m_initPlayerRot = Quaternion.identity;

	private 	Player		m_player;
	private		Quaternion	m_presetRot;

	public RootMotionComputer(Player player)
	{
		m_player = player;
		m_presetRot = Quaternion.AngleAxis(-90.0f, new Vector3(1.0f, 0.0f, 0.0f));
	}

	public void Reset()
	{
		m_scale = 1.0f;
		m_dirMove = Vector3.zero;
		
		m_lastRootLocalPos = Vector3.zero;
     	m_lastRootLocalRot = Quaternion.identity;
     	m_initRootLocalRot = Quaternion.identity;
     	m_initPlayerRot = Quaternion.identity;
	}

	public void DebugRootMotion()
	{
		m_player.model.root.localPosition = Vector3.zero;
	}

	public void ApplyRootMotion(float fDeltaTime)
	{
        //TODO


        //Vector3 curPosition = m_player.model.root.localPosition;
        //m_player.model.root.localPosition = Vector3.zero;

        //Vector3 deltaMove = Vector3.zero;
        //if( !Mathf.Approximately(m_scale, 0.0f) && !Mathf.Approximately(fDeltaTime, 0.0f) )
        //{
        //    deltaMove = (curPosition - m_lastRootLocalPos) / fDeltaTime * m_scale;
        //}

        //m_lastRootLocalPos = curPosition;
        //if( m_initRootLocalRot == Quaternion.identity )
        //{
        //    m_initRootLocalRot = m_player.model.root.localRotation;
        //    m_initPlayerRot = m_player.rotation.ToUnity();
        //}

        //float angleDelta = Mathf.DeltaAngle(m_lastRootLocalRot.eulerAngles.y, m_player.model.root.localRotation.eulerAngles.y);
        ////Logger.Log("angleDelta: " + angleDelta);
        //m_lastRootLocalRot = m_player.model.root.localRotation;
		
        ////m_player.m_root.localRotation = m_initRootLocalRot;
        //m_player.model.root.localRotation = m_presetRot;

        //m_player.rotation *= Quaternion.AngleAxis(angleDelta, Vector3.up);

        //if( m_dirMove != Vector3.zero )
        //    m_player.Move( fDeltaTime, Quaternion.LookRotation(m_dirMove) * deltaMove );
        //else
        //{
        //    m_player.Move(fDeltaTime, m_initPlayerRot * deltaMove);
        //}

        //Logger.Log(Time.frameCount + " ApplyRootMotion");
	}
}