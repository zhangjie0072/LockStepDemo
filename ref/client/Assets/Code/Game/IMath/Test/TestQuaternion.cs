using System.Collections;
using UE = UnityEngine;

namespace IM.Test
{
    public class TestQuaternion : UnitTest
    {
        public override string unitName { get { return "Quaternion"; } }

        public override void PrepareSteps()
        {
            AddStep("Quaternion.Euler", TestEuler);
            AddStep("Quaternion.eulerAngles", TestEulerAngles);
            AddStep("Quaternion.AngleAxis", TestAngleAxis);
            AddStep("Quaternion.MultiplyVector3", TestMultiplyVector3);
            AddStep("Quaternion.MultiplyQuaternion", TestMultiplyQuaternion);
            AddStep("Quaternion.LookRotation", TestLookRotation);
        }

        bool TestEuler(bool longTime)
        {
            var tester = Utils.GenerateTester("Euler",
                (Func1<Vector3, Quaternion>)Quaternion.Euler, UE.Quaternion.Euler,
                Math.MIN_LENGTH, Math.MAX_LENGTH, DevMode.Absolute, new Number(0, 3));
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
                Math.MIN_LENGTH, Math.MAX_LENGTH, DevMode.AbsoluteWrap360, new Number(11));
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestRandom(tester, 100);
        }

        bool TestAngleAxis(bool longTime)
        {
            var tester = Utils.GenerateTester("AngleAxis", 
                (Number angle, Vector3 axis) => Quaternion.AngleAxis(angle, axis.normalized),
                (float angle, UE.Vector3 axis) => UE.Quaternion.AngleAxis(angle, axis.normalized),
                -Math.TWO_PI, Math.TWO_PI, Math.MIN_LENGTH, Math.MAX_LENGTH, DevMode.Absolute, new Number(0, 005));
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
                Math.MIN_LENGTH, Math.MAX_LENGTH, DevMode.Absolute, new Number(0, 6));

            if (!tester.test(Vector3.zero, new Vector3(-Number.one, -Number.one, -Number.one)))
                return false;
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
                Math.MIN_LENGTH, Math.MAX_LENGTH, DevMode.Absolute, new Number(0, 4));
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestCritical(tester) && Utils.TestRandom(tester, 100);
        }

        bool TestLookRotation(bool longTime)
        {
            var tester = Utils.GenerateTester("LookRotation", 
                (Func1<Vector3, Quaternion>)Quaternion.LookRotation, UE.Quaternion.LookRotation,
                Math.MIN_LENGTH, Math.MAX_LENGTH, DevMode.Absolute, new Number(0, 010));
            return Utils.TestRandom(tester, 100);
        }
    }
}
