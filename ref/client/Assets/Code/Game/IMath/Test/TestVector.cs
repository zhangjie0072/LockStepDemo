using System.Collections;
using UE = UnityEngine;

namespace IM.Test
{
    public class TestVector : UnitTest
    {
        public override string unitName { get { return "Vector"; } }

        public override void PrepareSteps()
        {
            AddStep("Vector3.normalized", TestNormalize);
            AddStep("Vector3.Cross", TestCross);
            AddStep("Vector3.Angle", TestAngle);
            AddStep("Vector3.Lerp", TestLerp);
            //AddStep("Vector3.RotateTowards", TestRotateTowards);
            //AddStep("Vector3.FromToAngle", TestFromToAngle);
        }

        bool TestNormalize(bool longTime)
        {
            var tester = Utils.GenerateTester("Normalize",
                (Vector3 vec) => vec.normalized, (UE.Vector3 vec) => vec.normalized,
                Math.MIN_LENGTH, Math.MAX_LENGTH, DevMode.Absolute, new Number(0, 1));
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
            if (!tester.test(
                new Vector3(new Number(5, 253), new Number(-11, 984), new Number(17, 218)),
                new Vector3(new Number(-5, 881), new Number(13, 329), new Number(-19, 289))))
                return false;
            if (!tester.test(new Vector3(-Number.one, -Number.one, -Number.one), new Vector3(Number.one, Number.one, Number.one)))
                return false;
            if (!tester.test(Vector3.forward, Vector3.forward))
                return false;
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
                Math.MIN_LENGTH, Math.MAX_LENGTH, DevMode.Absolute, new Number(4));
            if (!tester.test(
                new Vector3(new Number(5, 253), new Number(-11, 984), new Number(17, 218)),
                new Vector3(new Number(-5, 881), new Number(13, 329), new Number(-19, 289))))
                return false;
            if(!tester.test(new Vector3(-Number.one, -Number.one, -Number.one), new Vector3(Number.one, Number.one, Number.one)))
                return false;
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestCritical(tester) && Utils.TestRandom(tester, 1000);
        }

        bool TestLerp(bool longTime)
        {
            var tester = Utils.GenerateTester("Vector3.Lerp", Vector3.Lerp, UE.Vector3.Lerp, 
                Math.MIN_LENGTH, Math.MAX_LENGTH, Number.zero, Number.one, DevMode.Absolute, new Number(0, 2));
            if (longTime)
                return Utils.TestSequence(tester);
            else
                return Utils.TestRandom(tester, 1000);
        }

        bool TestRotateTowards(bool longTime)
        {
            /*
            Debug.DrawLine("axis-X", (UE.Vector3.zero), UE.Vector3.right * 30, UE.Color.red);
            Debug.DrawLine("axis-Y", (UE.Vector3.zero), UE.Vector3.up * 30, UE.Color.green);
            Debug.DrawLine("axis-Z", (UE.Vector3.zero), UE.Vector3.forward * 30, UE.Color.blue);
            //*/

            var tester = Utils.GenerateTester("Vector3.RotateTowards",
                Vector3.RotateTowards, UE.Vector3.RotateTowards,
                Math.MIN_LENGTH, Math.MAX_LENGTH, Number.zero, Math.PI, Number.zero, Math.MAX_LENGTH,
                DevMode.Absolute, new Number(2, 200));

            return tester.test(
                new Vector3(new Number(-1, 971), Number.zero, new Number(4, 542)),
                new Vector3(new Number(4, 068), Number.zero, new Number(-9, 295)),
                new Number(0, 255), new Number(0, 115));
            /*  接近平行反向
            return tester.test(
                new Vector3(new Number(21, 986), Number.zero, new Number(19, 161)),
                new Vector3(new Number(-21, 887), Number.zero, new Number(-17, 003)),
                new Number(1, 079), new Number(1, 260));
            //*/
            /* 接近平行反向
            return tester.test(
                new Vector3(new Number(15, 532), Number.zero, new Number(16, 200)),
                new Vector3(new Number(-20, 684), Number.zero, new Number(-22, 059)),
                new Number(1, 626), new Number(19, 117));
            //*/
            /*  //接近反向平行，Unity未视为反向平行，夹角180.024度
            return tester.test(
                new Vector3(new Number(3, 161), new Number(10, 130), new Number(9, 351)),
                new Vector3(new Number(-2, 371), new Number(-10, 357), new Number(-9, 623)),
                new Number(1, 603), new Number(6, 497));
            //*/
            /*  //接近反向平行，作为反向平行处理后，与Unity旋转轴相反
            Vector3 v1 = new Vector3(new Number(5, 253), new Number(-11, 984), new Number(17, 218));
            Vector3 v2 = new Vector3(new Number(-5, 881), new Number(13, 329), new Number(-19, 289));
            Number maxRad = Math.HALF_PI + Math.HALF_PI / new Number(2);
            Number maxMag = Number.zero;
            return tester.test(v1, v2, maxRad, maxMag);
            //*/

            //return Utils.TestRandom(tester, 1);

            if (!Utils.TestRandom(tester, 1000))
                return false;

            tester = Utils.GenerateTester("Vector3.RotateTowards(Small)", Vector3.RotateTowards, UE.Vector3.RotateTowards, 
                Math.MIN_LENGTH / new Number(2), Math.MAX_LENGTH / new Number(2), 
                Number.zero, Math.PI, Number.zero, Math.MAX_LENGTH / new Number(2),
                DevMode.Absolute, new Number(0, 860));
            if (!Utils.TestRandom(tester, 1000))
                return false;

            tester = Utils.GenerateTester("Vector3.RotateTowards(Very Small)", Vector3.RotateTowards, UE.Vector3.RotateTowards, 
                Math.MIN_LENGTH / new Number(3), Math.MAX_LENGTH / new Number(3), 
                Number.zero, Math.PI, Number.zero, Math.MAX_LENGTH / new Number(3),
                DevMode.Absolute, new Number(1, 500));
            /*
            Number x = new Number(-5);
            Number z = new Number(5);
            if (!tester.test(
                new Vector3(x, Number.zero, z),
                new Vector3(-x, Number.zero, -z),
                Math.PI * new Number(0, 700), new Number(0, 100)))
                return false;
            //*/
            //* 测试平行反向的两个向量
            for (Number x = new Number(-10); x <= new Number(10); x += Number.one)
            {
                Number z = new Number(10) - Math.Abs(x);
                if (!tester.test(
                    new Vector3(x, Number.zero, z),
                    new Vector3(-x, Number.zero, -z),
                    Math.PI * new Number(0, 700), new Number(0, 100)))
                    return false;
                z = -z;
                if (!tester.test(
                    new Vector3(x, Number.zero, z),
                    new Vector3(-x, Number.zero, -z),
                    Math.PI * new Number(0, 700), new Number(0, 100)))
                    return false;
            }
            //*/
            //* 测试上转下和下转上
            if (!tester.test(
                Vector3.up, Vector3.down,
                Math.HALF_PI, new Number(0, 100)))
                return false;
            if (!tester.test(
                Vector3.down, Vector3.up,
                Math.HALF_PI, new Number(0, 100)))
                return false;
            //*/
            return Utils.TestRandom(tester, 1000);
            return true;
        }

        bool TestFromToAngle(bool longTime)
        {
            var tester = Utils.GenerateTester("Vector3.FromToAngle",
                (Vector3 from, Vector3 to) =>
                {
                    from.y = Number.zero;
                    from.Normalize();
                    to.y = Number.zero;
                    to.Normalize();
                    Number crossy = Vector3.Cross(from, to).y;
                    return Vector3.FromToAngle(from, to);
                },
                (UE.Vector3 from, UE.Vector3 to) =>
                {
                    from.y = 0;
                    from.Normalize();
                    to.y = 0;
                    to.Normalize();
                    UE.Vector3 eulerAngles = UE.Quaternion.FromToRotation(from, to).eulerAngles;
                    return eulerAngles.y;
                }, Math.MIN_LENGTH, Math.MAX_LENGTH, DevMode.AbsoluteWrap360, new Number(4, 100));

            //return tester.test(new Vector3(new Number(-14,818), Number.zero, new Number(-11,020)),
            //    new Vector3(new Number(17,697), Number.zero, new Number(-14,693)));
            //return tester.test(new Vector3(new Number(-11, 907), Number.zero, new Number(-22, 603)),
            //    new Vector3(new Number(11, 769), Number.zero, new Number(22, 305)));
            return Utils.TestRandom(tester, 1000);
        }
    }
}
