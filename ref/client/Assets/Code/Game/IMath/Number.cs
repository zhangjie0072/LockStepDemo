using System;

namespace IM
{
    public struct Number : IComparable<Number>, IEquatable<Number>, IComparable<int>, IEquatable<int>
    {
        public static Number max = Number.Raw(int.MaxValue);
        public static Number min = Number.Raw(int.MinValue);
        public static Number zero = new Number(0);
        public static Number one = new Number(1);
        public static Number two = new Number(2);
        public static Number half = new Number(0, 500);
        public static Number deviation = new Number(0, 001);

        private int _raw;
        public int raw { get { return _raw; } private set { _raw = value; } }

        public Number round { get { return new Number((int)Math.RndDiv(raw, Math.FACTOR)); } }

        public Number floor { get { return new Number((int)Math.FloorDiv(raw, Math.FACTOR)); } }

        public Number ceil { get { return new Number((int)Math.CeilDiv(raw, Math.FACTOR)); } }

        public int roundToInt { get { return (int)round; } }

        public int floorToInt { get { return (int)floor; } }

        public int ceilToInt { get { return (int)ceil; } }


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
            long x = (Math.Abs((long)i * Math.FACTOR) + f) * Math.Sign((long)i);
            Math.CheckRange(x, (long)int.MinValue, (long)int.MaxValue, "Number.Number(i, j)");
            raw = (int)x;
        }

#if IM_UNITY_COMPATIBLE
        public Number(float f)
        {
            _raw = UnityEngine.Mathf.RoundToInt(f * Math.FACTOR);
        }

        public float ToUnity()
        {
            return (float)this;
        }
#endif

#if IM_UNITY_COMPATIBLE2
        public static Number ToIMNumber(float f)
        {
             return Number.Raw(UnityEngine.Mathf.RoundToInt(f * Math.FACTOR));
        }

        public float ToUnity2()
        {
            return (float)this;
        }
#endif


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
        public static Number operator +(Number lhs, int rhs)
        {
            return lhs + new Number(rhs);
        }

        public static Number operator +(int lhs, Number rhs)
        {
            return new IM.Number(lhs) + rhs;
        }

        public static Number operator +(Number lhs, uint rhs)
        {
            return lhs + new IM.Number((int)rhs);
        }

        public static Number operator +(uint lhs, Number rhs)
        {
            return new IM.Number((int)lhs) + rhs;
        }

        public static Number operator -(Number lhs, Number rhs)
        {
            return Number.Raw(lhs.raw - rhs.raw);
        }
        public static Number operator -(Number lhs, int rhs)
        {
            return lhs - new Number(rhs);
        }
        public static Number operator -(int lhs, Number rhs)
        {
            return new Number(lhs) - rhs;
        }

        public static Number operator -(Number lhs, uint rhs)
        {
            return lhs - new Number((int)rhs);
        }
        public static Number operator -(uint lhs, Number rhs)
        {
            return new Number((int)lhs) - rhs;
        }

        public static Number operator -(Number x)
        {
            return Number.Raw(-x.raw);
        }

        public static Number operator *(Number lhs, Number rhs)
        {
            return Number.Raw((int)Math.RndDiv((long)lhs.raw * rhs.raw, (long)Math.FACTOR));
        }
        public static Number operator *(Number lhs, int rhs)
        {
            return lhs * new Number(rhs);
        }

        public static Number operator *(int lhs,Number rhs)
        {
            return new Number(lhs) * rhs;
        }

        public static Number operator *(uint lhs, Number rhs)
        {
            return new Number((int)lhs) * rhs;
        }


        public static Number operator *(Number lhs,uint rhs)
        {
            return lhs * new Number((int)rhs);
        }

        public static Number operator /(Number lhs, Number rhs)
        {
            long x = Math.RndDiv((long)lhs.raw * Math.FACTOR, (long)rhs.raw);
            Math.CheckRange(x, int.MinValue, int.MaxValue, string.Format("Number:{0}/{1} = {2}", lhs, rhs, x));
            return Number.Raw((int)x);
        }
        public static Number operator /(Number lhs, int rhs)
        {
            return lhs / new Number(rhs);
        }
        public static Number operator /(int lhs, Number rhs)
        {
            return new Number(lhs) / rhs;
        }

        public static Number operator /(uint lhs,Number rhs)
        {
            return new Number((int)lhs) / rhs;
        }

        public static Number operator /(Number lhs,uint rhs)
        {
            return lhs / new Number((int)rhs);
        }

        public static Number operator %(Number lhs, Number rhs)
        {
            return Number.Raw(lhs.raw % rhs.raw);
        }
        public static Number operator %(Number lhs, int rhs)
        {
            return lhs % new Number(rhs);
        }

        public static explicit operator int(Number number)
        {
            return (int)Math.RndDiv(number.raw, Math.FACTOR);
        }

        public static explicit operator float(Number number)
        {
            return (float)number.raw / Math.FACTOR;
        }
        //支持Number到BigNumber的隐式转换
        public static implicit operator BigNumber(Number number)
        {
            return BigNumber.Raw((int)Math.RndDiv((long)number.raw * Math.BIG_FACTOR, Math.FACTOR));
        }

        public int CompareTo(Number other)
        {
            return raw.CompareTo(other.raw);
        }
        public int CompareTo(int other)
        {
            return CompareTo(new Number(other));
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
        public static bool operator < (Number lhs, int rhs)
        {
            return lhs.CompareTo(rhs) < 0;
        }
        public static bool operator < (int lhs, Number rhs)
        {;
            return rhs.CompareTo(lhs) > 0;
        }
        public static bool operator <= (Number lhs, int rhs)
        {
            return lhs.CompareTo(rhs) <= 0;
        }
        public static bool operator <= (int lhs, Number rhs)
        {
            return rhs.CompareTo(lhs) >= 0;
        }
        public static bool operator > (Number lhs, int rhs)
        {
            return lhs.CompareTo(rhs) > 0;
        }
        public static bool operator > (int lhs, Number rhs)
        {
            return rhs.CompareTo(lhs) < 0;
        }
        public static bool operator >= (Number lhs, int rhs)
        {
            return lhs.CompareTo(rhs) >= 0;
        }
        public static bool operator >=(int lhs, Number rhs)
        {
            return rhs.CompareTo(lhs) <= 0;
        }
        public static bool operator == (Number lhs, int rhs)
        {
            return lhs.CompareTo(rhs) == 0;
        }
        public static bool operator != (Number lhs, int rhs)
        {
            return lhs.CompareTo(rhs) != 0;
        }

        public bool Equals(Number other)
        {
            return raw.Equals(other.raw);
        }
        public bool Equals(int other)
        {
            return Equals(new Number(other));
        }
        public override bool Equals(object obj)
        {
            return raw.Equals(obj);
        }
        public static bool Approximately(Number lhs, Number rhs)
        {
            return Approximately(lhs, rhs, deviation);
        }
        public static bool Approximately(Number lhs, Number rhs, Number dev)
        {
            return Math.Abs(lhs - rhs) <= dev;
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

        public static bool TryParse(string text,out Number result)
        {
            result = Number.zero;
            text = text.Trim();
            Number sign = Number.one;
            if (text.StartsWith("-"))
            {
                sign = -Number.one;
                text = text.Substring(1);
            }
            string[] tokens = text.Split('.');
            if (tokens.Length == 1)
            {
                int i = 0;
                if (int.TryParse(tokens[0], out i))
                {
                    result = new Number(i) * sign;
                    return true;
                }
                    
            }
            else if (tokens.Length == 2)
            {
                string f_part = tokens[1];
                f_part = f_part.PadRight(4, '0');//第四位用于四舍五入
                char[] chars = f_part.ToCharArray();

                int i = 0;
                int f1 = 0, f2 = 0, f3 = 0, f4 = 0;
                if (int.TryParse(tokens[0], out i) &&
                    int.TryParse(chars[0].ToString(), out f1) &&
                    int.TryParse(chars[1].ToString(), out f2) &&
                    int.TryParse(chars[2].ToString(), out f3) &&
                    int.TryParse(chars[3].ToString(), out f4))
                {
                    int f = f1 * 100 + f2 * 10 + f3;
                    if (f4 >= 5)
                        ++f;
                    result = new Number(i, f) * sign;
                    return true;
                }
            }
            return false;
          
        }
        
        public static Number Parse(string text )
        {
            Number result;
            bool b = TryParse(text, out result);
            if (b)
                return result;
            else
                throw new FormatException("Number.Parse: Illegal format" + text);
        }
    }
}
