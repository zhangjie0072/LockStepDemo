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
            AddStep("Sin", TestSin);
            AddStep("Cos", TestCos);
            AddStep("Asin", TestAsin);
            AddStep("Acos", TestAcos);
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
    }
}
