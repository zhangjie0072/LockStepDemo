using System.Collections;
using UE = UnityEngine;

namespace IM.Test
{
    public class TestVector : UnitTest
    {
        public override string unitName { get { return "Vector"; } }

        public override void PrepareSteps()
        {
            //AddStep("Vector3.normalized", TestNormalize);
            AddStep("Vector3.Cross", TestCross);
            AddStep("Vector3.Angle", TestAngle);
            //AddStep("Vector3.Lerp", TestLerp);
            AddStep("Vector3,RotateTowards", TestRotateTowards);
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
                Math.MIN_LENGTH, Math.MAX_LENGTH, DevMode.Absolute, new Number(3));
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
            var tester = Utils.GenerateTester("Vector3.RotateTowards", Vector3.RotateTowards, UE.Vector3.RotateTowards, 
                Math.MIN_LENGTH, Math.MAX_LENGTH, Number.zero, Math.PI, Number.zero, Math.MAX_LENGTH,
                DevMode.Absolute, new Number(2, 200));
            if (!tester.test(
                new Vector3(new Number(5, 253), new Number(-11, 984), new Number(17, 218)),
                new Vector3(new Number(-5, 881), new Number(13, 329), new Number(-19, 289)),
                new Number(2, 142), new Number(13, 646))
                )
                return false;
            if (longTime)
            {
                if (!Utils.TestSequence(tester))
                    return false;
            }
            else
            {
                if (!Utils.TestRandom(tester, 1000))
                    return false;
            }

            tester = Utils.GenerateTester("Vector3.RotateTowards(Small)", Vector3.RotateTowards, UE.Vector3.RotateTowards, 
                Math.MIN_LENGTH / new Number(2), Math.MAX_LENGTH / new Number(2), 
                Number.zero, Math.PI, Number.zero, Math.MAX_LENGTH / new Number(2),
                DevMode.Absolute, new Number(0, 860));
            if (!Utils.TestRandom(tester, 1000))
                return false;

            tester = Utils.GenerateTester("Vector3.RotateTowards(Very Small)", Vector3.RotateTowards, UE.Vector3.RotateTowards, 
                Math.MIN_LENGTH / new Number(3), Math.MAX_LENGTH / new Number(3), 
                Number.zero, Math.PI, Number.zero, Math.MAX_LENGTH / new Number(3),
                DevMode.Absolute, new Number(0, 330));
            if (!tester.test(
                new Vector3(Number.one, new Number(0, 100), Number.one),
                new Vector3(-Number.one, new Number(0, -100), -Number.one),
                Math.HALF_PI, new Number(0, 100)))
                return false;
            if (!tester.test(
                Vector3.up, Vector3.down,
                Math.HALF_PI, new Number(0, 100)))
                return false;
            if (!tester.test(
                Vector3.down, Vector3.up,
                Math.HALF_PI, new Number(0, 100)))
                return false;
            return Utils.TestRandom(tester, 1000);
        }
    }
}
