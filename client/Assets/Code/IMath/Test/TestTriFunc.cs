using System.Collections;

namespace IM.Test
{
    public class TestTriFunc : UnitTest
    {

        public override string Name()
        {
            return "Trigonometric functions";
        }

        public override void PrepareSteps()
        {
            //AddStep("Sin", TestSin);
            //AddStep("Cos", TestCos);
            //AddStep("Asin", TestAsin);
            //AddStep("Acos", TestAcos);
            AddStep("Atan", TestAtan);
            AddStep("Atan2", TestAtan2);
        }

        bool TestSin()
        {
            var tester = Utils.GenerateTester("Sin", IM.Math.Sin, UnityEngine.Mathf.Sin,
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, Math.FACTOR, DevMode.Absolute, 0.001f * Math.PI);
            return Utils.TestCritical(tester) && 
                Utils.TestSequence(tester, -Math.HALF_PI, Math.TWO_PI + Math.HALF_PI, Math.TWO_PI / 100);
        }

        bool TestCos()
        {
            var tester = Utils.GenerateTester("Cos", IM.Math.Cos, UnityEngine.Mathf.Cos,
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, Math.FACTOR, DevMode.Absolute, 0.001f * Math.PI);
            return Utils.TestCritical(tester) && 
                Utils.TestSequence(tester, -Math.HALF_PI, Math.TWO_PI + Math.HALF_PI, Math.TWO_PI / 100);
        }

        bool TestAsin()
        {
            var tester = Utils.GenerateTester("Asin", IM.Math.Asin, UnityEngine.Mathf.Asin,
                -Math.FACTOR, Math.FACTOR, Math.FACTOR, DevMode.Absolute, 1f);
            return Utils.TestSequence(tester, -Math.FACTOR, Math.FACTOR, 1);
        }

        bool TestAcos()
        {
            var tester = Utils.GenerateTester("Acos", IM.Math.Acos, UnityEngine.Mathf.Acos,
                -Math.FACTOR, Math.FACTOR, Math.FACTOR, DevMode.Absolute, 1f);
            return Utils.TestSequence(tester, -Math.FACTOR, Math.FACTOR, 1);
        }

        bool TestAtan()
        {
            var tester = Utils.GenerateTester("Atan", IM.Math.Atan, UnityEngine.Mathf.Atan,
                int.MinValue, int.MaxValue, Math.FACTOR, DevMode.Absolute, 45f);
            return Utils.TestCritical(tester) && Utils.TestRandom(tester, 500000);
        }

        bool TestAtan2()
        {
            var tester = Utils.GenerateTester("Atan2", 
                (Func2<int, int>)IM.Math.Atan2, (Func2<float, float>)UnityEngine.Mathf.Atan2,
                int.MinValue / Math.FACTOR, int.MaxValue / Math.FACTOR, 1, int.MaxValue,
                Math.FACTOR, DevMode.Absolute, 45f);
            if (!Utils.TestCritical(tester) || !Utils.TestRandom(tester, 500000))
                return false;

            tester = Utils.GenerateTester("Atan2", 
                (Func2<int, int>)IM.Math.Atan2, (Func2<float, float>)UnityEngine.Mathf.Atan2,
                int.MinValue / Math.FACTOR, int.MaxValue / Math.FACTOR, int.MinValue, -1,
                Math.FACTOR, DevMode.Absolute, 45f);
            return Utils.TestCritical(tester) && Utils.TestRandom(tester, 500000);
        }
    }
}
