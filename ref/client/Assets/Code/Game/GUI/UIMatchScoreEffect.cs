using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIMatchScoreEffect : MonoBehaviour
{
	class ScoreEffect : MonoBehaviour
	{
		public GameObject			m_goScoreEffect;
		public UIMatchScoreEffect	m_effect;
		public bool					m_done = false;

		void Awake()
		{
			m_done = false;
		}

		public void Play()
		{
			Vector3 viewPos = Camera.main.WorldToViewportPoint( m_effect.m_initPos );
			Vector3 screenPos = UIManager.Instance.m_uiCamera.GetComponent<Camera>().ViewportToWorldPoint(viewPos);

			m_goScoreEffect = NGUITools.AddChild(m_effect.m_uiMatch.gameObject, m_effect.m_scoretoboardEffect);
			NGUITools.SetLayer(m_goScoreEffect, m_goScoreEffect.layer);

			m_goScoreEffect.transform.position = screenPos;
			Vector3 pos = m_goScoreEffect.transform.localPosition;
			pos.x = Mathf.FloorToInt(pos.x);
			pos.y = Mathf.FloorToInt(pos.y);
			pos.z = 0.0f;
			m_goScoreEffect.transform.localPosition = pos;
			m_goScoreEffect.GetComponent<Renderer>().material.renderQueue = RenderQueue.ParticleOnGui;
			Renderer[] renderers = m_goScoreEffect.transform.GetComponentsInChildren<Renderer>();
			foreach( Renderer renderer in renderers )
				renderer.material.renderQueue = RenderQueue.ParticleOnGui;

			ParticleSystem ps = m_goScoreEffect.GetComponent<ParticleSystem>();
			UEffectSelfDestroy us = m_goScoreEffect.AddComponent<UEffectSelfDestroy>();
			ps.Play();

			_InitToExploded();
		}

		void _InitToExploded()
		{
			Vector3 targetPos = m_goScoreEffect.transform.localPosition;
			targetPos.x += Random.Range(-100.0f, 100.0f);
			targetPos.y += Random.Range(-200.0f, 0.0f);
			targetPos.z = 0.0f;

			TweenPosition tween = TweenPosition.Begin(m_goScoreEffect, m_effect.m_initToExplodedTime, targetPos);
			tween.method = UITweener.Method.EaseOut;
			tween.SetOnFinished(new EventDelegate(_ExplodedToEndFunc));
		}

		void _ExplodedToEndFunc()
		{
			StartCoroutine(_ExplodedToEnd());
		}

		IEnumerator _ExplodedToEnd()
		{
			yield return new WaitForSeconds(m_effect.m_explodedWaitingTime);

			TweenPosition tween = TweenPosition.Begin(m_goScoreEffect, m_effect.m_explodedToEndTime, m_effect.m_endPos);
			tween.method = UITweener.Method.EaseOut;
			tween.SetOnFinished(new EventDelegate(_OnArriveEnd));
		}

		void _OnArriveEnd()
		{
			ParticleSystem ps = m_goScoreEffect.GetComponent<ParticleSystem>();
			ps.Stop(); 
			GameObject goScoreEffect = NGUITools.AddChild(m_effect.m_uiMatch.gameObject, m_effect.m_scoretoboardEffect1);
			UEffectSelfDestroy destroy = goScoreEffect.AddComponent<UEffectSelfDestroy>();
			destroy.onFinish = _OnFinish;

			goScoreEffect.transform.localPosition = ps.transform.localPosition;
			ps = goScoreEffect.GetComponent<ParticleSystem>();

			NGUITools.SetLayer(goScoreEffect, goScoreEffect.layer);
			goScoreEffect.GetComponent<Renderer>().material.renderQueue = RenderQueue.ParticleOnGui;
			ps.Play();
		}

		void _OnFinish()
		{
			m_done = true;
		}
	}

	private float m_initToExplodedTime = 0.1f;
	private float m_explodedWaitingTime = 0.3f;
	private float m_explodedToEndTime = 0.4f;

	private GameObject m_scoretoboardEffect;
	private GameObject m_scoretoboardEffect1;

	private Vector3 m_initPos;
	private Vector3 m_endPos;
	private UIMatch m_uiMatch;

	private List<ScoreEffect> m_effects = new List<ScoreEffect>();

	void Awake()
	{
		m_scoretoboardEffect = ResourceLoadManager.Instance.LoadPrefab("prefab/effect/E_Scoretoboard") as GameObject;
		m_scoretoboardEffect1 = ResourceLoadManager.Instance.LoadPrefab("prefab/effect/E_Scoretoboard2") as GameObject;
	}

	void Update()
	{
		foreach( ScoreEffect effect in m_effects )
		{
			if( !effect.m_done )
				return;
		}
		//finish
		m_effects.ForEach( (ScoreEffect effect)=>{ DestroyObject(effect.gameObject);});
		m_effects.Clear();
	}

	public void Play(GameMatch match, UIMatch uiMatch, UBasketball ball, int cnt)
	{
        m_initPos = (Vector3)match.mCurScene.mBasket.m_vShootTarget;
		if( match is GameMatch_PVP )
			m_endPos = ball.m_actor.m_team == match.mainRole.m_team ? uiMatch.leftScoreBoard.transform.parent.localPosition : uiMatch.rightScoreBoard.transform.parent.localPosition;
		else
			m_endPos = ball.m_actor.m_team.m_side == Team.Side.eHome ? uiMatch.leftScoreBoard.transform.parent.localPosition : uiMatch.rightScoreBoard.transform.parent.localPosition;
		m_endPos.z = 0.0f;

		m_uiMatch = uiMatch;

		for( int idx = 0; idx != cnt; idx++ )
		{
			GameObject goItem = new GameObject();
			ScoreEffect effect = goItem.AddComponent<ScoreEffect>();
			m_effects.Add(effect);
			effect.m_effect = this;
			effect.Play();
		}
	}
}
