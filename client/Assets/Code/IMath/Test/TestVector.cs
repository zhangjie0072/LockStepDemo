using System.Collections;
using UE = UnityEngine;

namespace IM.Test
{
    public class TestVector : UnitTest
    {
        public override string name { get { return "Vector"; } }

        public override void PrepareSteps()
        {
            AddStep("Vector3.normalized", TestNormalize);
            AddStep("Vector3.Cross", TestCross);
            AddStep("Vector3.Angle", TestAngle);
        }

        bool TestNormalize(bool longTime)
        {
            var tester = Utils.GenerateTester("Normalize",
                (Vector3 vec) => vec.normalized, (UE.Vector3 vec) => vec.normalized,
                Math.MIN_LENGTH, Math.MAX_LENGTH, DevMode.Absolute, new Number(0, 1));
            return tester.test(new Vector3(Math.MIN_LENGTH, Math.MIN_LENGTH, Math.MIN_LENGTH));
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestCritical(tester) && Utils.TestRandom(tester, 100);
        }

        bool TestCross(bool longTime)
        {
            var tester = Utils.GenerateTester("Cross", Vector3.CrossAndNormalize, 
                (UE.Vector3 v1, UE.Vector3 v2) => UE.Vector3.Cross(v1, v2).normalized,
                Math.MIN_LENGTH, Math.MAX_LENGTH, DevMode.Absolute, new Number(0, 20));
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestCritical(tester) && Utils.TestRandom(tester, 100);
        }

        bool TestAngle(bool longTime)
        {
            var tester = Utils.GenerateTester("Vector3.Angle",
                (Vector3 v1, Vector3 v2) => Vector3.Angle(v1, v2),
                (UE.Vector3 v1, UE.Vector3 v2) => UE.Vector3.Angle(v1.normalized, v2.normalized),
                Math.MIN_LENGTH, Math.MAX_LENGTH, DevMode.Absolute, new Number(3));
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestCritical(tester) && Utils.TestRandom(tester, 1000);
        }
    }
}
