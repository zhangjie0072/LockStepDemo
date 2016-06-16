using System.Collections.Generic;

public class PseudoRandom
{
	IM.Number expectSum;
	IM.Number valueSum;

	//RET: shouldSumValue
	public bool AdjustRate(ref IM.Number rate, IM.Number value)
	{
		Debugger.Instance.m_steamer.message += "Rate before: " + rate;
        IM.Number imRate = rate;
        IM.Number imValue =value;
        if (imRate <= IM.Number.zero || imRate >= IM.Number.one)
			return false;
        IM.Number expect = imRate * imValue;
		expectSum += expect;
        imRate = (expectSum - valueSum) / imValue;
		return true;
	}

    public bool AdjustRate(ref IM.Number rate)
    {
        return AdjustRate(ref rate, IM.Number.one);
    }

	public void SumValue(IM.Number value) 
	{
		valueSum += value;
	}

    public void SumValue()
    {
        SumValue(IM.Number.one);
    }

	public void Clear()
	{
		expectSum = IM.Number.zero;
		valueSum = IM.Number.zero;
	}
}

public class PseudoRandomGroup
{
	Dictionary<Player, PseudoRandom> map = new Dictionary<Player, PseudoRandom>();

	public PseudoRandom this[Player target]
	{
		get
		{
			PseudoRandom random = null;
			map.TryGetValue(target, out random);
			if (random == null)
			{
				random = new PseudoRandom();
				map.Add(target, random);
			}
			return random;
		}
	}
}
