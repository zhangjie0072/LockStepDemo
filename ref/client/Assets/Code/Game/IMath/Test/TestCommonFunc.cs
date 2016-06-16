using System.Collections;

namespace IM.Test
{
    public class TestCommonFunc : UnitTest
    {
        public override string unitName { get { return "Common functions"; } }

        public override void PrepareSteps()
        {
            AddStep("Deg2Rad", TestDeg2Rad);
            AddStep("Rad2Deg", TestRad2Deg);
            AddStep("Sqrt", TestSqrt);
            AddStep("RoundDivide", TestRoundDivide);
        }

        bool TestSqrt(bool longTime)
        {
            var tester = Utils.GenerateTester("Sqrt", 
                (Number x) => Number.Raw(IM.Math.Sqrt(x.raw)), 
                (float x) => UnityEngine.Mathf.Sqrt(x * Math.FACTOR) / Math.FACTOR,
                Number.zero, Number.Raw(int.MaxValue), DevMode.Absolute, Number.Raw(2));
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestCritical(tester) && Utils.TestRandom(tester, 1000);
        }

        bool TestRoundDivide(bool longTime)
        {
            var tester = Utils.GenerateTester("RoundDivide", 
                (Number x, Number y) => Number.Raw(IM.Math.RndDiv(x.raw, y.raw)),
                (Number x, Number y) => Number.Raw(UnityEngine.Mathf.RoundToInt((float)x.raw / y.raw)),
                IM.Math.MIN_LENGTH, IM.Math.MAX_LENGTH, Number.one, IM.Math.MAX_LENGTH,
                DevMode.Absolute, Number.zero);
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
                (Number x, Number y) => Number.Raw(IM.Math.RndDiv(x.raw, y.raw)),
                (Number x, Number y) => Number.Raw(UnityEngine.Mathf.RoundToInt((float)x.raw / y.raw)),
                IM.Math.MIN_LENGTH, IM.Math.MAX_LENGTH, IM.Math.MIN_LENGTH, -Number.one,
                DevMode.Absolute, Number.zero);
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
                Math.MIN_LENGTH, Math.MAX_LENGTH, DevMode.Absolute, Math.TWO_PI / new Number(3600));
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestSequence(tester, new Number(-180), new Number(720), Number.Raw(Math.FACTOR >> 1));
        }

        bool TestRad2Deg(bool longTime)
        {
            var tester = Utils.GenerateTester("Rad2Deg",
                IM.Math.Rad2Deg, (float x) => UnityEngine.Mathf.Rad2Deg * x,
                Math.MIN_LENGTH, Math.MAX_LENGTH, DevMode.Absolute, new Number(0, 6));
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestSequence(tester, -Math.PI, Math.TWO_PI + Math.PI, Number.Raw(Math.TWO_PI.raw / 720));
        }
    }
}
