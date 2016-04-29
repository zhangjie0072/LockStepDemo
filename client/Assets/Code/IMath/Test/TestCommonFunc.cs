using System.Collections;

namespace IM.Test
{
    public class TestCommonFunc : UnitTest
    {
        public override string Name()
        {
            return "Common functions";
        }

        public override void PrepareSteps()
        {
            AddStep("Deg2Rad", TestDeg2Rad);
            AddStep("Rad2Deg", TestRad2Deg);
            AddStep("Sqrt", TestSqrt);
            AddStep("RoundDivide", TestRoundDivide);
        }

        bool TestSqrt(bool longTime)
        {
            var tester = Utils.GenerateTester("Sqrt", IM.Math.Sqrt, UnityEngine.Mathf.Sqrt,
                0, int.MaxValue, 1, DevMode.Absolute, 1f);
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestCritical(tester) && Utils.TestRandom(tester, 1000);
        }

        bool TestRoundDivide(bool longTime)
        {
            var tester = Utils.GenerateTester("RoundDivide", 
                (int x, int y) => IM.Math.RndDiv(x, y),
                (int x, int y) => UnityEngine.Mathf.RoundToInt((float)x / y),
                IM.Math.SUPPORTED_MIN, IM.Math.SUPPORTED_MAX, 1, IM.Math.SUPPORTED_MAX,
                1, DevMode.Absolute, 0f);
            if (longTime)
            {
                if (!Utils.TestSequence(tester))
                    return false;
            }
            else
            {
                if (!(Utils.TestCritical(tester) && Utils.TestRandom(tester, 1000)))
                    return false;
            }
            tester = Utils.GenerateTester("RoundDivide", 
                (int x, int y) => IM.Math.RndDiv(x, y),
                (int x, int y) => UnityEngine.Mathf.RoundToInt((float)x / y),
                IM.Math.SUPPORTED_MIN, IM.Math.SUPPORTED_MAX, IM.Math.SUPPORTED_MIN, -1,
                1, DevMode.Absolute, 0f);
            if (longTime)
            {
                if (!Utils.TestSequence(tester))
                    return false;
            }
            else
            {
                if (!(Utils.TestCritical(tester) && Utils.TestRandom(tester, 1000)))
                    return false;
            }
            return true;
        }

        bool TestDeg2Rad(bool longTime)
        {
            var tester = Utils.GenerateTester("Deg2Rad", 
                IM.Math.Deg2Rad, (float x) => UnityEngine.Mathf.Deg2Rad * x,
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, Math.FACTOR, DevMode.Absolute, Math.TWO_PI / 360);
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestSequence(tester, -180, 720 * Math.FACTOR, Math.FACTOR >> 1);
        }

        bool TestRad2Deg(bool longTime)
        {
            var tester = Utils.GenerateTester("Rad2Deg", 
                IM.Math.Rad2Deg, (float x) => UnityEngine.Mathf.Rad2Deg * x,
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, Math.FACTOR, DevMode.Absolute, Math.FACTOR);
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestSequence(tester, -Math.PI, Math.TWO_PI + Math.PI, Math.TWO_PI / 720);
        }
    }
}
