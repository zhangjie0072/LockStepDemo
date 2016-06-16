using UnityEngine;

public class AOD
{
    public static IM.Number bestDefenseRadius = new IM.Number(1, 500); 
    public static IM.Number validDefenseRadius = new IM.Number(3);
	/*?-?¨¨¦Ì?D¡ì1?
	public static float bestDefenseRadius = 1.25f;
	public static float validDefenseRadius = 2.5f;
	*/
    public IM.Number angle = new IM.Number(60);
	/*?-?¨¨¦Ì?D¡ì1?
	public float angle = 75.0f;
	*/
	public bool	visible = false;
	
	public enum Zone
	{
		eInvalid,
		eValid,
		eBest
	}
	
	public Player owner{ get; private set; }
	
	private Object		mResIndicator;
	private GameObject 	mIndicator;
	
	public AOD(Player player)
	{
		if( mResIndicator == null )
            mResIndicator = ResourceLoadManager.Instance.LoadPrefab("prefab/indicator/aod");
		
		mIndicator = GameObject.Instantiate(mResIndicator) as GameObject;
		mIndicator.GetComponentInChildren<Renderer>().material.renderQueue = RenderQueue.IndicatorAod;
		mIndicator.SetActive(false);
		/*
		mIndicatorValid = new ExtendShape.Arc();
		mIndicatorValid.material = GameObject.Instantiate(resMaterial) as Material;
		mIndicatorValid.material.SetPass(1);
		mIndicatorValid.material.color = new Color(1.0f,0.0f,0.0f,0.5f);
		mIndicatorValid.radius_min = 1.0f;
	    mIndicatorValid.radius_max = 2.0f;
		mIndicatorValid.Build();
		*/
		
		owner = player;
		mIndicator.transform.parent = owner.transform;
		mIndicator.transform.localPosition = Vector3.zero;
	}

    public Zone GetStateByPos( IM.Vector3 defTargetPos)
    {
        return GetStateByPos(defTargetPos, IM.Number.zero, IM.Number.zero);
    }
	
	public Zone GetStateByPos( IM.Vector3 defTargetPos, IM.Number devAngle, IM.Number devDist)
	{
		Zone tmpZone = Zone.eInvalid;
		
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		if( match == null )
			return tmpZone;
		if( match.mCurScene == null )
			return tmpZone;
		
		IM.Vector3 vDirToBasket = GameUtils.HorizonalNormalized(match.mCurScene.mBasket.m_vShootTarget, owner.position);
		IM.Vector3 vDirPlayerToDef = GameUtils.HorizonalNormalized(defTargetPos, owner.position);
		
        IM.Number fAngle = IM.Vector3.Angle(vDirPlayerToDef, vDirToBasket);
        fAngle *= (IM.Number.one - devAngle);
		if( fAngle > angle * IM.Number.half)
			tmpZone = Zone.eInvalid;
		else
		{
            IM.Number distPlayerToDef = GameUtils.HorizonalDistance(defTargetPos, owner.position);
            distPlayerToDef *= (IM.Number.one - devDist);
			if((bestDefenseRadius < distPlayerToDef) && (distPlayerToDef < validDefenseRadius))
				tmpZone = Zone.eValid;
			else if( distPlayerToDef < bestDefenseRadius )
				tmpZone = Zone.eBest;
			else
				tmpZone = Zone.eInvalid;
		}
		return tmpZone;
	}

    //äÖÈ¾²ã
	public void Update()
	{
		if( owner == null )
			return;

		if(!visible)
		{
			mIndicator.SetActive(false);
			return;
		}
		mIndicator.SetActive(true);

		Renderer renderer = mIndicator.GetComponentInChildren<Renderer>();
		if( renderer == null )
			return;

		Player defenseTarget = owner.m_defenseTarget; 
        Zone zone = GetStateByPos(defenseTarget.position);
		
		if( zone == Zone.eInvalid )
			renderer.material.color = GameUtils.red;
		else if( zone == Zone.eValid )
			renderer.material.color = GameUtils.yellow;
		else if( zone == Zone.eBest )
			renderer.material.color = GameUtils.green;
		renderer.material.color = new Color(renderer.material.color.r, renderer.material.color.g, renderer.material.color.b, 0.5f);

		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		if( match == null )
			return;
        Vector3 vDirToBasket = (Vector3)match.mCurScene.mBasket.m_vShootTarget - (Vector3)owner.position;
		vDirToBasket.y = 0.0f; 
		vDirToBasket.Normalize();

		mIndicator.transform.forward = vDirToBasket;
		
		mIndicator.transform.localPosition = Vector3.up * 0.02f;
	}
	
	void _DebugDrawIndicator(ExtendShape.Arc indicator)
	{
		if( owner == null || owner.m_defenseTarget == null )
			return;
		
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		if( match == null )
			return;
		if( match.mCurScene == null )
			return;

        Vector3 vDirToBasket = (Vector3)match.mCurScene.mBasket.m_vShootTarget - (Vector3)owner.position;
		
		indicator.center = (Vector3)owner.position;
		indicator.center.y += 0.01f;
		
		indicator.forward = vDirToBasket;
		indicator.Draw();
	}
}