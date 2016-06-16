using UnityEngine;
using System.Collections.Generic;

public enum PathFinding
{
	eSimple,
	eAStar,
}

public interface RoadPathFinding
{
	List<RoadPathManager.Sector> GetPath( List<RoadPathManager.Sector> map, int startIdx, int destIdx ); 
}

public class SimpleRoadPathFinding
	: RoadPathFinding
{
	public List<RoadPathManager.Sector> GetPath( List<RoadPathManager.Sector> map, int startIdx, int destIdx )
	{
		List<RoadPathManager.Sector> sectors = new List<RoadPathManager.Sector>();
		_FindPath(startIdx, map, sectors);

		return sectors;
	}


	void _FindPath(int iCurIdx, List<RoadPathManager.Sector> map, List<RoadPathManager.Sector> validSector )
	{
		/*
		int iForward = RoadPathManager.Instance.GetNeighbour( iCurIdx, EDirection.eForward );
		if( iForward == iCurIdx )
			return;
		
		if( map[iForward].taken )
		{
			int iFLeft = RoadPathManager.Instance.GetNeighbour( iForward, EDirection.eLeft );
			if( iFLeft == iForward || map[iFLeft].taken )
			{
				int iFRight = RoadPathManager.Instance.GetNeighbour( iForward, EDirection.eRight );
				if( iFRight == iForward || map[iFRight].taken )
				{
					int iLeft = RoadPathManager.Instance.GetNeighbour( iCurIdx, EDirection.eLeft );
					if( iLeft == iCurIdx || map[iLeft].taken )
					{
						int iRight = RoadPathManager.Instance.GetNeighbour( iCurIdx, EDirection.eRight );
						if( iRight == iCurIdx || map[iRight].taken )
							return;
						else
						{
							validSector.Add(map[iRight]);
							_FindPath(iRight, map, validSector);
						}
					}
					else
					{
						validSector.Add(map[iLeft]);
						_FindPath(iLeft, map, validSector);
					}
				}
				else
				{
					validSector.Add(map[iFRight]);
					_FindPath(iFRight, map, validSector);
				}
			}
			else
			{
				validSector.Add(map[iFLeft]);
				_FindPath(iFLeft, map, validSector);
			}
		}
		else
		{
			validSector.Add(map[iForward]);
			_FindPath(iForward, map, validSector);
		}
		*/
	}

}