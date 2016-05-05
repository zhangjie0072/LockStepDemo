using System;

namespace IM.Test
{
    public enum DevMode
    {
        RefResult,
        ValueRange,
        Absolute,
        AbsoluteWrap360,
    }

    public delegate bool TestFunc1<T>(T x);
    public delegate bool TestFunc2<T, U>(T x, U y);
    public delegate bool TestFunc3<T, U, V>(T x, U y, V z);

    public delegate T Func1<T>(T x);
    public delegate U Func1<T, U>(T x);
    public delegate T Func2<T, U>(T x, U y);
    public delegate V Func2<T, U, V>(T x, U y);
    public delegate T Func3<T, U, V>(T x, U y, V z);

    public class Tester<T>
    {
        public string name;
        public Number minValue;
        public Number maxValue;
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

    public class Tester<T, U, V>
    {
        public string name;
        public T minValue1;
        public T maxValue1;
        public U minValue2;
        public U maxValue2;
        public V minValue3;
        public V maxValue3;
        public TestFunc3<T, U, V> test;
    }
}
