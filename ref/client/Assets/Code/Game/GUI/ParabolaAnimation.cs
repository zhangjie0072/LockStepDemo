using UnityEngine;
using System.Collections;

public class ParabolaAnimation : MonoBehaviour {
	public bool testMode;
	public float translateStart = 0.0f;
	public float scaleStart = 0.0f;
	public float rotateStart = 0.0f;
	public float translateDuration = 0.5f;
	public float scaleDuration = 0.2f;
	public float rotateDuration = 0.65f;
	public bool doRotation = true;
	public bool doTranslation = true;
	public bool doScale = true;
	public bool AutoReleaseOnEnd = true;
	public float factor = 30f;
	//本身动画时间
	public Vector3 TweenMax ;
	public Vector3 TweenMin ;
	public float NormalScaleToMaxDuration = 0.0f;
	public float MaxScaleToMinDuration = 0.0f;
	public float MinScaleToNormalDuration = 0.0f;

	Vector3 startScale = Vector3.zero;
	Vector3 endScale = Vector3.zero;
	Vector3 startPosition = Vector3.zero;
	Vector3 endPosition = Vector3.zero;
	Vector3 startRotation = Vector3.zero;
	Vector3 endRotation = Vector3.zero;
	private TweenRotation tweenRotate;
	private TweenScale tweenScale;
	private TweenScale selfTweenScale;
	private float gX = 0;
	private float gY = 0;
	//速度
	private float VelocityX = 1;
	private float VelocityY = -1;
	private GameObject target = null;
	bool isAnimation = false;
	float startTime = 0;
	float endTime = 0;
	private short TweenFlag = 0;	//0 第一次tween 1 第二次tween 2第三次tween
	// Use this for initialization
	void Start () {
		selfTweenScale = transform.GetComponent<TweenScale>();
		selfTweenScale.AddOnFinished(this.ScaleOver);
		endPosition = transform.FindChild("Cola").transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		if (isAnimation)
		{
			if(Time.fixedTime>startTime)
			{
				if(Time.fixedTime>endTime)
				{
					isAnimation = false;
					//自动删除自身
					if(AutoReleaseOnEnd)
					{
						Destroy(target);
						ExcecuteScale();
					}
//					Debug.Log("Animation ended vx "+VelocityX+",vy "+VelocityY);
				}

				//设置位置
				float vx = VelocityX;
				float vy = VelocityY;
				VelocityY = VelocityY +gY*Time.deltaTime;
				VelocityX = VelocityX+gX*Time.deltaTime;
				float deltY = (VelocityY+vy)*Time.deltaTime/2;
				float deltX = (vx+VelocityX)*Time.deltaTime/2;
				Vector3 nowPos = target.transform.position;
				nowPos = nowPos+ new Vector3(deltX,deltY,0);
				target.transform.position = nowPos;
			}

		}
	}
	public void StartAtPosition(Vector3 startPos)
	{
//		Debug.Log("end position "+endPosition.ToString()+",start position "+startPos);
		this.enabled = true;
		startPosition = startPos;
		Excecute();
	}
	/// <summary>
	/// scale动画
	/// </summary>
	private void ExcecuteScale()
	{
		selfTweenScale.ResetToBeginning();
		selfTweenScale.from = new Vector3(1,1,1);
		selfTweenScale.to = TweenMax;
		selfTweenScale.duration = NormalScaleToMaxDuration;
		selfTweenScale.PlayForward();
	}
	/// <summary>
	/// 伸缩动画结束回调
	/// </summary>
	private void ScaleOver()
	{
//		Debug.Log("ScaleOver");
		if(TweenFlag == 0)
		{
			selfTweenScale.ResetToBeginning();
			selfTweenScale.to = TweenMin;
			selfTweenScale.from = TweenMax;
			selfTweenScale.duration = MaxScaleToMinDuration;
			selfTweenScale.PlayForward();
		}
		else if(TweenFlag == 1)
		{
			selfTweenScale.ResetToBeginning();
			selfTweenScale.to = new Vector3(1,1,1);
			selfTweenScale.from = TweenMin;
			selfTweenScale.duration = MinScaleToNormalDuration;
			selfTweenScale.PlayForward();
		}
		else
		{
			//所有动画结束后停止这个脚本
			this.enabled = false;
		}
		TweenFlag++;
	}
	/// <summary>
	/// 设置初始参数.
	/// </summary>
	public void Excecute ()
	{
		
		GameObject go = GameObject.Instantiate(ResourceLoadManager.Instance.LoadPrefab("Prefab/GUI/Cola"));
		if(target!=null)
		{
			Destroy(target);
		}
		target = go;//.transform.FindChild("Cola").gameObject;
		tweenRotate = go.transform.GetComponent<TweenRotation>();
		tweenScale = go.transform.GetComponent<TweenScale>();
		if(tweenRotate)
		{
			if(!doRotation)
				tweenRotate.enabled = false;
			else{
				if(rotateStart>0)
				{
					tweenRotate.delay = rotateStart;
				}
				if(rotateDuration>0)
				{
					tweenRotate.duration = rotateDuration;
				}
//				if(tweenRotate.duration>translateDuration)
//				{
//					tweenRotate.duration = translateDuration;
//				}
				if(startRotation!=Vector3.zero)
				{
					tweenRotate.from = startRotation;
				}
				if(endRotation!=Vector3.zero)
				{
					tweenRotate.to = endRotation;
				}
			}
		}
		if(tweenScale)
		{
			if(!doScale)
				tweenScale.enabled = false;
			else{
				if(scaleStart>0)
				{
					tweenScale.delay = scaleStart;
				}
				if(scaleDuration>0)
				{
					tweenScale.duration = scaleDuration;
				}
				if(startScale!=Vector3.zero)
				{
					tweenScale.from = startScale;
				}
				if(endScale!=Vector3.zero)
				{
					tweenScale.to = endScale;
				}
			}
		}
		go.transform.position = startPosition;
		isAnimation = true;
		go.transform.parent = transform.parent.parent;
		if(isAnimation)
		{
			//计算路径
			//f(x) = ax^2 +bx+c
			startTime = Time.fixedTime+translateStart;
			endTime = startTime+translateDuration;
			float scaleY = transform.lossyScale.y;
			gY = -(720-(startPosition.y - endPosition.y)/scaleY)/720*factor;
//			Debug.Log("ref "+(startPosition.y - endPosition.y)/scaleY);
			//y2-y1 = v*t +a*t^2/2
			//v = (y2-y1-a*t^2/2)/t
			VelocityX = (-startPosition.x + endPosition.x)/translateDuration;
			VelocityY = (-startPosition.y + endPosition.y - gY*translateDuration*translateDuration/2)/translateDuration;
//			Debug.Log("gy "+gY+",vx "+VelocityX+",vy "+VelocityY);
//			Debug.Log("starting "+startPosition.ToString());

		}
		TweenFlag = 0;

	}
//	bool check()
//	{
//		return true;
//	}
//	public void Click(GameObject go)
//	{
//		if (!isAnimation)
//		{
//			startPos = go.transform.position;
//			Excecute();
//		}
//	}
}
