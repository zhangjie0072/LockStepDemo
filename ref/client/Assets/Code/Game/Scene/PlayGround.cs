using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using fogs.proto.msg;

public interface ZoneConstrain
{
	void Constraint(ref IM.Vector3 pos);
}

public class DefaultZoneConstrain
	: ZoneConstrain
{
	private PlayGround mGround;

	public DefaultZoneConstrain( PlayGround playground )
	{
		mGround = playground;
	}

	public void Constraint(ref IM.Vector3 pos)
	{
		pos.x = IM.Math.Max( pos.x,	-mGround.mHalfSize.x + mGround.mCenter.x );
        pos.x = IM.Math.Min(pos.x, mGround.mHalfSize.x + mGround.mCenter.x);

        pos.z = IM.Math.Max(pos.z, mGround.mCenter.z);
        pos.z = IM.Math.Min(pos.z, mGround.mHalfSize.y + mGround.mCenter.z);
	}
}

public class PlayGround
{
	public IM.Vector2 mHalfSize;
	public IM.Vector3 mCenter;
    public IM.Vector3 mBasketPos;
	
	public IM.Number m3PointRadius;
	public IM.Vector2 m3PointCenter; 
	public IM.Number  m3PointBaseLine;
	
	public IM.Number mFreeThrowLane;

    public IM.Number m2PointRadius;

    public IM.Number mLineWidth;

    public IM.Number mDist3PtOutside = new IM.Number(1,800);

    public IM.Number m3PointMaxAngle;
	private GameScene mScene;

	private List<ZoneConstrain>		mZoneConstrain = new List<ZoneConstrain>();

	public PlayGround( GameScene scene )
	{
		mScene = scene;
	}
	
	public void Build()
	{ 
        m3PointMaxAngle = new IM.Number(90) -IM.Math.Acos(m3PointBaseLine / m3PointRadius) / IM.Math.PI * new IM.Number(180);
		mZoneConstrain.Clear();

		mZoneConstrain.Add( new DefaultZoneConstrain(this) );
	}

	public void AddZoneConstrain(ZoneConstrain newConstrain)
	{
		mZoneConstrain.Add(newConstrain);
	}

	public void BoundInZone( ref IM.Vector3 pos )
	{
		foreach( ZoneConstrain zoneConstrain in mZoneConstrain )
			zoneConstrain.Constraint(ref pos);
	}

	public Area	GetArea( Player player )
	{
        IM.Vector2 vPlayerPos = player.position.xz;
		if( !In3PointRange(vPlayerPos) )
			return Area.eFar;
		if( InFreeThrowLane(vPlayerPos) )
			return Area.eNear;
		return Area.eMiddle;
	}

	public Area	GetDunkArea(Player player)
	{
		IM.Vector2 vPlayerPos = player.position.xz;
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		IM.Number fDistance = GameUtils.HorizonalDistance(match.mCurScene.mBasket.m_vShootTarget, player.position);
		if( InFreeThrowLane(vPlayerPos) )
			return Area.eNear;
		return Area.eMiddle;

		//else if( fDistance < player.m_dunkDistance )
		//	return Area.eMiddle;
		//return Area.eInvalid;
	}

	public Area	GetLayupArea(Player player)
	{
        IM.Vector2 vPlayerPos = player.position.xz;
		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
        IM.Number fDistance = GameUtils.HorizonalDistance(match.mCurScene.mBasket.m_vShootTarget, player.position);
		if( InFreeThrowLane(vPlayerPos) )
			return Area.eNear;
		return Area.eMiddle;
		/*
		else if( fDistance < player.m_LayupDistance )
			return Area.eMiddle;
		return Area.eInvalid;
		*/
	}

	public bool In3PointRange(IM.Vector2 pos, IM.Number fOffset)
	{
		IM.Number dist = GetDistTo3PTLine(pos);
		return (dist + fOffset) > 0;
	}

    public bool In3PointRange(IM.Vector2 pos)
    {
        return In3PointRange(pos, IM.Number.zero);
    }

	public IM.Number GetDistTo3PTLine(IM.Vector2 pos)
	{
        IM.Vector2 v3PtCenter = new IM.Vector2(mCenter.x + m3PointCenter.x, mCenter.z + m3PointCenter.y);

        IM.Vector2 dir = (pos - v3PtCenter).normalized;
        IM.Vector2 dirPoint3Forward = new IM.Vector2(IM.Number.zero, -IM.Number.one);
        IM.Number angle = IM.Vector2.Angle(dir, dirPoint3Forward);
		if( angle > m3PointMaxAngle )
		{
			return m3PointBaseLine - IM.Math.Abs(pos.x);
		}
		else
		{
			return m3PointRadius - IM.Vector2.Distance(pos, v3PtCenter);
		}
	}

	public bool InFreeThrowLane(IM.Vector2 pos)
	{
		UBasket basket = mScene.mBasket;
        IM.Number fDistanceToNet = IM.Vector2.Distance(pos, basket.m_vShootTarget.xz);
		return fDistanceToNet < mFreeThrowLane;
	}
}