using System.Collections.Generic;
using UnityEngine;

public class UIMatchMessageBlob 
	: MonoBehaviour
{
	class MsgBlob
	{
		public GameObject goBtn;
		public MatchMsg msg;
		public bool	bHome = true;
	}

	private GameObject	m_goBlob;
	private List<MsgBlob> m_goBlobs = new List<MsgBlob>();
	private bool m_bNeedRefresh = false;
	private float m_moveTime = 0.5f;
	private float m_dispearTime = 5.0f;
	private float m_blobWitdh = 1.0f;

	void Awake()
	{
		Object resBlob = ResourceLoadManager.Instance.GetResources("Prefab/GUI/ButtonTalk3");
		m_goBlob = GameObject.Instantiate(resBlob) as GameObject ;
		m_goBlob.AddComponent<TweenPosition>();
		m_goBlob.AddComponent<TweenAlpha>();
		m_goBlob.SetActive(false);

		UISprite uiBlob = m_goBlob.GetComponent<UISprite>();
		m_blobWitdh = (float)uiBlob.width;

		m_bNeedRefresh = false;
	}

	public void AddNewBlob(MatchMsg msg, bool bHome)
	{
		MsgBlob msgBlob = new MsgBlob();

		GameObject newBtn = NGUITools.AddChild(gameObject, m_goBlob);
		TweenAlpha.Begin(newBtn, m_dispearTime, 0.0f);

		newBtn.SetActive(true);
		msgBlob.goBtn = newBtn;
		msgBlob.msg = msg;
		msgBlob.bHome = bHome;

		UILabel uiLabel = newBtn.GetComponentInChildren<UILabel>();
		uiLabel.text = msg.pop_text;

		if( m_goBlobs.Count == 3 )
		{
			MsgBlob lastMsgBlob = m_goBlobs[2];
			m_goBlobs.Remove(lastMsgBlob);
			Destroy(lastMsgBlob.goBtn);
		}
		m_goBlobs.Insert(0, msgBlob);
		m_bNeedRefresh = true;
	}

	void Update()
	{
		if( m_bNeedRefresh )
		{
			if( m_goBlobs.Count > 1 )
			{
				for(int idx = 1; idx != m_goBlobs.Count; ++idx)
				{
					MsgBlob msgblob = m_goBlobs[idx];
					GameObject blob = msgblob.goBtn;
					Vector3 target = blob.transform.localPosition;

					if( msgblob.bHome )
						target.x -= m_blobWitdh;
					else
						target.x += m_blobWitdh;
						

					TweenPosition.Begin(blob, m_moveTime, target).method = UITweener.Method.EaseOut;
				}
			}

			foreach(MsgBlob msgBlob in m_goBlobs)
			{
				if( msgBlob.bHome )
					continue;
				UISprite uiBlob = msgBlob.goBtn.GetComponent<UISprite>();
				uiBlob.flip = UIBasicSprite.Flip.Horizontally;
			}

			m_bNeedRefresh = false;
		}
	}
}

