using System.Collections;
using UE = UnityEngine;

namespace IM.Test
{
    public class TestQuaternion : UnitTest
    {
        public override string Name()
        {
            return "Quaternion";
        }

        public override void PrepareSteps()
        {
            AddStep("Quaternion.Euler", TestEuler);
            AddStep("Quaternion.eulerAngles", TestEulerAngles);
            AddStep("Quaternion.AngleAxis", TestAngleAxis);
            AddStep("Quaternion.MultiplyVector3", TestMultiplyVector3);
            AddStep("Quaternion.MultiplyQuaternion", TestMultiplyQuaternion);
        }

        bool TestEuler(bool longTime)
        {
            var tester = Utils.GenerateTester("Euler",
                Quaternion.Euler, UE.Quaternion.Euler,
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, DevMode.Absolute, 0.003f);
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestRandom(tester, 100);
        }

        bool TestEulerAngles(bool longTime)
        {
            var tester = Utils.GenerateTester("eulerAngles",
                (Vector3 e) => Quaternion.Euler(e).eulerAngles,
                (UE.Vector3 e) => UE.Quaternion.Euler(e).eulerAngles,
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, 1, DevMode.AbsoluteWrap360, 10f);
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestRandom(tester, 100);
        }

        bool TestAngleAxis(bool longTime)
        {
            var tester = Utils.GenerateTester("AngleAxis", 
                (int angle, Vector3 axis) => Quaternion.AngleAxis(angle, axis.normalized),
                (float angle, UE.Vector3 axis) => UE.Quaternion.AngleAxis(angle, axis.normalized),
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, 
                Math.FACTOR, DevMode.Absolute, 0.002f);
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestRandom(tester, 100);
        }

        bool TestMultiplyVector3(bool longTime)
        {
            var tester = Utils.GenerateTester("MultiplyVector3",
                (Vector3 euler, Vector3 vec) =>
                {
                    Quaternion q = Quaternion.Euler(euler);
                    Vector3 v = q * vec.normalized;
                    return v;
                },
                (UE.Vector3 euler, UE.Vector3 vec) =>
                {
                    UE.Quaternion q = UE.Quaternion.Euler(euler);
                    UE.Vector3 v = q * vec.normalized;
                    return v;
                },
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, 1, DevMode.Absolute, 0.006f);
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestCritical(tester) && Utils.TestRandom(tester, 100);
        }

        bool TestMultiplyQuaternion(bool longTime)
        {
            var tester = Utils.GenerateTester("MultiplyQuaternion",
                (Vector3 le, Vector3 re) =>
                {
                    Quaternion lhs = Quaternion.Euler(le);
                    Quaternion rhs = Quaternion.Euler(re);
                    return lhs * rhs;
                },
                (UE.Vector3 le, UE.Vector3 re) =>
                {
                    UE.Quaternion lhs = UE.Quaternion.Euler(le);
                    UE.Quaternion rhs = UE.Quaternion.Euler(re);
                    return lhs * rhs;
                },
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, 1, DevMode.Absolute, 0.004f);
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestCritical(tester) && Utils.TestRandom(tester, 100);
        }
    }
}
