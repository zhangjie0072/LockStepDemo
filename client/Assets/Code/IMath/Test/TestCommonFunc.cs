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
            //AddStep("fastInvSqrt", TestFastInvSqrt);
            AddStep("Deg2Rad", TestDeg2Rad);
            AddStep("Rad2Deg", TestRad2Deg);
            AddStep("Sqrt", TestSqrt);
            AddStep("RoundDivide", TestRoundDivide);
        }

        //bool TestFastInvSqrt()
        //{
        //    const float DEV = 0.001f;
        //    for (int i = 0; i <= 360; ++i)
        //    {
        //        float f = UnityEngine.Random.value * 100000;
        //        float r1 = UnityEngine.Mathf.Sqrt(f);
        //        float r2 = 1 / IM.Math.fastInvSqrt(f);
        //        float diff = UnityEngine.Mathf.Abs(r1 - r2);
        //        if (diff > DEV)
        //        {
        //            Logger.LogError(string.Format("TestFastInvSqrt:{0}, Dev:{1} Diff: {2} r1:{3} r2:{4}", f, DEV, diff, r1, r2));
        //            return false;
        //        }
        //    }
        //    return true;
        //}

        bool TestSqrt()
        {
            var tester = Utils.GenerateTester("Sqrt", IM.Math.Sqrt, UnityEngine.Mathf.Sqrt,
                0, int.MaxValue, 1, DevMode.RefResult, 0.002f);
            return Utils.TestCritical(tester) && Utils.TestRandom(tester, 1000);
        }

        bool TestRoundDivide()
        {
            var tester = Utils.GenerateTester("RoundDivide", 
                (int x, int y) => IM.Math.RndDiv(x, y),
                (int x, int y) => UnityEngine.Mathf.RoundToInt((float)x / y),
                IM.Math.SUPPORTED_MIN, IM.Math.SUPPORTED_MAX, 1, IM.Math.SUPPORTED_MAX,
                1, DevMode.Absolute, 0f);
            if (!(Utils.TestCritical(tester) && Utils.TestRandom(tester, 1000)))
                return false;
            tester = Utils.GenerateTester("RoundDivide", 
                (int x, int y) => IM.Math.RndDiv(x, y),
                (int x, int y) => UnityEngine.Mathf.RoundToInt((float)x / y),
                IM.Math.SUPPORTED_MIN, IM.Math.SUPPORTED_MAX, IM.Math.SUPPORTED_MIN, -1,
                1, DevMode.Absolute, 0f);
            if (!(Utils.TestCritical(tester) && Utils.TestRandom(tester, 1000)))
                return false;
            return true;
        }

        bool TestDeg2Rad()
        {
            var tester = Utils.GenerateTester("Deg2Rad", 
                IM.Math.Deg2Rad, (float x) => UnityEngine.Mathf.Deg2Rad * x,
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, Math.FACTOR, DevMode.Absolute, Math.TWO_PI / 360);
            return Utils.TestSequence(tester, -180, 720 * Math.FACTOR, Math.FACTOR >> 1);
        }

        bool TestRad2Deg()
        {
            var tester = Utils.GenerateTester("Rad2Deg", 
                IM.Math.Rad2Deg, (float x) => UnityEngine.Mathf.Rad2Deg * x,
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, Math.FACTOR, DevMode.Absolute, Math.FACTOR);
            return Utils.TestSequence(tester, -Math.PI, Math.TWO_PI + Math.PI, Math.TWO_PI / 720);
        }
    }
}
