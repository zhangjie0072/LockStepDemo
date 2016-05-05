using System.Collections;

namespace IM.Test
{
    public class TestTriFunc : UnitTest
    {

        public override string name { get { return "Trigonometric functions"; } }

        public override void PrepareSteps()
        {
            AddStep("Cos", TestCos);
            AddStep("Sin", TestSin);
            AddStep("Asin", TestAsin);
            AddStep("Acos", TestAcos);
            AddStep("Atan", TestAtan);
            AddStep("Atan2", TestAtan2);
        }

        bool TestSin(bool longTime)
        {
            var tester = Utils.GenerateTester("Sin", IM.Math.Sin, UnityEngine.Mathf.Sin,
                Math.MIN_ANGLE, Math.MAX_ANGLE, DevMode.Absolute, new Number(0, 30));

            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestCritical(tester) && 
                    Utils.TestSequence(tester, Math.MIN_ANGLE, Math.MAX_ANGLE, Math.TWO_PI / new Number(200));
        }

        bool TestCos(bool longTime)
        {
            var tester = Utils.GenerateTester("Cos", IM.Math.Cos, UnityEngine.Mathf.Cos,
                Math.MIN_ANGLE, Math.MAX_ANGLE, DevMode.Absolute, new Number(0, 30));
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestCritical(tester) && 
                    Utils.TestSequence(tester, Math.MIN_ANGLE, Math.MAX_ANGLE, Math.TWO_PI / new Number(200));
        }

        bool TestAsin(bool longTime)
        {
            var tester = Utils.GenerateTester("Asin", IM.Math.Asin, UnityEngine.Mathf.Asin,
                -Number.one, Number.one, DevMode.Absolute, Math.PI / new Number(360));
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestSequence(tester, -Number.one, Number.one, new Number(0, 150));
        }

        bool TestAcos(bool longTime)
        {
            var tester = Utils.GenerateTester("Acos", IM.Math.Acos, UnityEngine.Mathf.Acos,
                -Number.one, Number.one, DevMode.Absolute, Math.PI / new Number(360));
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestSequence(tester, -Number.one, Number.one, new Number(0, 150));
        }

        bool TestAtan(bool longTime)
        {
            var tester = Utils.GenerateTester("Atan", IM.Math.Atan, UnityEngine.Mathf.Atan,
                Number.min, Number.max, DevMode.Absolute, new Number(0, 45));
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestCritical(tester) && Utils.TestRandom(tester, 50000);
        }

        bool TestAtan2(bool longTime)
        {
            var tester = Utils.GenerateTester("Atan2(Pos)", 
                IM.Math.Atan2, UnityEngine.Mathf.Atan2,
                Number.Raw(int.MinValue / Math.FACTOR), Number.Raw(int.MaxValue / Math.FACTOR),
                new Number(0, 1), Number.max, DevMode.Absolute, new Number(0, 45));
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
                IM.Math.Atan2, UnityEngine.Mathf.Atan2,
                Number.Raw(int.MinValue / Math.FACTOR), Number.Raw(int.MaxValue / Math.FACTOR),
                Number.min, new Number(0, -1), DevMode.Absolute, new Number(0, 45));
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestCritical(tester) && Utils.TestRandom(tester, 500000);
        }
    }
}
