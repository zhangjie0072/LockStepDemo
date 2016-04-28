using System;

namespace IM.Test
{
    public enum DevMode
    {
        Input,
        RefResult,
        ValueRange,
        Absolute,
        AbsoluteWrap360,
    }

    public delegate bool TestFunc1<T>(T x);
    public delegate bool TestFunc2<T, U>(T x, U y);

    public delegate T Func1<T>(T x);
    public delegate U Func1<T, U>(T x);
    public delegate T Func2<T, U>(T x, U y);
    public delegate V Func2<T, U, V>(T x, U y);

    public class Tester<T>
    {
        public string name;
        public int minValue;
        public int maxValue;
        public TestFunc1<T> test;
    }

    public class Tester<T, U>
    {
        public string name;
        public T minValue1;
        public T maxValue1;
        public U minValue2;
        public U maxValue2;
        public TestFunc2<T, U> test;
    }
}
