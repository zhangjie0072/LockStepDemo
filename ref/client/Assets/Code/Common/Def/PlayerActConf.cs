using UnityEngine;
using System.Collections;

public enum EDirection
{
	eForward		= 1,
	eForwardRight	= 2,
	eRight			= 3,
	eBackRight		= 4,
	eBack			= 5,
	eBackLeft		= 6,
	eLeft			= 7,
	eForwardLeft	= 8,
	eNone			= 9,
}

public enum RotateTo
{
	eNone,
	eBasket,
	eDefenseTarget,
	eBall,
}

public enum RotateType
{
	eDirect,
	eSmooth,
}
