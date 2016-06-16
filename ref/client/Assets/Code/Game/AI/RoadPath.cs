using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class RoadPathManager
	: Singleton<RoadPathManager>
{
    /**ÉÈÇø*/
	public class Sector
	{
		public IM.Vector2 distance;
        public IM.Vector2 distance1;
        public IM.Vector2 range;
        public IM.Vector2 center;

		public List<Player> holders = new List<Player>();

		public int	idx;
		public bool nonArc;
		public Sector(int index)
		{
			idx = index;
			distance = IM.Vector2.zero;
            distance1 = IM.Vector2.zero;
            range = IM.Vector2.zero;
            center = IM.Vector2.zero;
		}
	}

	public class SectorArea
	{
		//public List<Vector2>	sectors = new List<Vector2>();
		public IM.Vector2		range = new IM.Vector2();
        public IM.Vector2       start = new IM.Vector2();
	}

	public IM.Number m_angle;
	public IM.Number m_angleNonArc;
	public IM.Number m_range;
    public IM.Number m_sectorDistance;
    public int m_nonArcNum;
    public int m_arcNum { get { return m_angleNum - m_nonArcNum; } }
    public int m_angleNum { get { return m_AngleList.Count; } }

    public List<IM.Number> m_DistanceList = new List<IM.Number>();
    public List<IM.Number> m_AngleList = new List<IM.Number>();
	public List<Sector> m_Sectors;
    public List<int> m_takenSectors;

	public  IM.Vector3 m_rim = IM.Vector3.zero;

	private RoadPathFinding	simpleRPF;

	private List<Sector> _debugAIPath;

	private Dictionary<string, Sector>	_drawSectors;

	public RoadPathManager()
	{
		m_Sectors = new List<Sector>();
        m_takenSectors = new List<int>();

		simpleRPF = new SimpleRoadPathFinding();

		_drawSectors = new Dictionary<string, Sector>();
	}

    //public List<Sector> GetAPathToRim( Vector3 curPos, PathFinding eFind )
    //{
    //    int iStartIdx = CalcSectorIdx(new IM.Vector3(curPos));
    //    int iDestIdx = CalcSectorIdx(m_rim);

    //    if( eFind == PathFinding.eSimple )
    //        return simpleRPF.GetPath(m_Sectors, iStartIdx, iDestIdx);

    //    return simpleRPF.GetPath(m_Sectors, iStartIdx, iDestIdx);
    //}

	public int GetNeighbour( int idx, EDirection dir )
	{
		int index = idx;
		switch(dir)
		{
		case EDirection.eBack:
			index = idx / m_AngleList.Count == m_DistanceList.Count - 1 ? idx : idx + m_AngleList.Count;
			break;
		case EDirection.eForward:
			index = idx / m_AngleList.Count == 0 ? idx : idx - m_AngleList.Count;
			break;
		case EDirection.eRight:
			index = idx % m_AngleList.Count == 0 ? idx : idx - 1;
			break;
		case EDirection.eLeft:
			index = idx % m_AngleList.Count == m_AngleList.Count - 1 ? idx : idx + 1;
			break;
		default:
			break;
		}
		return index;
	}

	public bool InSectors(SectorArea area, IM.Vector3 vPos)
	{
		int iCurSector = CalcSectorIdx(vPos);
		int row = iCurSector / m_AngleList.Count;
		int col = iCurSector % m_AngleList.Count;

		return area.start.x <= col && col < (area.start.x + area.range.x) && area.start.y <= row && row < (area.start.y + area.range.y);
	}

	//todo: pre-estimalte the border
    //public bool RayIntersectDetect( List<int> sectors, Ray ray, out int colSectorIdx )
    //{
    //    int colomn = m_AngleList.Count;
    //    int row = sectors.Count / colomn;

    //    //float minAngle = float.MaxValue;
    //    IM.Number minAngle = IM.Number.max;
    //    int minSectorIdx = -1;
    //    //colomn
    //    for( int idx = 0; idx != colomn; ++idx )
    //    {
    //        int sectorId = sectors[idx];
    //        IM.Vector3 dirPos2SectorBorder = (m_Sectors[sectorId].center.xz - new IM.Vector3(ray.origin)).normalized;
    //        IM.Number fAngle2Ray = IM.Vector3.Angle(dirPos2SectorBorder, new IM.Vector3(ray.direction));
    //        if( minAngle > fAngle2Ray )
    //        {
    //            minAngle = fAngle2Ray;
    //            minSectorIdx = sectorId;
    //        }

    //        sectorId = sectors[idx + (row - 1) * colomn];
    //        dirPos2SectorBorder = (m_Sectors[sectorId].center.xz - new IM.Vector3(ray.origin)).normalized;
    //        fAngle2Ray = IM.Vector3.Angle(dirPos2SectorBorder, new IM.Vector3(ray.direction));
    //        if( minAngle > fAngle2Ray )
    //        {
    //            minAngle = fAngle2Ray;
    //            minSectorIdx = sectorId;
    //        }
    //    }
    //    //row
    //    for( int idx = 0; idx != row; ++idx )
    //    {
    //        int sectorId = sectors[idx * colomn];
    //        IM.Vector3 dirPos2SectorBorder = (m_Sectors[sectorId].center.xz - new IM.Vector3(ray.origin)).normalized;
    //        IM.Number fAngle2Ray = IM.Vector3.Angle(dirPos2SectorBorder, new IM.Vector3(ray.direction));
    //        if( minAngle > fAngle2Ray )
    //        {
    //            minAngle = fAngle2Ray;
    //            minSectorIdx = idx;
    //        }

    //        sectorId = sectors[(idx + 1) * colomn - 1];
    //        dirPos2SectorBorder = (m_Sectors[sectorId].center.xz - new IM.Vector3(ray.origin)).normalized;
    //        fAngle2Ray = IM.Vector3.Angle(dirPos2SectorBorder, new IM.Vector3(ray.direction));
    //        if( minAngle > fAngle2Ray )
    //        {
    //            minAngle = fAngle2Ray;
    //            minSectorIdx = idx;
    //        }
    //    }
    //    colSectorIdx = minSectorIdx;

    //    return minSectorIdx != -1;
    //}

	public void BuildSectors(IM.Vector3 rim, IM.Number angle3pt, int nonArcNum, int arcNum, IM.Number range, IM.Number sectorDistance)
	{
		m_rim = rim;
		m_rim.y = IM.Number.zero;

		m_angle = angle3pt / arcNum;
		m_range = range;
		m_sectorDistance = sectorDistance;
		m_angleNonArc = (IM.Math.HALF_CIRCLE - angle3pt) / (nonArcNum * 2);
		m_nonArcNum = nonArcNum;

		m_Sectors.Clear();
		m_takenSectors.Clear();
		m_DistanceList.Clear();
		m_AngleList.Clear();

		if( _debugAIPath != null )
			_debugAIPath.Clear();

		_drawSectors.Clear();

		int iAngleCnt = nonArcNum * 2 + arcNum;
        int iDistanceCnt = (m_range / m_sectorDistance).roundToInt;

		for( int iDistanceIdx = 0; iDistanceIdx != iDistanceCnt; ++iDistanceIdx )
			m_DistanceList.Add( iDistanceIdx * m_sectorDistance );
		for (int iAngleIdx = 0; iAngleIdx < nonArcNum; ++iAngleIdx)
			m_AngleList.Add(iAngleIdx * m_angleNonArc);
		for (int iAngleIdx = nonArcNum; iAngleIdx < (iAngleCnt - nonArcNum); ++iAngleIdx)
			m_AngleList.Add(nonArcNum * m_angleNonArc + (iAngleIdx - nonArcNum) * m_angle);
		for (int iAngleIdx = (iAngleCnt - nonArcNum); iAngleIdx < iAngleCnt; ++iAngleIdx)
			m_AngleList.Add((nonArcNum + iAngleIdx - (iAngleCnt - nonArcNum)) * m_angleNonArc + arcNum * m_angle);

        IM.Number secDistNonArc = m_sectorDistance * IM.Math.Cos(IM.Math.Deg2Rad(m_angleNonArc * m_nonArcNum));
		for( int iDistanceIdx = 0; iDistanceIdx != iDistanceCnt; ++iDistanceIdx )
		{
			for( int iAngleIdx = 0; iAngleIdx != iAngleCnt; ++iAngleIdx )
			{
				Sector sector = new Sector(iDistanceIdx * iAngleCnt + iAngleIdx);
				if (iAngleIdx < m_nonArcNum)	// right non-arc
				{
                    IM.Number secDist = secDistNonArc / IM.Math.Cos(IM.Math.Deg2Rad(m_angleNonArc * iAngleIdx));
					sector.distance = new IM.Vector2( iDistanceIdx * secDist, (iDistanceIdx + 1) * secDist );
                    secDist = secDistNonArc / IM.Math.Cos(IM.Math.Deg2Rad(m_angleNonArc * (iAngleIdx + 1)));
					sector.distance1 = new IM.Vector2( iDistanceIdx * secDist, (iDistanceIdx + 1) * secDist );
					sector.range = new IM.Vector2(m_angleNonArc * iAngleIdx, m_angleNonArc * (iAngleIdx + 1));
				}
				else if (iAngleIdx > (iAngleCnt - m_nonArcNum - 1))		// left non-arc
				{
                    IM.Number secDist = secDistNonArc / IM.Math.Cos(IM.Math.Deg2Rad(m_angleNonArc * (iAngleCnt - 1 - iAngleIdx)));
					sector.distance1 = new IM.Vector2( iDistanceIdx * secDist, (iDistanceIdx + 1) * secDist );
                    secDist = secDistNonArc / IM.Math.Cos(IM.Math.Deg2Rad(m_angleNonArc * (iAngleCnt - iAngleIdx)));
					sector.distance = new IM.Vector2( iDistanceIdx * secDist, (iDistanceIdx + 1) * secDist );
					sector.range = new IM.Vector2(new IM.Number(180) - (iAngleCnt - iAngleIdx) * m_angleNonArc, new IM.Number(180) - (iAngleCnt - iAngleIdx - 1) * m_angleNonArc);
				}
				else	// arc area
				{
					sector.distance = new IM.Vector2( iDistanceIdx * m_sectorDistance, (iDistanceIdx + 1) * m_sectorDistance );
					sector.distance1 = sector.distance;
					sector.range = new IM.Vector2(
						(iAngleIdx - m_nonArcNum) * m_angle + m_angleNonArc * m_nonArcNum,
						(iAngleIdx + 1 - m_nonArcNum) * m_angle + m_angleNonArc * m_nonArcNum);
				}
				sector.center = (IM.Quaternion.AngleAxis( (sector.range.x + sector.range.y) * IM.Number.half, IM.Vector3.up) * IM.Vector3.right * (sector.distance.x + sector.distance.y + sector.distance1.x + sector.distance1.y) * new IM.Number(0,250) + m_rim).xz;
				sector.nonArc = iAngleIdx < 2 || iAngleIdx >= (iAngleCnt - 2);
				m_Sectors.Add(sector);
			}

		}

		Player.BuildFavorSectors();
	}

	void _UpdateSectorHolders(Player player)
	{
		GameMatch.MatchRole matchRole = player.m_team.m_role;

		int sectorColomns = RoadPathManager.Instance.m_AngleList.Count;
		int sectorRows = RoadPathManager.Instance.m_DistanceList.Count;
		
		int iCurPlayerSection = RoadPathManager.Instance.CalcSectorIdx(player.position);
		if( iCurPlayerSection == -1 )
			return;

		int iCurColomn 	= iCurPlayerSection % sectorColomns;
		int iCurRow 	= iCurPlayerSection / sectorColomns;
		//Logger.Log("Column: " + iCurColomn + " Row:" + iCurRow);

		if( matchRole == GameMatch.MatchRole.eOffense)
		{
			foreach(Vector2 range in player.m_takenSectorRanges)
			{
				int iRangeColomn = iCurColomn + (int)range.x;
				int iRangeRow = iCurRow + (int)range.y;
				//colomn invalid, bounds collided
				if( iRangeColomn >= sectorColomns || iRangeColomn < 0 )
					continue;
				//row invalid, bounds collided
				if( iRangeRow >= sectorRows || iRangeRow < 0 )
					continue;
				int iNewColIdx = iRangeRow * sectorColomns + iRangeColomn;
				RoadPathManager.Sector sector = RoadPathManager.Instance.m_Sectors[iNewColIdx];
				sector.holders.Add(player);
				m_takenSectors.Add(iNewColIdx);
			}
		}
		else
		{
			RoadPathManager.Sector sector = RoadPathManager.Instance.m_Sectors[iCurPlayerSection];
			sector.holders.Add(player);
			m_takenSectors.Add(iCurPlayerSection);
		}
	}

	public void Update( List<Player> players )
	{
		if( m_Sectors.Count == 0 )
			return;

		m_Sectors.ForEach( delegate(Sector sector){ sector.holders.Clear(); } );
		m_takenSectors.Clear();

		foreach( Player player in players )
			_UpdateSectorHolders(player);

		m_takenSectors = m_takenSectors.Distinct().ToList();

		foreach( int takenIdx in m_takenSectors )
		{
		 	Sector takenSector = m_Sectors[takenIdx];
			if( takenSector.holders.Count < 2 )
				continue;
			foreach( Player player in takenSector.holders )
			{
				if( player.m_aiMgr == null || !player.m_aiMgr.m_enable )
					continue;
				player.m_aiMgr.OnSectorCollided(takenSector);
			}
		}
		/*
		foreach( Player player in players )
		{
			if( !player.m_bWithBall )
				continue;
			_debugAIPath = GetAPathToRim(player.position, PathFinding.eSimple);
			int sectorIdx = CalcSectorIdx( m_rim, player.position);
			if( sectorIdx != -1 )
				_debugAIPath.Insert(0, m_Sectors[sectorIdx]);
		}
		*/
	}

	public int CalcSectorIdx(IM.Vector3 pos )
	{
		IM.Vector3 dirToShootTarget = pos - m_rim;
		dirToShootTarget.y = IM.Number.zero;
		if( dirToShootTarget == IM.Vector3.zero )
			return -1;

		IM.Number distToShootTarget = dirToShootTarget.magnitude;
        //Quaternion quatAngle = Quaternion.FromToRotation(Vector3.right, dirToShootTarget.normalized);
        //float fAngle = quatAngle.eulerAngles.y;
        IM.Number fAngle = IM.Vector3.FromToAngle(IM.Vector3.right, dirToShootTarget.normalized);

        IM.Number sectorDistNonArc = m_sectorDistance * IM.Math.Cos(IM.Math.Deg2Rad(m_angleNonArc * m_nonArcNum));
		if (fAngle < IM.Number.zero || fAngle > new IM.Number(180))	// illegal area
			return -1;
        else if (fAngle < m_angleNonArc * m_nonArcNum || fAngle > (new IM.Number(180) - m_angleNonArc * m_nonArcNum))	// no-arc area
		{
			if (IM.Math.Abs(pos.x) > sectorDistNonArc * m_DistanceList.Count)
				return -1;
		}
		else if (distToShootTarget > m_range)	//	arc area
			return -1;

		int iDistanceIndex;
		int iAngleIndex;
		if (fAngle <= m_angleNonArc * m_nonArcNum)	// right non-arc
		{
            iDistanceIndex = (pos.x / sectorDistNonArc).floorToInt;
			iAngleIndex = (fAngle / m_angleNonArc).floorToInt;
		}
		else if (fAngle >= (new IM.Number(180) - m_angleNonArc * m_nonArcNum))		// left non-arc
		{
            iDistanceIndex = (-pos.x / sectorDistNonArc).floorToInt;
			iAngleIndex = m_angleNum - 1 - ((new IM.Number(180) - fAngle) / m_angleNonArc).floorToInt;
		}
		else	// arc area
		{
			iDistanceIndex = (distToShootTarget / m_sectorDistance).floorToInt;
			iAngleIndex = ((fAngle - m_angleNonArc * m_nonArcNum) / m_angle).floorToInt + m_nonArcNum;
		}

		return iDistanceIndex * m_AngleList.Count + iAngleIndex;
	}

	public void AddDrawSector( string strSector, Sector toDraw )
	{
		if( _drawSectors.ContainsKey(strSector) )
		{
			_drawSectors[strSector] = toDraw;
			return;
		}
		_drawSectors.Add(strSector, toDraw);
	}

	void _DrawSector( Sector sector, Color color )
	{
		Gizmos.color = color;
		if (sector.nonArc)
		{
			IM.Vector3 from = m_rim + IM.Quaternion.AngleAxis(sector.range.x, IM.Vector3.up) * IM.Vector3.right * sector.distance.x;
			IM.Vector3 to = m_rim + IM.Quaternion.AngleAxis(sector.range.x, IM.Vector3.up) * IM.Vector3.right * sector.distance.y;
			Gizmos.DrawLine((Vector3)from,(Vector3)to);

			IM.Vector3 from1 = m_rim + IM.Quaternion.AngleAxis(sector.range.y, IM.Vector3.up) * IM.Vector3.right * sector.distance1.x;
			IM.Vector3 to1 = m_rim + IM.Quaternion.AngleAxis(sector.range.y, IM.Vector3.up) * IM.Vector3.right * sector.distance1.y;
			Gizmos.DrawLine((Vector3)from1,(Vector3)to1);

			Gizmos.DrawLine((Vector3)from, (Vector3)from1);
			Gizmos.DrawLine((Vector3)to,(Vector3)to1);
		}
		else
		{
			GameUtils.DrawWireArc((Vector3)m_rim, Vector3.up, Quaternion.AngleAxis((float)sector.range.x ,Vector3.up) * Vector3.right,(float)m_angle, (float)sector.distance.x,color);
			GameUtils.DrawWireArc((Vector3)m_rim,Vector3.up, Quaternion.AngleAxis((float)sector.range.x, Vector3.up) * Vector3.right, (float)m_angle, (float)sector.distance.y, color);

			IM.Vector3 dirRim2Sector1 = m_rim + IM.Quaternion.AngleAxis(sector.range.x, IM.Vector3.up) * IM.Vector3.right * sector.distance.x;
			IM.Vector3 dirRim2Sector2 = m_rim + IM.Quaternion.AngleAxis(sector.range.x, IM.Vector3.up) * IM.Vector3.right * sector.distance.y;
			Gizmos.DrawLine((Vector3)dirRim2Sector1, (Vector3)dirRim2Sector2);

			dirRim2Sector1 = m_rim + IM.Quaternion.AngleAxis(sector.range.y, IM.Vector3.up) * IM.Vector3.right * sector.distance.x;
			dirRim2Sector2 = m_rim + IM.Quaternion.AngleAxis(sector.range.y, IM.Vector3.up) * IM.Vector3.right * sector.distance.y;
			Gizmos.DrawLine((Vector3)dirRim2Sector1, (Vector3)dirRim2Sector2);
		}

		//Debug.DrawRay(GameUtils.DummyV2Y(sector.center), Vector3.up);
	}

    //public bool IsBlocked(Vector2 pos)
    //{
    //    return true;
    //}

	public Sector Bounce(IM.Vector2 pos, Sector collideSector, SectorArea area)
	{
		int iCurSector 	= CalcSectorIdx( pos.xz );
		int iColomn = m_AngleList.Count;
		
		int iCurColomn 	= iCurSector % iColomn;

		GameMatch match = GameSystem.Instance.mClient.mCurMatch;
		IM.Vector3 dirSectorToBasket = GameUtils.HorizonalNormalized(match.mCurScene.mBasket.m_vShootTarget, collideSector.center.xy);
		IM.Vector3 dirSectorToBasketRight = IM.Quaternion.AngleAxis(new IM.Number(90), IM.Vector3.up) * dirSectorToBasket;
        IM.Vector2 curRight = dirSectorToBasketRight.xz;
		
		IM.Vector2 dirSector2Pos = (collideSector.center - pos).normalized;
		int iNewCol = iCurColomn;
		if( IM.Vector2.Dot(dirSector2Pos, curRight) > 0.0f )
			iNewCol = Mathf.Clamp( iCurColomn + Random.Range(3, 6), (int)area.start.x, (int)area.start.x + (int)area.range.x - 1 );
		else
			iNewCol = Mathf.Clamp( iCurColomn - Random.Range(3, 6), (int)area.start.x, (int)area.start.x + (int)area.range.x - 1 );
		
		int iNewRow = Mathf.Clamp( (int)area.start.y + Random.Range(0, (int)area.range.y), (int)area.start.y, (int)area.start.y + (int)area.range.y - 1 );
		int index = iNewRow * iColomn + iNewCol;
		return m_Sectors[index];
		//AddDrawSector("targetSector", targetSector);
	}

	public void OnDebugDraw()
	{
		if( m_takenSectors.Count != 0 )
		{
			foreach( int idx in m_takenSectors )
			{
				Sector takenSector = m_Sectors[idx];
				_DrawSector(takenSector, Color.red);
			}
		}

		if( _debugAIPath != null )
		{
			int cnt = _debugAIPath.Count;
			if( cnt != 0 )
			{
				for( int idx = 0; idx != cnt - 1; idx++ )
					Gizmos.DrawLine((Vector3)(_debugAIPath[idx].center).xz, (Vector3)(_debugAIPath[idx+1].center).xz);
			}
		}

		foreach (KeyValuePair<string, Sector> keyValue in _drawSectors)
		{
			_DrawSector(keyValue.Value, Color.white);
		}
	}
}