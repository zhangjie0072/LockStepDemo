using System;

namespace IM
{
    //精确到小数点后面6位
    //只能用于属性计算，不可用于空间几何计算
    public struct PrecNumber: IComparable<PrecNumber>, IEquatable<PrecNumber>, IComparable<int>, IEquatable<int>
    {
        public static PrecNumber max = PrecNumber.Raw(int.MaxValue);
        public static PrecNumber min = PrecNumber.Raw(int.MinValue);
        public static PrecNumber zero = new PrecNumber(0);
        public static PrecNumber one = new PrecNumber(1);
        public static PrecNumber two = new PrecNumber(2);
        public static PrecNumber half = new PrecNumber(0, 500000);

        private int _raw;
        public int raw { get { return _raw; } private set { _raw = value; } }

        public PrecNumber round { get { return new PrecNumber((int)Math.RndDiv(raw, Math.BIG_FACTOR)); } }

        public PrecNumber floor { get { return new PrecNumber((int)Math.FloorDiv(raw, Math.BIG_FACTOR)); } }

        public PrecNumber ceil { get { return new PrecNumber((int)Math.CeilDiv(raw, Math.BIG_FACTOR)); } }

        public int roundToInt { get { return (int)round; } }

        public int floorToInt { get { return (int)floor; } }

        public int ceilToInt { get { return (int)ceil; } }

        public PrecNumber(int i)
        {
            _raw = 0;
            long x = i * Math.BIG_FACTOR;
            Math.CheckRange(x, (long)int.MinValue, (long)int.MaxValue, "PrecNumber.PrecNumber(i)");
            raw = (int)x;
        }

        

        public PrecNumber(int i, int f)
        {
            _raw = 0;
            while (f >= Math.BIG_FACTOR)
                f = (int)Math.RndDiv((long)f, 10L);
            long x = (Math.Abs((long)i * Math.BIG_FACTOR) + f) * Math.Sign((long)i);
            Math.CheckRange(x, (long)int.MinValue, (long)int.MaxValue, "Number.Number(i, j)");
            raw = (int)x;
        }

        public static PrecNumber Raw(int x)
        {
            PrecNumber number = new PrecNumber();
            number.raw = x;
            return number;
        }

        public static PrecNumber Raw(long x)
        {
            Math.CheckRange(x, (long)int.MinValue, (long)int.MaxValue, "PrecNumber.Raw(x)");
            return PrecNumber.Raw((int)x);
        }

        public static PrecNumber operator +(PrecNumber lhs, PrecNumber rhs)
        {
            return PrecNumber.Raw(lhs.raw + rhs.raw);
        }
        public static PrecNumber operator +(PrecNumber lhs, int rhs)
        {
            return lhs + new PrecNumber(rhs);
        }

        public static PrecNumber operator +(int lhs, PrecNumber rhs)
        {
            return new IM.PrecNumber(lhs) + rhs;
        }

        public static PrecNumber operator +(PrecNumber lhs, uint rhs)
        {
            return lhs + new IM.PrecNumber((int)rhs);
        }

        public static PrecNumber operator +(uint lhs, PrecNumber rhs)
        {
            return new IM.PrecNumber((int)lhs) + rhs;
        }

        public static PrecNumber operator -(PrecNumber lhs, PrecNumber rhs)
        {
            return PrecNumber.Raw(lhs.raw - rhs.raw);
        }
        public static PrecNumber operator -(PrecNumber lhs, int rhs)
        {
            return lhs - new PrecNumber(rhs);
        }
        public static PrecNumber operator -(int lhs, PrecNumber rhs)
        {
            return new PrecNumber(lhs) - rhs;
        }

        public static PrecNumber operator -(PrecNumber lhs, uint rhs)
        {
            return lhs - new PrecNumber((int)rhs);
        }
        public static PrecNumber operator -(uint lhs, PrecNumber rhs)
        {
            return new PrecNumber((int)lhs) - rhs;
        }

        public static PrecNumber operator -(PrecNumber x)
        {
            return PrecNumber.Raw(-x.raw);
        }

        public static PrecNumber operator *(PrecNumber lhs, PrecNumber rhs)
        {
            return PrecNumber.Raw((int)Math.RndDiv((long)lhs.raw * rhs.raw, (long)Math.BIG_FACTOR));
        }
        public static PrecNumber operator *(PrecNumber lhs, int rhs)
        {
            return lhs * new PrecNumber(rhs);
        }

        public static PrecNumber operator *(int lhs,PrecNumber rhs)
        {
            return new PrecNumber(lhs) * rhs;
        }

        public static PrecNumber operator *(uint lhs, PrecNumber rhs)
        {
            return new PrecNumber((int)lhs) * rhs;
        }


        public static PrecNumber operator *(PrecNumber lhs,uint rhs)
        {
            return lhs * new PrecNumber((int)rhs);
        }

        public static PrecNumber operator /(PrecNumber lhs, PrecNumber rhs)
        {
            long x = Math.RndDiv((long)lhs.raw * Math.BIG_FACTOR, (long)rhs.raw);
            Math.CheckRange(x, int.MinValue, int.MaxValue, string.Format("PrecNumber:{0}/{1} = {2}", lhs, rhs, x));
            return PrecNumber.Raw((int)x);
        }
        public static PrecNumber operator /(PrecNumber lhs, int rhs)
        {
            return lhs / new PrecNumber(rhs);
        }
        public static PrecNumber operator /(int lhs, PrecNumber rhs)
        {
            return new PrecNumber(lhs) / rhs;
        }

        public static PrecNumber operator /(uint lhs,PrecNumber rhs)
        {
            return new PrecNumber((int)lhs) / rhs;
        }

        public static PrecNumber operator /(PrecNumber lhs,uint rhs)
        {
            return lhs / new PrecNumber((int)rhs);
        }

        public static PrecNumber operator %(PrecNumber lhs, PrecNumber rhs)
        {
            return PrecNumber.Raw(lhs.raw % rhs.raw);
        }
        public static PrecNumber operator %(PrecNumber lhs, int rhs)
        {
            return lhs % new PrecNumber(rhs);
        }

        public static explicit operator int(PrecNumber number)
        {
            return (int)Math.RndDiv(number.raw, Math.BIG_FACTOR);
        }

        public static explicit operator float(PrecNumber number)
        {
            return (float)number.raw / Math.BIG_FACTOR;
        }
        
        //PrecNumber到Number必要是显示转换：：
        public static explicit operator Number(PrecNumber bigNumber)
        {
            return Number.Raw((int)Math.RndDiv((long)bigNumber.raw * Math.FACTOR, Math.BIG_FACTOR));
        }


        public int CompareTo(PrecNumber other)
        {
            return raw.CompareTo(other.raw);
        }
        public int CompareTo(int other)
        {
            return CompareTo(new PrecNumber(other));
        }

        public static bool operator < (PrecNumber lhs, PrecNumber rhs)
        {
            return lhs.CompareTo(rhs) < 0;
        }
        public static bool operator <= (PrecNumber lhs, PrecNumber rhs)
        {
            return lhs.CompareTo(rhs) <= 0;
        }
        public static bool operator > (PrecNumber lhs, PrecNumber rhs)
        {
            return lhs.CompareTo(rhs) > 0;
        }
        public static bool operator >= (PrecNumber lhs, PrecNumber rhs)
        {
            return lhs.CompareTo(rhs) >= 0;
        }
        public static bool operator == (PrecNumber lhs, PrecNumber rhs)
        {
            return lhs.CompareTo(rhs) == 0;
        }
        public static bool operator != (PrecNumber lhs, PrecNumber rhs)
        {
            return lhs.CompareTo(rhs) != 0;
        }
        public static bool operator < (PrecNumber lhs, int rhs)
        {
            return lhs.CompareTo(rhs) < 0;
        }
        public static bool operator < (int lhs, PrecNumber rhs)
        {;
            return rhs.CompareTo(lhs) > 0;
        }
        public static bool operator <= (PrecNumber lhs, int rhs)
        {
            return lhs.CompareTo(rhs) <= 0;
        }
        public static bool operator <= (int lhs, PrecNumber rhs)
        {
            return rhs.CompareTo(lhs) >= 0;
        }
        public static bool operator > (PrecNumber lhs, int rhs)
        {
            return lhs.CompareTo(rhs) > 0;
        }
        public static bool operator > (int lhs, PrecNumber rhs)
        {
            return rhs.CompareTo(lhs) < 0;
        }
        public static bool operator >= (PrecNumber lhs, int rhs)
        {
            return lhs.CompareTo(rhs) >= 0;
        }
        public static bool operator >=(int lhs, PrecNumber rhs)
        {
            return rhs.CompareTo(lhs) <= 0;
        }
        public static bool operator == (PrecNumber lhs, int rhs)
        {
            return lhs.CompareTo(rhs) == 0;
        }
        public static bool operator != (PrecNumber lhs, int rhs)
        {
            return lhs.CompareTo(rhs) != 0;
        }

        public bool Equals(PrecNumber other)
        {
            return raw.Equals(other.raw);
        }
        public bool Equals(int other)
        {
            return Equals(new PrecNumber(other));
        }
        public override bool Equals(object obj)
        {
            return raw.Equals(obj);
        }

        public override string ToString()
        {
            return string.Format("{0}{1}.{2:D6}", 
                (Math.Sign(raw) == -1 ? "-" : ""),
                Math.Abs(raw / Math.BIG_FACTOR),
                Math.Abs(raw % Math.BIG_FACTOR));
        }
        public override int GetHashCode()
        {
            return raw.GetHashCode();
        }

        public static bool TryParse(string text,out PrecNumber result)
        {
            result = PrecNumber.zero;
            text = text.Trim();
            PrecNumber sign = PrecNumber.one;
            if (text.StartsWith("-"))
            {
                sign = -PrecNumber.one;
                text = text.Substring(1);
            }
            string[] tokens = text.Split('.');
            if (tokens.Length == 1)
            {
                int i = 0;
                if (int.TryParse(tokens[0], out i))
                {
                    result = new PrecNumber(i) * sign;
                    return true;
                }
                    
            }
            else if (tokens.Length == 2)
            {
                string f_part = tokens[1];
                f_part = f_part.PadRight(7, '0');
                char[] chars = f_part.ToCharArray();

                int i = 0;
                int f1 = 0, f2 = 0, f3 = 0, f4 = 0,f5 = 0,f6 = 0,f7 = 0;
                if (int.TryParse(tokens[0], out i) &&
                    int.TryParse(chars[0].ToString(), out f1) &&
                    int.TryParse(chars[1].ToString(), out f2) &&
                    int.TryParse(chars[2].ToString(), out f3) &&
                    int.TryParse(chars[3].ToString(), out f4) &&
                    int.TryParse(chars[4].ToString(), out f5) &&
                    int.TryParse(chars[5].ToString(), out f6) && 
                    int.TryParse(chars[6].ToString(), out f7)  )
                {
                    int f = f1 * 100000 + f2 * 10000 + f3 * 1000 + f4 * 100 + f5 * 10 + f6;
                    if (f7 >= 5)
                        ++f;
                    result = new PrecNumber(i, f) * sign;
                    return true;
                }
            }
            return false;
          
        }
        
        public static PrecNumber Parse(string text )
        {
            PrecNumber result;
            bool b = TryParse(text, out result);
            if (b)
                return result;
            else
                throw new FormatException("PrecNumber.Parse: Illegal format" + text);
        }

   
    }
}
