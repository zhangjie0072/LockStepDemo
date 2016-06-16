using UnityEngine;
using System.Collections;

public class UITips : MonoBehaviour
{
	public static UITips tip;
	public UISprite IgnoreSpriteArea;
	public UIWidget IgnoreWidgetArea;
	// Use this for initialization
	void Start ()
	{
		if (tip != null)
		{
			Destroy (tip.gameObject);
		}
		tip = this;
		UIEventListener.Get(gameObject).onClick += OnTipClick;
	}
	void OnTipClick(GameObject go)
	{
		Vector3 touch = IgnoreSpriteArea.transform.position;
		bool isTouched = false;
		foreach(Touch tc in Input.touches)
		{
			if(tc.phase == TouchPhase.Ended)
			{
				Vector2 tcp2 = tc.position;
				Vector3 tcp3 = UIManager.Instance.m_uiCamera.cachedCamera.ScreenToViewportPoint(tcp2);

				float globalScale = IgnoreSpriteArea.transform.lossyScale.x;
				float l,r,u,d,w,h;
				w = IgnoreSpriteArea.width*globalScale;
				h = IgnoreSpriteArea.height*globalScale;
				l = IgnoreSpriteArea.transform.position.x-w/2;
				r = IgnoreSpriteArea.transform.position.x+w/2;
				u = IgnoreSpriteArea.transform.position.y+h/2;
				d = IgnoreSpriteArea.transform.position.y-h/2;
				
				bool contain = l<tcp3.x && r>tcp3.x && u>tcp3.y && d<tcp3.y;

				if(!contain)
				{
					Ray ray = new Ray(tcp3+new Vector3(0,0,-1000),new Vector3(0,0,1000));
					RaycastHit[] hits =  Physics.RaycastAll(ray);
					foreach(RaycastHit hit in hits)
					{
						if(hit.collider.gameObject!=this.gameObject && hit.collider.gameObject.GetComponent<UIEventListener>())
						{
							hit.collider.gameObject.SendMessage("OnClick");
						}
					}
				}


			}
		}
		if(!isTouched)
		{
			Vector3 pos = UIManager.Instance.m_uiCamera.cachedCamera.ScreenToWorldPoint(Input.mousePosition);
			float globalScale = IgnoreSpriteArea.transform.lossyScale.x;

			float l,r,u,d,w,h;
			w = IgnoreSpriteArea.width*globalScale;
			h = IgnoreSpriteArea.height*globalScale;
			l = IgnoreSpriteArea.transform.position.x-w/2;
			r = IgnoreSpriteArea.transform.position.x+w/2;
			u = IgnoreSpriteArea.transform.position.y+h/2;
			d = IgnoreSpriteArea.transform.position.y-h/2;

			bool contain = l<pos.x && r>pos.x && u>pos.y && d<pos.y;

			if(!contain)
			{
				Ray ray = new Ray(pos+new Vector3(0,0,-1000),new Vector3(0,0,1000));
				RaycastHit[] hits =  Physics.RaycastAll(ray);
				foreach(RaycastHit hit in hits)
				{
					if(hit.collider.gameObject!=this.gameObject && hit.collider.gameObject.GetComponent<UIEventListener>())
					{
						hit.collider.gameObject.SendMessage("OnClick");
					}
				}
			}
		}
	}
	void OnDestroy()
	{
		UIEventListener.Get(gameObject).onClick = null;
	}
}

