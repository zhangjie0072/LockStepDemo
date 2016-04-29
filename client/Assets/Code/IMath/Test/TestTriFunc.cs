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
            AddStep("Atan", TestAtan);
            AddStep("Atan2", TestAtan2);
        }

        bool TestSin(bool longTime)
        {
            var tester = Utils.GenerateTester("Sin", IM.Math.Sin, UnityEngine.Mathf.Sin,
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, Math.FACTOR, DevMode.Absolute, 0.001f * Math.PI);

            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestCritical(tester) && 
                    Utils.TestSequence(tester, -Math.HALF_PI, Math.TWO_PI + Math.HALF_PI, Math.TWO_PI / 200);
        }

        bool TestCos(bool longTime)
        {
            var tester = Utils.GenerateTester("Cos", IM.Math.Cos, UnityEngine.Mathf.Cos,
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, Math.FACTOR, DevMode.Absolute, 0.001f * Math.PI);
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestCritical(tester) && 
                    Utils.TestSequence(tester, -Math.HALF_PI, Math.TWO_PI + Math.HALF_PI, Math.TWO_PI / 200);
        }

        bool TestAsin(bool longTime)
        {
            var tester = Utils.GenerateTester("Asin", IM.Math.Asin, UnityEngine.Mathf.Asin,
                -Math.FACTOR, Math.FACTOR, Math.FACTOR, DevMode.Absolute, 1f);
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestSequence(tester, -Math.FACTOR, Math.FACTOR, 150);
        }

        bool TestAcos(bool longTime)
        {
            var tester = Utils.GenerateTester("Acos", IM.Math.Acos, UnityEngine.Mathf.Acos,
                -Math.FACTOR, Math.FACTOR, Math.FACTOR, DevMode.Absolute, 1f);
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestSequence(tester, -Math.FACTOR, Math.FACTOR, 150);
        }

        bool TestAtan(bool longTime)
        {
            var tester = Utils.GenerateTester("Atan", IM.Math.Atan, UnityEngine.Mathf.Atan,
                int.MinValue, int.MaxValue, Math.FACTOR, DevMode.Absolute, 45f);
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestCritical(tester) && Utils.TestRandom(tester, 50000);
        }

        bool TestAtan2(bool longTime)
        {
            var tester = Utils.GenerateTester("Atan2(Pos)", 
                (Func2<int, int>)IM.Math.Atan2, (Func2<float, float>)UnityEngine.Mathf.Atan2,
                int.MinValue / Math.FACTOR, int.MaxValue / Math.FACTOR, 1, int.MaxValue,
                Math.FACTOR, DevMode.Absolute, 45f);
            if (longTime)
            {
                if (!Utils.TestSequence(tester))
                    return false;
            }
            else
            {
                if (!Utils.TestCritical(tester) || !Utils.TestRandom(tester, 50000))
                    return false;
            }

            tester = Utils.GenerateTester("Atan2(Neg)", 
                (Func2<int, int>)IM.Math.Atan2, (Func2<float, float>)UnityEngine.Mathf.Atan2,
                int.MinValue / Math.FACTOR, int.MaxValue / Math.FACTOR, int.MinValue, -1,
                Math.FACTOR, DevMode.Absolute, 45f);
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestCritical(tester) && Utils.TestRandom(tester, 500000);
        }
    }
}
