using UnityEngine;

public class HedgingHelper
{
	string name;
    IM.Number resultMin;
    IM.Number resultMax;
    IM.Number resultDefault;
    IM.Number factorMin;
    IM.Number factorMax;

	public HedgingHelper(string func, string subFunc = "")
	{
		name = func;
		if (subFunc != "")
			name = name + "(" + subFunc + ")";
		HedgingConfig config = GameSystem.Instance.HedgingConfig;
		resultMin = config.GetAttribute("resultMin", func);
		resultMax = config.GetAttribute("resultMax", func);
		if (string.IsNullOrEmpty(subFunc))
			resultDefault = config.GetAttribute("resultDefault", func);
		else
			resultDefault = config.GetAttribute("resultDefault", func + "/" + subFunc);
		factorMin = config.factorMin;
		factorMax = config.factorMax;
		Logger.Log("Hedging, " + name + ", resultMin:" + resultMin + " resultMax:" + resultMax +
			" resultDefault:" + resultDefault + " factorMin:" + factorMin + " factorMax:" + factorMax);
	}

	public IM.Number Calc(IM.Number value1, IM.Number value2)
	{
        IM.Number factor = value1 / value2;
		IM.Number result;
		if (factor > 1)
		{
			result = resultDefault + (resultMax - resultDefault) * (factor - 1) / (factorMax - 1);
		}
		else
		{
			result = resultMin + (resultDefault - resultMin) * (factor - factorMin) / (IM.Number.one - factorMin);
		}
        result = IM.Math.Clamp(result, resultMin, resultMax);
		Logger.Log("Hedging calc, " + name + ", " + value1 + ", " + value2 + ", " + result);
		return result;
	}
}
