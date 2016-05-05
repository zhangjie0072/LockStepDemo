using System;
using UE = UnityEngine;

namespace IM.Test
{
    public static class Utils
    {
        public static Tester<Number> GenerateTester(
            string name, Func1<Number> testFunc, Func1<float> refFunc,
            Number minValue, Number maxValue, DevMode devMode, Number maxDev)
        {
            TestFunc1<Number> test = (Number x) =>
            {
                try
                {
                    Number result = testFunc(x);
                    float f = (float)x;
                    float refResult = refFunc(f);
                    return CheckResult(name, x, (float)result, refResult, minValue, maxValue, devMode, (float)maxDev);
                }
                catch (Exception ex)
                {
                    ExOutput(name, ex, x);
                    return false;
                }
            };
            Tester<Number> tester = new Tester<Number>();
            tester.name = name;
            tester.minValue = minValue;
            tester.maxValue = maxValue;
            tester.test = test;
            return tester;
        }
        public static Tester<Number, Number> GenerateTester(
            string name, Func2<Number, Number> testFunc, Func2<Number, Number> refFunc,
            Number minValue1, Number maxValue1, Number minValue2, Number maxValue2,
            DevMode devMode, Number maxDev)
        {
            TestFunc2<Number, Number> test = (Number x, Number y) =>
            {
                try
                {
                    Number result = testFunc(x, y);
                    Number refResult = refFunc(x, y);
                    return CheckResult(name, x, (float)result, (float)refResult, minValue1, maxValue1, devMode, (float)maxDev);
                }
                catch (Exception ex)
                {
                    ExOutput(name, ex, x, y);
                    return false;
                }
            };
            Tester<Number, Number> tester = new Tester<Number, Number>();
            tester.name = name;
            tester.minValue1 = minValue1;
            tester.maxValue1 = maxValue1;
            tester.minValue2 = minValue2;
            tester.maxValue2 = maxValue2;
            tester.test = test;
            return tester;
        }
        public static Tester<Number, Number> GenerateTester(
            string name, Func2<Number, Number> testFunc, Func2<float, float> refFunc,
            Number minValue1, Number maxValue1, Number minValue2, Number maxValue2,
            DevMode devMode, Number maxDev)
        {
            TestFunc2<Number, Number> test = (Number x, Number y) =>
            {
                try
                {
                    Number result = testFunc(x, y);
                    float refResult = refFunc((float)x, (float)y);
                    return CheckResult(name, x, y, (float)result, refResult, minValue1, maxValue1, devMode, (float)maxDev);
                }
                catch (Exception ex)
                {
                    ExOutput(name, ex, x, y);
                    return false;
                }
            };
            Tester<Number, Number> tester = new Tester<Number, Number>();
            tester.name = name;
            tester.minValue1 = minValue1;
            tester.maxValue1 = maxValue1;
            tester.minValue2 = minValue2;
            tester.maxValue2 = maxValue2;
            tester.test = test;
            return tester;
        }
        public static Tester<Number, Vector3> GenerateTester(
            string name, Func2<Number, Vector3, Quaternion> testFunc, Func2<float, UE.Vector3, UE.Quaternion> refFunc,
            Number minValue1, Number maxValue1, Number minValue2, Number maxValue2, DevMode devMode, Number maxDev)
        {
            TestFunc2<Number, Vector3> test = (Number x, Vector3 y) =>
            {
                try
                {
                    Quaternion result = testFunc(x, y);
                    UE.Quaternion refResult = refFunc((float)x, (UE.Vector3)y);
                    return CheckResult(name, x, y, (UE.Quaternion)result, refResult, minValue1, maxValue1, devMode, (float)maxDev);
                }
                catch (Exception ex)
                {
                    ExOutput(name, ex, x, y);
                    return false;
                }
            };
            Tester<Number, Vector3> tester = new Tester<Number, Vector3>();
            tester.name = name;
            tester.minValue1 = minValue1;
            tester.maxValue1 = maxValue1;
            tester.minValue2 = new Vector3(minValue2);
            tester.maxValue2 = new Vector3(maxValue2);
            tester.test = test;
            return tester;
        }
        public static Tester<Vector3> GenerateTester(
            string name, Func1<Vector3> testFunc, Func1<UE.Vector3> refFunc,
            Number minValue, Number maxValue, DevMode devMode, Number maxDev)
        {
            TestFunc1<Vector3> test = (Vector3 x) =>
            {
                try
                {
                    Vector3 result = testFunc(x);
                    UE.Vector3 refResult = refFunc((UE.Vector3)x);
                    return CheckResult(name, x, (UE.Vector3)result, refResult, minValue, maxValue, devMode, (float)maxDev);
                }
                catch (Exception ex)
                {
                    ExOutput(name, ex, x);
                    return false;
                }
            };
            Tester<Vector3> tester = new Tester<Vector3>();
            tester.name = name;
            tester.minValue = minValue;
            tester.maxValue = maxValue;
            tester.test = test;
            return tester;
        }
        public static Tester<Vector3> GenerateTester(
            string name, Func1<Vector3, Quaternion> testFunc, Func1<UE.Vector3, UE.Quaternion> refFunc,
            Number minValue, Number maxValue, DevMode devMode, Number maxDev)
        {
            TestFunc1<Vector3> test = (Vector3 x) =>
            {
                try
                {
                    Quaternion result = testFunc(x);
                    UE.Quaternion refResult = refFunc((UE.Vector3)x);
                    return CheckResult(name, x, (UE.Quaternion)result, refResult, minValue, maxValue, devMode, (float)maxDev);
                }
                catch (Exception ex)
                {
                    ExOutput(name, ex, x);
                    return false;
                }
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
            Number minValue, Number maxValue, DevMode devMode, Number maxDev)
        {
            TestFunc2<Vector3, Vector3> test = (Vector3 x, Vector3 y) =>
            {
                try
                {
                    Vector3 result = testFunc(x, y);
                    UE.Vector3 refResult = refFunc((UE.Vector3)x, (UE.Vector3)y);
                    return CheckResult(name, x, y, (UE.Vector3)result, refResult, minValue, maxValue, devMode, (float)maxDev);
                }
                catch (Exception ex)
                {
                    ExOutput(name, ex, x, y);
                    return false;
                }
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
        public static Tester<Vector3, Vector3, Number> GenerateTester(
            string name, Func3<Vector3, Vector3, Number> testFunc, Func3<UE.Vector3, UE.Vector3, float> refFunc,
            Number minValue1, Number maxValue1, Number minValue2, Number maxValue2, DevMode devMode, Number maxDev)
        {
            TestFunc3<Vector3, Vector3, Number> test = (Vector3 x, Vector3 y, Number z) =>
            {
                try
                {
                    Vector3 result = testFunc(x, y, z);
                    UE.Vector3 refResult = refFunc((UE.Vector3)x, (UE.Vector3)y, (float)z);
                    return CheckResult(name, x, y, z, (UE.Vector3)result, refResult, minValue1, maxValue1, devMode, (float)maxDev);
                }
                catch (Exception ex)
                {
                    ExOutput(name, ex, x, y, z);
                    return false;
                }
            };
            Tester<Vector3, Vector3, Number> tester = new Tester<Vector3, Vector3, Number>();
            tester.name = name;
            tester.minValue1 = new Vector3(minValue1);
            tester.maxValue1 = new Vector3(maxValue1);
            tester.minValue2 = new Vector3(minValue1);
            tester.maxValue2 = new Vector3(maxValue1);
            tester.minValue3 = minValue2;
            tester.maxValue3 = maxValue2;
            tester.test = test;
            return tester;
        }
        public static Tester<Vector3, Vector3> GenerateTester(
            string name, Func2<Vector3, Vector3, Number> testFunc, Func2<UE.Vector3, UE.Vector3, float> refFunc,
            Number minValue, Number maxValue, DevMode devMode, Number maxDev)
        {
            TestFunc2<Vector3, Vector3> test = (Vector3 x, Vector3 y) =>
            {
                try
                {
                    Number result = testFunc(x, y);
                    float refResult = refFunc((UE.Vector3)x, (UE.Vector3)y);
                    return CheckResult(name, x, y, (float)result, refResult, minValue, maxValue, devMode, (float)maxDev);
                }
                catch (Exception ex)
                {
                    ExOutput(name, ex, x, y);
                    return false;
                }
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
        public static Tester<Vector3, Vector3> GenerateTester(
            string name, Func2<Vector3, Vector3, Quaternion> testFunc, Func2<UE.Vector3, UE.Vector3, UE.Quaternion> refFunc,
            Number minValue, Number maxValue, DevMode devMode, Number maxDev)
        {
            TestFunc2<Vector3, Vector3> test = (Vector3 x, Vector3 y) =>
            {
                try
                {
                    Quaternion result = testFunc(x, y);
                    UE.Quaternion refResult = refFunc((UE.Vector3)x, (UE.Vector3)y);
                    return CheckResult(name, x, y, (UE.Quaternion)result, refResult, minValue, maxValue, devMode, (float)maxDev);
                }
                catch (Exception ex)
                {
                    ExOutput(name, ex, x, y);
                    return false;
                }
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

        public static bool TestCritical(Tester<Number> tester)
        {
            Logger.Log("IMath test, Test critical:" + tester.name);
            if (!tester.test(tester.minValue))
                return false;
            if (!tester.test(tester.maxValue))
                return false;
            if (tester.minValue < Number.zero && Number.zero < tester.maxValue && !tester.test(Number.zero))
                return false;
            return true;
        }
        public static bool TestCritical(Tester<Number, Number> tester)
        {
            Logger.Log("IMath test, Test critical:" + tester.name);
            for (int i = 0; i < 3; ++i)
            {
                Number x = Number.zero;
                if (i == 0)
                    x = tester.minValue1;
                else if (i == 1)
                {
                    if (tester.minValue1 < Number.zero && Number.zero < tester.maxValue1)
                        x = Number.zero;
                    else
                        continue;
                }
                else if (i == 2)
                    x = tester.maxValue1;
                for (int j = 0; j < 3; ++j)
                {
                    Number y = Number.zero;
                    if (j == 0)
                        y = tester.minValue2;
                    else if (j == 1)
                    {
                        if (tester.minValue2 < Number.zero && Number.zero < tester.maxValue2)
                            y = Number.zero;
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
            bool zeroInRange = tester.minValue < Number.zero && Number.zero < tester.maxValue;
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
                if (!tester.test(new Vector3(Number.zero, tester.minValue, tester.minValue)))
                    return false;
                if (!tester.test(new Vector3(Number.zero, tester.minValue, tester.maxValue)))
                    return false;
                if (!tester.test(new Vector3(Number.zero, tester.maxValue, tester.minValue)))
                    return false;
                if (!tester.test(new Vector3(Number.zero, tester.maxValue, tester.maxValue)))
                    return false;
                if (!tester.test(new Vector3(tester.minValue, Number.zero, tester.minValue)))
                    return false;
                if (!tester.test(new Vector3(tester.minValue, Number.zero, tester.maxValue)))
                    return false;
                if (!tester.test(new Vector3(tester.maxValue, Number.zero, tester.minValue)))
                    return false;
                if (!tester.test(new Vector3(tester.maxValue, Number.zero, tester.maxValue)))
                    return false;
                if (!tester.test(new Vector3(tester.minValue, tester.minValue, Number.zero)))
                    return false;
                if (!tester.test(new Vector3(tester.minValue, tester.maxValue, Number.zero)))
                    return false;
                if (!tester.test(new Vector3(tester.maxValue, tester.minValue, Number.zero)))
                    return false;
                if (!tester.test(new Vector3(tester.maxValue, tester.maxValue, Number.zero)))
                    return false;
                if (!tester.test(new Vector3(tester.minValue, Number.zero, Number.zero)))
                    return false;
                if (!tester.test(new Vector3(tester.maxValue, Number.zero, Number.zero)))
                    return false;
                if (!tester.test(new Vector3(Number.zero, tester.minValue, Number.zero)))
                    return false;
                if (!tester.test(new Vector3(Number.zero, tester.maxValue, Number.zero)))
                    return false;
                if (!tester.test(new Vector3(Number.zero, Number.zero, tester.minValue)))
                    return false;
                if (!tester.test(new Vector3(Number.zero, Number.zero, tester.maxValue)))
                    return false;
                if (!tester.test(new Vector3(Number.zero, Number.zero, Number.zero)))
                    return false;
            }
            return true;
        }
        public static bool TestCritical(Tester<Vector3, Vector3> tester)
        {
            Logger.Log("IMath test, Test critical:" + tester.name);
            if (!tester.test(new Vector3(Number.zero, Number.zero, Number.zero), new Vector3(Number.zero, Number.zero, Number.zero)))
                return false;
            if (!tester.test(new Vector3(Number.zero, Number.zero, Number.zero), new Vector3(tester.minValue2.x, Number.zero, tester.maxValue2.z)))
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

        public static bool interruptTest = false;
        public static bool TestSequence(Tester<Number> tester)
        {
            return TestSequence(tester, tester.minValue, tester.maxValue, Number.one);
        }
        public static bool TestSequence(Tester<Number> tester, Number start, Number end, Number step)
        {
            Logger.Log(string.Format("IMath test, Test sequence:{0} start:{1} end:{2} step:{3}", 
                tester.name, start, end, step));
            bool succ = true;
            for (long i = start.raw; i <= end.raw; i += step.raw)
            {
                if (!tester.test(Number.Raw(i)))
                    succ = false;
                if (interruptTest)
                    return succ;
            }
            return succ;
        }
        public static bool TestSequence(Tester<Number, Number> tester)
        {
            return TestSequence(tester, tester.minValue1, tester.maxValue1, Number.Raw(1), tester.minValue2, tester.maxValue2, Number.Raw(1));
        }
        public static bool TestSequence(Tester<Number, Number> tester, 
            Number start1, Number end1, Number step1, Number start2, Number end2, Number step2)
        {
            Logger.Log(string.Format("IMath test, Test sequence:{0} start:{1} end:{2} step:{3} start:{4} end:{5} step:{6}", 
                tester.name, start1, end1, step1, start2, end2, step2));
            bool succ = true;
            for (long i = start1.raw; i <= end1.raw; i += step1.raw)
            {
                for (long j = start2.raw; j <= end2.raw; j += step2.raw)
                {
                    if (!tester.test(Number.Raw(i), Number.Raw(j)))
                        succ = false;
                    if (interruptTest)
                        return succ;
                }
            }
            return succ;
        }
        public static bool TestSequence(Tester<Vector3> tester)
        {
            Logger.Log(string.Format("IMath test, Test sequence:{0} start:{1} end:{2} step:{3}", 
                tester.name, tester.minValue, tester.maxValue, 1));
            bool succ = true;
            for (long x = tester.minValue.raw; x <= tester.maxValue.raw; x += 1)
            {
                for (long y = tester.minValue.raw; y <= tester.maxValue.raw; y += 1)
                {
                    for (long z = tester.minValue.raw; z <= tester.maxValue.raw; z += 1)
                    {
                        if (!tester.test(new Vector3(Number.Raw(x), Number.Raw(y), Number.Raw(z))))
                            succ = false;
                        if (interruptTest)
                            return succ;
                    }
                }
            }
            return succ;
        }
        public static bool TestSequence(Tester<Number, Vector3> tester)
        {
            Logger.Log(string.Format("IMath test, Test sequence:{0} start:{1} end:{2} step:{3} start:{4} end:{5} step:{6}", 
                tester.name, tester.minValue1, tester.maxValue1, 1, tester.minValue2, tester.maxValue2, 1));
            bool succ = true;
            for (long i = tester.minValue1.raw; i <= tester.maxValue1.raw; ++i)
            {
                for (long x = tester.minValue2.x.raw; x <= tester.maxValue2.x.raw; x += 1)
                {
                    for (long y = tester.minValue2.y.raw; y <= tester.maxValue2.y.raw; y += 1)
                    {
                        for (long z = tester.minValue2.z.raw; z <= tester.maxValue2.z.raw; z += 1)
                        {
                            if (!tester.test(Number.Raw(i), new Vector3(Number.Raw(x), Number.Raw(y), Number.Raw(z))))
                                succ = false;
                            if (interruptTest)
                                return succ;
                        }
                    }
                }
            }
            return succ;
        }
        public static bool TestSequence(Tester<Vector3, Vector3> tester)
        {
            Logger.Log(string.Format("IMath test, Test sequence:{0} start:{1} end:{2} step:{3} start:{4} end:{5} step:{6}", 
                tester.name, tester.minValue1, tester.maxValue1, 1, tester.minValue2, tester.maxValue2, 1));
            bool succ = true;
            for (long x1 = tester.minValue1.x.raw; x1 <= tester.maxValue1.x.raw; x1 += 1)
            {
                for (long y1 = tester.minValue1.y.raw; y1 <= tester.maxValue1.y.raw; y1 += 1)
                {
                    for (long z1 = tester.minValue1.z.raw; z1 <= tester.maxValue1.z.raw; z1 += 1)
                    {
                        for (long x2 = tester.minValue2.x.raw; x2 <= tester.maxValue2.x.raw; x2 += 1)
                        {
                            for (long y2 = tester.minValue2.y.raw; y2 <= tester.maxValue2.y.raw; y2 += 1)
                            {
                                for (long z2 = tester.minValue2.z.raw; z2 <= tester.maxValue2.z.raw; z2 += 1)
                                {
                                    if (!tester.test(new Vector3(Number.Raw(x1), Number.Raw(y1), Number.Raw(z1)), new Vector3(Number.Raw(x2), Number.Raw(y2), Number.Raw(z2))))
                                        succ = false;
                                    if (interruptTest)
                                        return succ;
                                }
                            }
                        }
                    }
                }
            }
            return succ;
        }
        public static bool TestSequence(Tester<Vector3, Vector3, Number> tester)
        {
            Logger.Log(string.Format("IMath test, Test sequence:{0} start:{1} end:{2} step:{3} start:{4} end:{5} step:{6}", 
                tester.name, tester.minValue1, tester.maxValue1, 1, tester.minValue2, tester.maxValue2, 1));
            bool succ = true;
            for (long x1 = tester.minValue1.x.raw; x1 <= tester.maxValue1.x.raw; x1 += 1)
            {
                for (long y1 = tester.minValue1.y.raw; y1 <= tester.maxValue1.y.raw; y1 += 1)
                {
                    for (long z1 = tester.minValue1.z.raw; z1 <= tester.maxValue1.z.raw; z1 += 1)
                    {
                        for (long x2 = tester.minValue2.x.raw; x2 <= tester.maxValue2.x.raw; x2 += 1)
                        {
                            for (long y2 = tester.minValue2.y.raw; y2 <= tester.maxValue2.y.raw; y2 += 1)
                            {
                                for (long z2 = tester.minValue2.z.raw; z2 <= tester.maxValue2.z.raw; z2 += 1)
                                {
                                    for (long n = tester.minValue3.raw; n <= tester.maxValue3.raw; n += 1)
                                    {
                                        Vector3 v1 = new Vector3(Number.Raw(x1), Number.Raw(y1), Number.Raw(z1));
                                        Vector3 v2 = new Vector3(Number.Raw(x2), Number.Raw(y2), Number.Raw(z2));
                                        if (!tester.test(v1, v2, Number.Raw(n)))
                                            succ = false;
                                        if (interruptTest)
                                            return succ;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            return succ;
        }
        public static bool TestRandom(Tester<Number> tester, int count)
        {
            Logger.Log("IMath test, Test random:" + tester.name + " count:" + count);
            for (int i = 0; i < count; ++i)
            {
                if (!tester.test(Number.Raw(UE.Random.Range(tester.minValue.raw, tester.maxValue.raw))))
                    return false;
            }
            return true;
        }
        public static bool TestRandom(Tester<Vector3> tester, int count)
        {
            Logger.Log("IMath test, Test random:" + tester.name + " count:" + count);
            for (int i = 0; i < count; ++i)
            {
                Number x = Number.Raw(UE.Random.Range(tester.minValue.raw, tester.maxValue.raw));
                Number y = Number.Raw(UE.Random.Range(tester.minValue.raw, tester.maxValue.raw));
                Number z = Number.Raw(UE.Random.Range(tester.minValue.raw, tester.maxValue.raw));
                if (!tester.test(new Vector3(x, y, z)))
                    return false;
            }
            return true;
        }
        public static bool TestRandom(Tester<Number, Number> tester, int count)
        {
            Logger.Log("IMath test, Test random:" + tester.name + " count:" + count);
            for (int i = 0; i < count; ++i)
            {
                Number x = Number.Raw(UE.Random.Range(tester.minValue1.raw, tester.maxValue1.raw));
                Number y = Number.Raw(UE.Random.Range(tester.minValue2.raw, tester.maxValue2.raw));
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
                Number x1 = Number.Raw(UE.Random.Range(tester.minValue1.x.raw, tester.maxValue1.x.raw));
                Number y1 = Number.Raw(UE.Random.Range(tester.minValue1.y.raw, tester.maxValue1.y.raw));
                Number z1 = Number.Raw(UE.Random.Range(tester.minValue1.z.raw, tester.maxValue1.z.raw));
                Number x2 = Number.Raw(UE.Random.Range(tester.minValue2.x.raw, tester.maxValue2.x.raw));
                Number y2 = Number.Raw(UE.Random.Range(tester.minValue2.y.raw, tester.maxValue2.y.raw));
                Number z2 = Number.Raw(UE.Random.Range(tester.minValue2.z.raw, tester.maxValue2.z.raw));
                if (!tester.test(new Vector3(x1, y1, z1), new Vector3(x2, y2, z2)))
                    return false;
            }
            return true;
        }
        public static bool TestRandom(Tester<Vector3, Vector3, Number> tester, int count)
        {
            Logger.Log("IMath test, Test random:" + tester.name + " count:" + count);
            for (int i = 0; i < count; ++i)
            {
                Number x1 = Number.Raw(UE.Random.Range(tester.minValue1.x.raw, tester.maxValue1.x.raw));
                Number y1 = Number.Raw(UE.Random.Range(tester.minValue1.y.raw, tester.maxValue1.y.raw));
                Number z1 = Number.Raw(UE.Random.Range(tester.minValue1.z.raw, tester.maxValue1.z.raw));
                Number x2 = Number.Raw(UE.Random.Range(tester.minValue2.x.raw, tester.maxValue2.x.raw));
                Number y2 = Number.Raw(UE.Random.Range(tester.minValue2.y.raw, tester.maxValue2.y.raw));
                Number z2 = Number.Raw(UE.Random.Range(tester.minValue2.z.raw, tester.maxValue2.z.raw));
                Number n = Number.Raw(UE.Random.Range(tester.minValue3.raw, tester.maxValue3.raw));
                if (!tester.test(new Vector3(x1, y1, z1), new Vector3(x2, y2, z2), n))
                    return false;
            }
            return true;
        }
        public static bool TestRandom(Tester<Number, Vector3> tester, int count)
        {
            Logger.Log("IMath test, Test random:" + tester.name + " count:" + count);
            for (int i = 0; i < count; ++i)
            {
                Number input1 = Number.Raw(UE.Random.Range(tester.minValue1.raw, tester.maxValue1.raw));
                Number x2 = Number.Raw(UE.Random.Range(tester.minValue2.x.raw, tester.maxValue2.x.raw));
                Number y2 = Number.Raw(UE.Random.Range(tester.minValue2.y.raw, tester.maxValue2.y.raw));
                Number z2 = Number.Raw(UE.Random.Range(tester.minValue2.z.raw, tester.maxValue2.z.raw));
                if (!tester.test(input1, new Vector3(x2, y2, z2)))
                    return false;
            }
            return true;
        }

        static float CalcDev(float diff, float refResult, Number minValue, Number maxValue, DevMode devMode)
        {
            float dev = 0f;
            if (devMode == DevMode.RefResult)
                dev = (float)diff / refResult;
            else if (devMode == DevMode.ValueRange)
                dev = (float)diff / (maxValue.raw - minValue.raw + 1);
            else if (devMode == DevMode.Absolute)
                dev = (float)diff;
            else if (devMode == DevMode.AbsoluteWrap360)
            {
                dev = System.Math.Abs((float)diff);
                dev = System.Math.Min(dev, 360 - dev);
            }
            return System.Math.Abs(dev);
        }

        static bool CheckResult(string name, Number input, float result, float refResult,
            Number minValue, Number maxValue, DevMode devMode, float maxDev)
        {
            float diff = System.Math.Abs(result - refResult);
            float dev = CalcDev(diff, refResult, minValue, maxValue, devMode);
            if (dev > maxDev)
            {
                Logger.LogError(string.Format(
                    "TestName:{0} Input:{1} Result:{2} RefResult:{3} Diff:{4} Dev:{5} MaxDev:{6} DevMode:{7}",
                    name, input, result, refResult, diff, dev, maxDev, devMode));
                return false;
            }
            return true;
        }
        static bool CheckResult(string name, Number input1, Number input2, float result, float refResult,
            Number minValue, Number maxValue, DevMode devMode, float maxDev)
        {
            float diff = System.Math.Abs(result - refResult);
            float dev = CalcDev(diff, refResult, minValue, maxValue, devMode);
            if (dev > maxDev)
            {
                Logger.LogError(string.Format(
                    "TestName:{0} Input:{1},{2} Result:{3} RefResult:{4} Diff:{5} Dev:{6} MaxDev:{7} DevMode:{8}",
                    name, input1, input2, result, refResult, diff, dev, maxDev, devMode));
                return false;
            }
            return true;
        }
        static bool CheckResult(string name, Vector3 input, UE.Vector3 result, UE.Vector3 refResult,
            Number minValue, Number maxValue, DevMode devMode, float maxDev)
        {
            UE.Vector3 diff = result - refResult;
            float devX = CalcDev(System.Math.Abs(diff.x), refResult.x, minValue, maxValue, devMode);
            float devY = CalcDev(System.Math.Abs(diff.y), refResult.y, minValue, maxValue, devMode);
            float devZ = CalcDev(System.Math.Abs(diff.z), refResult.z, minValue, maxValue, devMode);
            if (devX > maxDev || devY > maxDev || devZ > maxDev)
            {
                Logger.LogError(string.Format(
                    "TestName:{0} Input:{1} Result:{2} RefResult:{3} Diff:{4} Dev:{5} MaxDev:{6} DevMode:{7}",
                    name, input, result.ToString("F4"), refResult.ToString("F4"), 
                    diff.ToString("F4"), new UE.Vector3(devX, devY, devZ).ToString("F4"), maxDev, devMode));
                return false;
            }
            return true;
        }
        static bool CheckResult(string name, Vector3 input, UE.Quaternion result, UE.Quaternion refResult,
            Number minValue, Number maxValue, DevMode devMode, float maxDev)
        {
            float diffX = System.Math.Abs(result.x - refResult.x);
            float diffY = System.Math.Abs(result.y - refResult.y);
            float diffZ = System.Math.Abs(result.z - refResult.z);
            float diffW = System.Math.Abs(result.w - refResult.w);
            float devX = CalcDev(System.Math.Abs(diffX), refResult.x, minValue, maxValue, devMode);
            float devY = CalcDev(System.Math.Abs(diffY), refResult.y, minValue, maxValue, devMode);
            float devZ = CalcDev(System.Math.Abs(diffZ), refResult.z, minValue, maxValue, devMode);
            float devW = CalcDev(System.Math.Abs(diffW), refResult.w, minValue, maxValue, devMode);
            if (devX > maxDev || devY > maxDev || devZ > maxDev)
            {
                Logger.LogError(string.Format(
                    "TestName:{0} Input:{1} Result:{2} RefResult:{3} Diff:{4} Dev:{5} MaxDev:{6} DevMode:{7}",
                    name, input, result.ToString("F4"), refResult.ToString("F4"), 
                    (new UE.Quaternion(diffX, diffY, diffZ, diffW)).ToString("F4"),
                    (new UE.Quaternion(devX, devY, devZ, devW)).ToString("F4"), maxDev, devMode));
                return false;
            }
            return true;
        }
        static bool CheckResult(string name, Number input1, Vector3 input2, UE.Quaternion result, UE.Quaternion refResult,
            Number minValue, Number maxValue, DevMode devMode, float maxDev)
        {
            float diffX = System.Math.Abs(result.x - refResult.x);
            float diffY = System.Math.Abs(result.y - refResult.y);
            float diffZ = System.Math.Abs(result.z - refResult.z);
            float diffW = System.Math.Abs(result.w - refResult.w);
            float devX = CalcDev(System.Math.Abs(diffX), refResult.x, minValue, maxValue, devMode);
            float devY = CalcDev(System.Math.Abs(diffY), refResult.y, minValue, maxValue, devMode);
            float devZ = CalcDev(System.Math.Abs(diffZ), refResult.z, minValue, maxValue, devMode);
            float devW = CalcDev(System.Math.Abs(diffW), refResult.w, minValue, maxValue, devMode);
            if (devX > maxDev || devY > maxDev || devZ > maxDev)
            {
                Logger.LogError(string.Format(
                    "TestName:{0} Input:{1} {2} Result:{3} RefResult:{4} Diff:{5} Dev:{6} MaxDev:{7} DevMode:{8}",
                    name, input1, input2, result.ToString("F4"), refResult.ToString("F4"), 
                    (new UE.Quaternion(diffX, diffY, diffZ, diffW)).ToString("F4"),
                    (new UE.Quaternion(devX, devY, devZ, devW)).ToString("F4"), maxDev, devMode));
                return false;
            }
            return true;
        }
        static bool CheckResult(string name, Vector3 input1, Vector3 input2, UE.Vector3 result, UE.Vector3 refResult,
            Number minValue, Number maxValue, DevMode devMode, float maxDev)
        {
            UE.Vector3 diff = result - refResult;
            float devX = CalcDev(System.Math.Abs(diff.x), refResult.x, minValue, maxValue, devMode);
            float devY = CalcDev(System.Math.Abs(diff.y), refResult.y, minValue, maxValue, devMode);
            float devZ = CalcDev(System.Math.Abs(diff.z), refResult.z, minValue, maxValue, devMode);
            if (devX > maxDev || devY > maxDev || devZ > maxDev)
            {
                Logger.LogError(string.Format(
                    "TestName:{0} Input:{1} {2} Result:{3} RefResult:{4} Diff:{5} Dev:{6} MaxDev:{7} DevMode:{8}",
                    name, input1, input2, result.ToString("F4"), refResult.ToString("F4"),
                    diff.ToString("F4"), new UE.Vector3(devX, devY, devZ).ToString("F4"), maxDev, devMode));
                return false;
            }
            return true;
        }
        static bool CheckResult(string name, Vector3 input1, Vector3 input2, UE.Quaternion result, UE.Quaternion refResult,
            Number minValue, Number maxValue, DevMode devMode, float maxDev)
        {
            float diffX = result.x - refResult.x;
            float diffY = result.y - refResult.y;
            float diffZ = result.z - refResult.z;
            float diffW = result.w - refResult.w;
            float devX = CalcDev(System.Math.Abs(diffX), refResult.x, minValue, maxValue, devMode);
            float devY = CalcDev(System.Math.Abs(diffY), refResult.y, minValue, maxValue, devMode);
            float devZ = CalcDev(System.Math.Abs(diffZ), refResult.z, minValue, maxValue, devMode);
            float devW = CalcDev(System.Math.Abs(diffW), refResult.w, minValue, maxValue, devMode);
            if (devX > maxDev || devY > maxDev || devZ > maxDev || devW > maxDev)
            {
                Logger.LogError(string.Format(
                    "TestName:{0} Input:{1} {2} Result:{3} RefResult:{4} Diff:{5} Dev:{6} MaxDev:{7} DevMode:{8}",
                    name, input1, input2, result.ToString("F4"), refResult.ToString("F4"),
                    new UE.Quaternion(diffX, diffY, diffZ, diffW).ToString("F4"),
                    new UE.Vector3(devX, devY, devZ).ToString("F4"), maxDev, devMode));
                return false;
            }
            return true;
        }
        static bool CheckResult(string name, Vector3 input1, Vector3 input2, float result, float refResult,
            Number minValue, Number maxValue, DevMode devMode, float maxDev)
        {
            float diff = result - refResult;
            float dev = CalcDev(System.Math.Abs(diff), refResult, minValue, maxValue, devMode);
            if (dev > maxDev)
            {
                Logger.LogError(string.Format(
                    "TestName:{0} Input:{1} {2} Result:{3} RefResult:{4} Diff:{5} Dev:{6} MaxDev:{7} DevMode:{8}",
                    name, input1, input2, result, refResult, diff, dev, maxDev, devMode));
                return false;
            }
            return true;
        }
        static bool CheckResult(string name, Vector3 input1, Vector3 input2, Number input3,
            UE.Vector3 result, UE.Vector3 refResult, Number minValue, Number maxValue, DevMode devMode, float maxDev)
        {
            UE.Vector3 diff = result - refResult;
            float devX = CalcDev(System.Math.Abs(diff.x), refResult.x, minValue, maxValue, devMode);
            float devY = CalcDev(System.Math.Abs(diff.y), refResult.y, minValue, maxValue, devMode);
            float devZ = CalcDev(System.Math.Abs(diff.z), refResult.z, minValue, maxValue, devMode);
            if (devX > maxDev || devY > maxDev || devZ > maxDev)
            {
                Logger.LogError(string.Format(
                    "TestName:{0} Input:{1} {2} {3} Result:{4} RefResult:{5} Diff:{6} Dev:{7} MaxDev:{8} DevMode:{9}",
                    name, input1, input2, input3, result.ToString("F4"), refResult.ToString("F4"),
                    diff.ToString("F4"), new UE.Vector3(devX, devY, devZ).ToString("F4"), maxDev, devMode));
                return false;
            }
            return true;
        }

        static void ExOutput<T>(string name, Exception ex, T input)
        {
            Logger.LogError(ex);
            Logger.LogError(string.Format("Test:{0} Input:{1}", name, input));
        }
        static void ExOutput<T, U>(string name, Exception ex, T input1, U input2)
        {
            Logger.LogError(ex);
            Logger.LogError(string.Format("Test:{0} Input:{1}, {2}", name, input1, input2));
        }
        static void ExOutput<T, U, V>(string name, Exception ex, T input1, U input2, V input3)
        {
            Logger.LogError(ex);
            Logger.LogError(string.Format("Test:{0} Input:{1}, {2} {3}", name, input1, input2, input3));
        }
    }
}
