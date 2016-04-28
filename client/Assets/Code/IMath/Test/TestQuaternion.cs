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
        }

        bool TestEuler()
        {
            var tester = Utils.GenerateTester("Euler",
                Quaternion.Euler, UE.Quaternion.Euler,
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, DevMode.Absolute, 0.003f);
            //return Utils.TestRandom(tester, 100);
            return tester.test(new Vector3(-17238, -22931, -21492));
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

        bool TestEulerAngles()
        {
            var tester = Utils.GenerateTester("eulerAngles",
                (Vector3 e) => Quaternion.Euler(e).eulerAngles,
                (UE.Vector3 e) => UE.Quaternion.Euler(e).eulerAngles,
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, 1, DevMode.AbsoluteWrap360, 9f);
            //return Utils.TestRandom(tester, 100);
            return tester.test(new Vector3(-17238, -22931, -21492));
        }
    }
}
