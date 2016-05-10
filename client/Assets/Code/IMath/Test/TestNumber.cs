using System.Collections;
using UE = UnityEngine;

namespace IM.Test
{
    public class TestNumber : UnitTest
    {
        public override string unitName { get { return "Number"; } }

        public override void PrepareSteps()
        {
            AddStep("Number.Multiply", TestMultiply);
            AddStep("Number.Division", TestDivision);
        }

        bool TestMultiply(bool longTime)
        {
            var tester = Utils.GenerateTester("Number.Multiply",
                (Number n1, Number n2) => n1 * n2, (float f1, float f2) => f1 * f2,
                Math.MIN_LENGTH, Math.MAX_LENGTH, Math.MIN_LENGTH, Math.MAX_LENGTH, 
                DevMode.Absolute, new Number(0, 1));
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestCritical(tester) && Utils.TestRandom(tester, 1000);
        }

        bool TestDivision(bool longTime)
        {
            var tester = Utils.GenerateTester("Number.Division",
                (Number n1, Number n2) => n1 / n2, (float f1, float f2) => f1 / f2,
                Number.Raw(int.MinValue / Math.FACTOR), Number.Raw(int.MaxValue / Math.FACTOR),
                new Number(0, 1), Number.max, DevMode.Absolute, new Number(0, 250));
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestCritical(tester) && Utils.TestRandom(tester, 1000);
        }
    }
}
