using System;
using UE = UnityEngine;

namespace IM.Test
{
    public static class Utils
    {
        public static Tester<int> GenerateTester(
            string name, Func1<int> testFunc, Func1<float> refFunc,
            int minValue, int maxValue, int factor, DevMode devMode, float maxDev)
        {
            TestFunc1<int> test = (int x) =>
            {
                int result = testFunc(x);
                float refResult = refFunc((float)x / factor) * factor;
                return CheckResult(name, x, (float)result, refResult, minValue, maxValue, devMode, maxDev);
            };
            Tester<int> tester = new Tester<int>();
            tester.name = name;
            tester.minValue = minValue;
            tester.maxValue = maxValue;
            tester.test = test;
            return tester;
        }
        public static Tester<int, int> GenerateTester(
            string name, Func2<int, int> testFunc, Func2<int, int> refFunc,
            int minValue1, int maxValue1, int minValue2, int maxValue2,
            int factor, DevMode devMode, float maxDev)
        {
            TestFunc2<int, int> test = (int x, int y) =>
            {
                int result = testFunc(x, y);
                int refResult = refFunc(x, y);
                return CheckResult(name, System.Math.Max(x, y), result, refResult, minValue1, maxValue1, devMode, maxDev);
            };
            Tester<int, int> tester = new Tester<int, int>();
            tester.name = name;
            tester.minValue1 = minValue1;
            tester.maxValue1 = maxValue1;
            tester.minValue2 = minValue2;
            tester.maxValue2 = maxValue2;
            tester.test = test;
            return tester;
        }
        public static Tester<Vector3> GenerateTester(
            string name, Func1<Vector3> testFunc, Func1<UE.Vector3> refFunc,
            int minValue, int maxValue, int factor, DevMode devMode, float maxDev)
        {
            TestFunc1<Vector3> test = (Vector3 x) =>
            {
                Vector3 result = testFunc(x);
                UE.Vector3 refResult = refFunc((UE.Vector3)x / factor) * factor;
                return CheckResult(name, x, (UE.Vector3)result, refResult, minValue, maxValue, devMode, maxDev);
            };
            Tester<Vector3> tester = new Tester<Vector3>();
            tester.name = name;
            tester.minValue = minValue;
            tester.maxValue = maxValue;
            tester.test = test;
            return tester;
        }
        public static Tester<Vector3, Vector3> GenerateTester(
            string name, Func2<Vector3, Vector3> testFunc, Func2<UE.Vector3, UE.Vector3> refFunc,
            int minValue, int maxValue, int factor, DevMode devMode, float maxDev)
        {
            TestFunc2<Vector3, Vector3> test = (Vector3 x, Vector3 y) =>
            {
                Vector3 result = testFunc(x, y);
                UE.Vector3 refResult = refFunc((UE.Vector3)x / factor, (UE.Vector3)y / factor);
                return CheckResult(name, x, y, (UE.Vector3)result, refResult, minValue, maxValue, devMode, maxDev);
            };
            Tester<Vector3, Vector3> tester = new Tester<Vector3, Vector3>();
            tester.name = name;
            tester.minValue1 = new Vector3(minValue);
            tester.maxValue1 = new Vector3(maxValue);
            tester.minValue2 = new Vector3(minValue);
            tester.maxValue2 = new Vector3(maxValue);
            tester.test = test;
            return tester;
        }

        public static bool TestCritical(Tester<int> tester)
        {
            Logger.Log("IMath test, Test critical:" + tester.name);
            if (!tester.test(tester.minValue))
                return false;
            if (!tester.test(tester.maxValue))
                return false;
            if (tester.minValue < 0 && 0 < tester.maxValue && !tester.test(0))
                return false;
            return true;
        }
        public static bool TestCritical(Tester<int, int> tester)
        {
            Logger.Log("IMath test, Test critical:" + tester.name);
            for (int i = 0; i < 3; ++i)
            {
                int x = 0;
                if (i == 0)
                    x = tester.minValue1;
                else if (i == 1)
                {
                    if (tester.minValue1 < 0 && 0 < tester.maxValue1)
                        x = 0;
                    else
                        continue;
                }
                else if (i == 2)
                    x = tester.maxValue1;
                for (int j = 0; j < 3; ++j)
                {
                    int y = 0;
                    if (j == 0)
                        y = tester.minValue2;
                    else if (j == 1)
                    {
                        if (tester.minValue2 < 0 && 0 < tester.maxValue2)
                            y = 0;
                        else
                            continue;
                    }
                    else if (j == 2)
                        y = tester.maxValue2;

                    if (!tester.test(x, y))
                        return false;
                }
            }
            return true;
        }
        public static bool TestCritical(Tester<Vector3> tester)
        {
            Logger.Log("IMath test, Test critical:" + tester.name);
            bool zeroInRange = tester.minValue < 0 && 0 < tester.maxValue;
            if (!tester.test(new Vector3(tester.minValue, tester.minValue, tester.minValue)))
                return false;
            if (!tester.test(new Vector3(tester.minValue, tester.minValue, tester.maxValue)))
                return false;
            if (!tester.test(new Vector3(tester.minValue, tester.maxValue, tester.minValue)))
                return false;
            if (!tester.test(new Vector3(tester.maxValue, tester.minValue, tester.minValue)))
                return false;
            if (!tester.test(new Vector3(tester.maxValue, tester.maxValue, tester.minValue)))
                return false;
            if (!tester.test(new Vector3(tester.maxValue, tester.minValue, tester.maxValue)))
                return false;
            if (!tester.test(new Vector3(tester.minValue, tester.maxValue, tester.maxValue)))
                return false;
            if (!tester.test(new Vector3(tester.maxValue, tester.maxValue, tester.maxValue)))
                return false;
            if (zeroInRange)
            {
                if (!tester.test(new Vector3(0, tester.minValue, tester.minValue)))
                    return false;
                if (!tester.test(new Vector3(0, tester.minValue, tester.maxValue)))
                    return false;
                if (!tester.test(new Vector3(0, tester.maxValue, tester.minValue)))
                    return false;
                if (!tester.test(new Vector3(0, tester.maxValue, tester.maxValue)))
                    return false;
                if (!tester.test(new Vector3(tester.minValue, 0, tester.minValue)))
                    return false;
                if (!tester.test(new Vector3(tester.minValue, 0, tester.maxValue)))
                    return false;
                if (!tester.test(new Vector3(tester.maxValue, 0, tester.minValue)))
                    return false;
                if (!tester.test(new Vector3(tester.maxValue, 0, tester.maxValue)))
                    return false;
                if (!tester.test(new Vector3(tester.minValue, tester.minValue, 0)))
                    return false;
                if (!tester.test(new Vector3(tester.minValue, tester.maxValue, 0)))
                    return false;
                if (!tester.test(new Vector3(tester.maxValue, tester.minValue, 0)))
                    return false;
                if (!tester.test(new Vector3(tester.maxValue, tester.maxValue, 0)))
                    return false;
                if (!tester.test(new Vector3(tester.minValue, 0, 0)))
                    return false;
                if (!tester.test(new Vector3(tester.maxValue, 0, 0)))
                    return false;
                if (!tester.test(new Vector3(0, tester.minValue, 0)))
                    return false;
                if (!tester.test(new Vector3(0, tester.maxValue, 0)))
                    return false;
                if (!tester.test(new Vector3(0, 0, tester.minValue)))
                    return false;
                if (!tester.test(new Vector3(0, 0, tester.maxValue)))
                    return false;
                if (!tester.test(new Vector3(0, 0, 0)))
                    return false;
            }
            return true;
        }
        public static bool TestCritical(Tester<Vector3, Vector3> tester)
        {
            Logger.Log("IMath test, Test critical:" + tester.name);
            if (!tester.test(new Vector3(0, 0, 0), new Vector3(0, 0, 0)))
                return false;
            if (!tester.test(new Vector3(0, 0, 0), new Vector3(tester.minValue2.x, 0, tester.maxValue2.z)))
                return false;
            if (!tester.test(tester.minValue1, tester.minValue2))
                return false;
            if (!tester.test(tester.minValue1, tester.maxValue2))
                return false;
            if (!tester.test(tester.maxValue1, tester.minValue1))
                return false;
            if (!tester.test(tester.maxValue1, tester.maxValue2))
                return false;
            return true;
        }
        public static bool TestSequence(Tester<int> tester, int start, int end, int step)
        {
            Logger.Log(string.Format("IMath test, Test sequence:{0} start:{1} end:{2} step:{3}", 
                tester.name, start, end, step));
            for (int i = start; i <= end; i += step)
            {
                if (!tester.test(i))
                    return false;
            }
            return true;
        }
        public static bool TestRandom(Tester<int> tester, int count)
        {
            Logger.Log("IMath test, Test random:" + tester.name + " count:" + count);
            for (int i = 0; i < count; ++i)
            {
                if (!tester.test(UE.Random.Range(tester.minValue, tester.maxValue)))
                    return false;
            }
            return true;
        }
        public static bool TestRandom(Tester<Vector3> tester, int count)
        {
            Logger.Log("IMath test, Test random:" + tester.name + " count:" + count);
            for (int i = 0; i < count; ++i)
            {
                int x = UE.Random.Range(tester.minValue, tester.maxValue);
                int y = UE.Random.Range(tester.minValue, tester.maxValue);
                int z = UE.Random.Range(tester.minValue, tester.maxValue);
                if (!tester.test(new Vector3(x, y, z)))
                    return false;
            }
            return true;
        }
        public static bool TestRandom(Tester<int, int> tester, int count)
        {
            Logger.Log("IMath test, Test random:" + tester.name + " count:" + count);
            for (int i = 0; i < count; ++i)
            {
                int x = UE.Random.Range(tester.minValue1, tester.maxValue1);
                int y = UE.Random.Range(tester.minValue2, tester.maxValue2);
                if (!tester.test(x, y))
                    return false;
            }
            return true;
        }
        public static bool TestRandom(Tester<Vector3, Vector3> tester, int count)
        {
            Logger.Log("IMath test, Test random:" + tester.name + " count:" + count);
            for (int i = 0; i < count; ++i)
            {
                int x1 = UE.Random.Range(tester.minValue1.x, tester.maxValue1.x);
                int y1 = UE.Random.Range(tester.minValue1.y, tester.maxValue1.y);
                int z1 = UE.Random.Range(tester.minValue1.z, tester.maxValue1.z);
                int x2 = UE.Random.Range(tester.minValue2.x, tester.maxValue2.x);
                int y2 = UE.Random.Range(tester.minValue2.y, tester.maxValue2.y);
                int z2 = UE.Random.Range(tester.minValue2.z, tester.maxValue2.z);
                if (!tester.test(new Vector3(x1, y1, z1), new Vector3(x2, y2, z2)))
                    return false;
            }
            return true;
        }

        static float CalcDev(float diff, int input, float refResult, int minValue, int maxValue, DevMode devMode)
        {
            float dev = 0f;
            if (devMode == DevMode.Input)
                dev = (float)diff / input;
            else if (devMode == DevMode.RefResult)
                dev = (float)diff / refResult;
            else if (devMode == DevMode.ValueRange)
                dev = (float)diff / (maxValue - minValue + 1);
            else if (devMode == DevMode.Absolute)
                dev = (float)diff;
            return System.Math.Abs(dev);
        }

        static bool CheckResult(string name, int input, float result, float refResult,
            int minValue, int maxValue, DevMode devMode, float maxDev)
        {
            float diff = System.Math.Abs(result - refResult);
            float dev = CalcDev(diff, input, refResult, minValue, maxValue, devMode);
            if (dev > maxDev)
            {
                Logger.LogError(string.Format(
                    "TestName:{0} Input:{1} Result:{2} RefResult:{3} Diff:{4} Dev:{5} MaxDev{6}",
                    name, input, result, refResult, diff, dev, maxDev));
                return false;
            }
            return true;
        }
        static bool CheckResult(string name, Vector3 input, UE.Vector3 result, UE.Vector3 refResult,
            int minValue, int maxValue, DevMode devMode, float maxDev)
        {
            UE.Vector3 diff = result - refResult;
            float devX = CalcDev(System.Math.Abs(diff.x), input.x, refResult.x, minValue, maxValue, devMode);
            float devY = CalcDev(System.Math.Abs(diff.y), input.y, refResult.y, minValue, maxValue, devMode);
            float devZ = CalcDev(System.Math.Abs(diff.y), input.y, refResult.y, minValue, maxValue, devMode);
            if (devX > maxDev || devY > maxDev || devZ > maxDev)
            {
                Logger.LogError(string.Format(
                    "TestName:{0} Input:{1} Result:{2} RefResult:{3} Diff:{4} Dev:{5} MaxDev{6}",
                    name, input, result.ToString("F4"), refResult.ToString("F4"), 
                    diff.ToString("F4"), new UE.Vector3(devX, devY, devZ), maxDev));
                return false;
            }
            return true;
        }
        static bool CheckResult(string name, Vector3 input1, Vector3 input2, UE.Vector3 result, UE.Vector3 refResult,
            int minValue, int maxValue, DevMode devMode, float maxDev)
        {
            UE.Vector3 diff = result - refResult;
            float devX = CalcDev(System.Math.Abs(diff.x), input1.x, refResult.x, minValue, maxValue, devMode);
            float devY = CalcDev(System.Math.Abs(diff.y), input1.y, refResult.y, minValue, maxValue, devMode);
            float devZ = CalcDev(System.Math.Abs(diff.z), input1.z, refResult.z, minValue, maxValue, devMode);
            if (devX > maxDev || devY > maxDev || devZ > maxDev)
            {
                Logger.LogError(string.Format(
                    "TestName:{0} Input:{1} {2} Result:{3} RefResult:{4} Diff:{5} Dev:{6} MaxDev{7}",
                    name, input1, input2, result.ToString("F4"), refResult.ToString("F4"),
                    diff.ToString("F4"), new UE.Vector3(devX, devY, devZ).ToString("F4"), maxDev));
                return false;
            }
            return true;
        }
    }
}
