using UnityEngine;
//随机数生成器 author by jackwu 采用google 线性同余生成器算法
namespace IM
{
    public class Random
    {
        private int state;
        static int _seed;
        static int A = 48271;
        static int M = 2147483647;
        static int Q = M / A;
        static int R = M % A;
        static Random random = new Random(System.DateTime.Now.Millisecond);
        public static int Range(long min, long max)
        {
            return (int)random.RandomInt(min, max);
        }
        public static IM.Number Range(IM.Number min, IM.Number max)
        {
            return IM.Number.Raw(Range(min.raw, max.raw));
        }
        public static IM.BigNumber Range(IM.BigNumber min,IM.BigNumber max)
        {
            return IM.BigNumber.Raw(Range(min.raw, max.raw));
        }
        public static IM.Number value
        {
            get
            {
                return Range(IM.Number.zero, IM.Number.one);
            }
        }
        public static IM.BigNumber bigValue
        {
            get
            {
                return Range(IM.BigNumber.zero, IM.BigNumber.one);
            }
        }
        //设置种子，相同种子的随机数生成序列是一样的
        public static int seed
        {
            get { return _seed; }
            set
            {
                _seed = value;
                random = new Random(value);
            }
        }

        public Random(int initVal = 1)
        {
            if (initVal < 0)
                initVal += M;
            state = initVal;
            if (state == 0)
                state = 0;
        }
        public long RandomInt()
        {
            int tempState = A * (state % Q) - R * (state / Q);
            if (tempState > 0)
                state = tempState;
            else
                state = tempState + M;
            return state;
        }
        public long RandomInt(long min, long max)
        {
            long range = max - min;
            if (range <= 0)
                return min;
            return min + RandomInt() % range;
        }

        //public float RandomFloat0_1()
        //{
        //    return (float)RandomInt() / M;
        //}
    }
}
