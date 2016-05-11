using System;

namespace IM
{
    public struct Number : IComparable<Number>, IEquatable<Number>
    {
        public static Number max = Number.Raw(int.MaxValue);
        public static Number min = Number.Raw(int.MinValue);
        public static Number zero = new Number(0);
        public static Number one = new Number(1);
        public static Number two = new Number(2);
        public static Number half = new Number(0, 500);
        public static Number deviation = Number.one;

        private int _raw;
        public int raw { get { return _raw; } private set { _raw = value; } }

        public Number(int i)
        {
            _raw = 0;
            long x = i * Math.FACTOR;
            Math.CheckRange(x, (long)int.MinValue, (long)int.MaxValue, "Number.Number(i)");
            raw = (int)x;
        }

        public Number(int i, int f)
        {
            _raw = 0;
            while (f >= Math.FACTOR)
                f = (int)Math.RndDiv((long)f, 10L);
            long x = (Math.Abs(i * Math.FACTOR) + f) * Math.Sign(i);
            Math.CheckRange(x, (long)int.MinValue, (long)int.MaxValue, "Number.Number(i, j)");
            raw = (int)x;
        }

        public static Number Raw(int x)
        {
            Number number = new Number();
            number.raw = x;
            return number;
        }

        public static Number Raw(long x)
        {
            Math.CheckRange(x, (long)int.MinValue, (long)int.MaxValue, "Number.FromRaw(x)");
            return Number.Raw((int)x);
        }

        public static Number operator +(Number lhs, Number rhs)
        {
            return Number.Raw(lhs.raw + rhs.raw);
        }

        public static Number operator -(Number lhs, Number rhs)
        {
            return Number.Raw(lhs.raw - rhs.raw);
        }

        public static Number operator -(Number x)
        {
            return Number.Raw(-x.raw);
        }

        public static Number operator *(Number lhs, Number rhs)
        {
            return Number.Raw((int)Math.RndDiv((long)lhs.raw * rhs.raw, (long)Math.FACTOR));
        }

        public static Number operator /(Number lhs, Number rhs)
        {
            long x = Math.RndDiv((long)lhs.raw * Math.FACTOR, (long)rhs.raw);
            Math.CheckRange(x, int.MinValue, int.MaxValue, string.Format("Number:{0}/{1} = {2}", lhs, rhs, x));
            return Number.Raw((int)x);
        }

        public static Number operator %(Number lhs, Number rhs)
        {
            return Number.Raw(lhs.raw % rhs.raw);
        }

        public static explicit operator int(Number number)
        {
            return (int)Math.RndDiv(number.raw, Math.FACTOR);
        }

        public static explicit operator float(Number number)
        {
            return (float)number.raw / Math.FACTOR;
        }

        public int CompareTo(Number other)
        {
            return raw.CompareTo(other.raw);
        }

        public static bool operator < (Number lhs, Number rhs)
        {
            return lhs.CompareTo(rhs) < 0;
        }
        public static bool operator <= (Number lhs, Number rhs)
        {
            return lhs.CompareTo(rhs) <= 0;
        }
        public static bool operator > (Number lhs, Number rhs)
        {
            return lhs.CompareTo(rhs) > 0;
        }
        public static bool operator >= (Number lhs, Number rhs)
        {
            return lhs.CompareTo(rhs) >= 0;
        }
        public static bool operator == (Number lhs, Number rhs)
        {
            return lhs.CompareTo(rhs) == 0;
        }
        public static bool operator != (Number lhs, Number rhs)
        {
            return lhs.CompareTo(rhs) != 0;
        }

        public bool Equals(Number other)
        {
            return raw.Equals(other.raw);
        }
        public override bool Equals(object obj)
        {
            return raw.Equals(obj);
        }
        public static bool Approximately(Number lhs, Number rhs)
        {
            return Math.Abs(lhs - rhs) <= deviation;
        }

        public override string ToString()
        {
            return string.Format("{0}{1}.{2:D3}", 
                (Math.Sign(raw) == -1 ? "-" : ""),
                Math.Abs(raw / Math.FACTOR),
                Math.Abs(raw % Math.FACTOR));
        }
        public override int GetHashCode()
        {
            return raw.GetHashCode();
        }
    }
}
