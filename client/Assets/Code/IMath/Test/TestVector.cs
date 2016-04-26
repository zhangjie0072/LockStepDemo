using System.Collections;
using UE = UnityEngine;

namespace IM.Test
{
    public class TestVector : UnitTest
    {
        public override string Name()
        {
            return "Vector";
        }

        public override void PrepareSteps()
        {
            AddStep("Vector3.normalized", TestNormalize);
            AddStep("Vector3.Cross", TestCross);
            AddStep("Vector3.Angle", TestAngle);
        }

        bool TestNormalize()
        {
            var tester = Utils.GenerateTester("Normalize",
                (Vector3 vec) => vec.normalized, (UE.Vector3 vec) => vec.normalized,
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, 1, DevMode.Absolute, 0.001f);
            return Utils.TestCritical(tester) && Utils.TestRandom(tester, 100);
        }

        bool TestCross()
        {
            var tester = Utils.GenerateTester("Cross", Vector3.Cross, 
                (UE.Vector3 v1, UE.Vector3 v2) => UE.Vector3.Cross(v1, v2).normalized,
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, 1, DevMode.Absolute, 0.02f);
            return Utils.TestCritical(tester) && Utils.TestRandom(tester, 1000);
        }

        bool TestAngle()
        {
            var tester = Utils.GenerateTester("Vector3.Angle",
                (Func2<Vector3, Vector3, int>)((Vector3 v1, Vector3 v2) => Vector3.Angle(v1, v2)),
                (Func2<UE.Vector3, UE.Vector3, float>)((UE.Vector3 v1, UE.Vector3 v2) => UE.Vector3.Angle(v1.normalized, v2.normalized) * Math.FACTOR),
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, 1, DevMode.Absolute, 3000f);
            return Utils.TestCritical(tester) && Utils.TestRandom(tester, 10000);
        }
    }
}
