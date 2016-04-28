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
            //AddStep("Quaternion.Euler", TestEuler);
            //AddStep("Quaternion.eulerAngles", TestEulerAngles);
            //AddStep("Quaternion.AngleAxis", TestAngleAxis);
            AddStep("Quaternion.MultiplyVector3", TestMultiplyVector3);
        }

        bool TestEuler()
        {
            var tester = Utils.GenerateTester("Euler",
                Quaternion.Euler, UE.Quaternion.Euler,
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, DevMode.Absolute, 0.003f);
            return Utils.TestRandom(tester, 100);
            //return tester.test(new Vector3(-17238, -22931, -21492));
        }

        //bool TestEuler()
        //{
        //    var tester = Utils.GenerateTester("Euler",
        //        (Vector3 vec) => Convert(Quaternion.Eulerf((UE.Vector3)vec)), 
        //        (UE.Vector3 vec) => UE.Quaternion.Euler(vec.x * UE.Mathf.Rad2Deg, vec.y * UE.Mathf.Rad2Deg, vec.z * UE.Mathf.Rad2Deg),
        //        Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, DevMode.Absolute, 0.0001f);
        //    //return Utils.TestRandom(tester, 100);
        //    return tester.test(new Vector3(-17374, -19215, -21096));
        //}
        //Quaternion Convert(UE.Quaternion q)
        //{
        //    return new Quaternion(
        //        UE.Mathf.RoundToInt(q.x * Math.FACTOR),
        //        UE.Mathf.RoundToInt(q.y * Math.FACTOR),
        //        UE.Mathf.RoundToInt(q.z * Math.FACTOR),
        //        UE.Mathf.RoundToInt(q.w * Math.FACTOR));
        //}

        /*
        bool TestEulerAngles()
        {
            var tester = Utils.GenerateTester("eulerAngles",
                (Vector3 e) => Quaternion.Euler(e).eulerAngles,
                (UE.Vector3 e) =>
                {
                    Vector3 v = new Vector3(UE.Mathf.RoundToInt(e.x * Math.FACTOR),
                        UE.Mathf.RoundToInt(e.y * Math.FACTOR),
                        UE.Mathf.RoundToInt(e.z * Math.FACTOR));
                    Quaternion q = Quaternion.Euler(v);
                    UE.Quaternion uq = (UE.Quaternion)q;
                    UE.Vector3 vec = uq.eulerAngles;
                    return vec;
                },
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, 1, DevMode.AbsoluteWrap360, 0f);
            //return Utils.TestRandom(tester, 100);
            return tester.test(new Vector3(-17238, -22931, -21492));
        }
        */

        bool TestEulerAngles()
        {
            var tester = Utils.GenerateTester("eulerAngles",
                (Vector3 e) => Quaternion.Euler(e).eulerAngles,
                (UE.Vector3 e) => UE.Quaternion.Euler(e).eulerAngles,
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, 1, DevMode.AbsoluteWrap360, 10f);
            return Utils.TestRandom(tester, 100);
        }

        bool TestAngleAxis()
        {
            var tester = Utils.GenerateTester("AngleAxis", 
                (int angle, Vector3 axis) => Quaternion.AngleAxis(angle, axis.normalized),
                (float angle, UE.Vector3 axis) => UE.Quaternion.AngleAxis(angle, axis.normalized),
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, 
                Math.FACTOR, DevMode.Absolute, 0.002f);
            return Utils.TestRandom(tester, 100);
        }

        bool TestMultiplyVector3()
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
            return Utils.TestCritical(tester) && Utils.TestRandom(tester, 100);
        }
    }
}
