using UnityEngine;
using System.Collections;

public class SparkEffect 
{
	public interface ISparkTarget
	{
		void RestoreMaterial();
		void SetSparkMaterial(Material mat);
	}

	private ISparkTarget	m_target;
	private Material		m_matSpark;
	private Material 		m_curMaterial;
	private GameUtils.Timer4View m_sparkTimer;

	private bool			m_enable = false;

	private bool			_orig;
	
	public SparkEffect(ISparkTarget target, float fInterval, int iCount = -1)
	{
		m_target = target;
		m_sparkTimer = new GameUtils.Timer4View(fInterval, _Spark, iCount);
		m_sparkTimer.stop = true;
		Shader shader = Shader.Find("Diffuse");
        m_matSpark = new Material(shader);
	}

	public void Update(float fDeltaTime)
	{
		m_sparkTimer.Update(fDeltaTime);
	}

	public void EnableSpark(bool bEnable)
	{
		if( !m_enable && bEnable )
		{
			m_target.SetSparkMaterial(m_matSpark);
			_orig = false;
			
			m_sparkTimer.stop = false;
			m_enable = true;
			m_sparkTimer.Reset();
		}
		else if( m_enable && !bEnable )
		{
			m_target.RestoreMaterial();
			_orig = true;

			m_sparkTimer.stop = true;
			m_enable = false;
		}
	}

	void _Spark()
	{
		if( !_orig )
		{
			m_target.RestoreMaterial();
			_orig = true;
		}
		else
		{
			m_target.SetSparkMaterial(m_matSpark);
			_orig = false;
		}
	}
}
