using System;

namespace IM
{
    public struct BigNumber: IComparable<BigNumber>, IEquatable<BigNumber>, IComparable<int>, IEquatable<int>
    {
        public static BigNumber max = BigNumber.Raw(int.MaxValue);
        public static BigNumber min = BigNumber.Raw(int.MinValue);
        public static BigNumber zero = new BigNumber(0);
        public static BigNumber one = new BigNumber(1);
        public static BigNumber two = new BigNumber(2);
        public static BigNumber half = new BigNumber(0, 500000);
        //public static BigNumber deviation = new BigNumber(0, 001);

        private int _raw;
        public int raw { get { return _raw; } private set { _raw = value; } }

        public BigNumber round { get { return new BigNumber((int)Math.RndDiv(raw, Math.BIG_FACTOR)); } }

        public BigNumber floor { get { return new BigNumber((int)Math.FloorDiv(raw, Math.BIG_FACTOR)); } }

        public BigNumber ceil { get { return new BigNumber((int)Math.CeilDiv(raw, Math.BIG_FACTOR)); } }

        public int roundToInt { get { return (int)round; } }

        public int floorToInt { get { return (int)floor; } }

        public int ceilToInt { get { return (int)ceil; } }

        public BigNumber(int i)
        {
            _raw = 0;
            long x = i * Math.BIG_FACTOR;
            Math.CheckRange(x, (long)int.MinValue, (long)int.MaxValue, "BigNumber.BigNumber(i)");
            raw = (int)x;
        }

        

        public BigNumber(int i, int f)
        {
            _raw = 0;
            while (f >= Math.BIG_FACTOR)
                f = (int)Math.RndDiv((long)f, 10L);
            long x = (Math.Abs((long)i * Math.BIG_FACTOR) + f) * Math.Sign((long)i);
            Math.CheckRange(x, (long)int.MinValue, (long)int.MaxValue, "Number.Number(i, j)");
            raw = (int)x;
        }

#if IM_UNITY_COMPATIBLE
        public BigNumber(float f)
        {
            _raw = UnityEngine.Mathf.RoundToInt(f * Math.BIG_FACTOR);
        }

        public float ToUnity()
        {
            return (float)this;
        }
#endif

#if IM_UNITY_COMPATIBLE2
        public static BigNumber ToIMBigNumber(float f)
        {
             return new BigNumber(UnityEngine.Mathf.RoundToInt(f * Math.BIG_FACTOR));
        }

        public float ToUnity2()
        {
            return (float)this;
        }
#endif


        public static BigNumber Raw(int x)
        {
            BigNumber number = new BigNumber();
            number.raw = x;
            return number;
        }

        public static BigNumber Raw(long x)
        {
            Math.CheckRange(x, (long)int.MinValue, (long)int.MaxValue, "BigNumber.FromRaw(x)");
            return BigNumber.Raw((int)x);
        }

        public static BigNumber operator +(BigNumber lhs, BigNumber rhs)
        {
            return BigNumber.Raw(lhs.raw + rhs.raw);
        }
        public static BigNumber operator +(BigNumber lhs, int rhs)
        {
            return lhs + new BigNumber(rhs);
        }

        public static BigNumber operator +(int lhs, BigNumber rhs)
        {
            return new IM.BigNumber(lhs) + rhs;
        }

        public static BigNumber operator +(BigNumber lhs, uint rhs)
        {
            return lhs + new IM.BigNumber((int)rhs);
        }

        public static BigNumber operator +(uint lhs, BigNumber rhs)
        {
            return new IM.BigNumber((int)lhs) + rhs;
        }

        public static BigNumber operator -(BigNumber lhs, BigNumber rhs)
        {
            return BigNumber.Raw(lhs.raw - rhs.raw);
        }
        public static BigNumber operator -(BigNumber lhs, int rhs)
        {
            return lhs - new BigNumber(rhs);
        }
        public static BigNumber operator -(int lhs, BigNumber rhs)
        {
            return new BigNumber(lhs) - rhs;
        }

        public static BigNumber operator -(BigNumber lhs, uint rhs)
        {
            return lhs - new BigNumber((int)rhs);
        }
        public static BigNumber operator -(uint lhs, BigNumber rhs)
        {
            return new BigNumber((int)lhs) - rhs;
        }

        public static BigNumber operator -(BigNumber x)
        {
            return BigNumber.Raw(-x.raw);
        }

        public static BigNumber operator *(BigNumber lhs, BigNumber rhs)
        {
            return BigNumber.Raw((int)Math.RndDiv((long)lhs.raw * rhs.raw, (long)Math.BIG_FACTOR));
        }
        public static BigNumber operator *(BigNumber lhs, int rhs)
        {
            return lhs * new BigNumber(rhs);
        }

        public static BigNumber operator *(int lhs,BigNumber rhs)
        {
            return new BigNumber(lhs) * rhs;
        }

        public static BigNumber operator *(uint lhs, BigNumber rhs)
        {
            return new BigNumber((int)lhs) * rhs;
        }


        public static BigNumber operator *(BigNumber lhs,uint rhs)
        {
            return lhs * new BigNumber((int)rhs);
        }

        public static BigNumber operator /(BigNumber lhs, BigNumber rhs)
        {
            long x = Math.RndDiv((long)lhs.raw * Math.BIG_FACTOR, (long)rhs.raw);
            Math.CheckRange(x, int.MinValue, int.MaxValue, string.Format("BigNumber:{0}/{1} = {2}", lhs, rhs, x));
            return BigNumber.Raw((int)x);
        }
        public static BigNumber operator /(BigNumber lhs, int rhs)
        {
            return lhs / new BigNumber(rhs);
        }
        public static BigNumber operator /(int lhs, BigNumber rhs)
        {
            return new BigNumber(lhs) / rhs;
        }

        public static BigNumber operator /(uint lhs,BigNumber rhs)
        {
            return new BigNumber((int)lhs) / rhs;
        }

        public static BigNumber operator /(BigNumber lhs,uint rhs)
        {
            return lhs / new BigNumber((int)rhs);
        }

        public static BigNumber operator %(BigNumber lhs, BigNumber rhs)
        {
            return BigNumber.Raw(lhs.raw % rhs.raw);
        }
        public static BigNumber operator %(BigNumber lhs, int rhs)
        {
            return lhs % new BigNumber(rhs);
        }

        public static explicit operator int(BigNumber number)
        {
            return (int)Math.RndDiv(number.raw, Math.BIG_FACTOR);
        }

        public static explicit operator float(BigNumber number)
        {
            return (float)number.raw / Math.BIG_FACTOR;
        }
        
        //BigNumber到Number必要是显示转换：：
        public static explicit operator Number(BigNumber bigNumber)
        {
            return Number.Raw((int)Math.RndDiv((long)bigNumber.raw * Math.FACTOR, Math.BIG_FACTOR));
        }


        public int CompareTo(BigNumber other)
        {
            return raw.CompareTo(other.raw);
        }
        public int CompareTo(int other)
        {
            return CompareTo(new BigNumber(other));
        }

        public static bool operator < (BigNumber lhs, BigNumber rhs)
        {
            return lhs.CompareTo(rhs) < 0;
        }
        public static bool operator <= (BigNumber lhs, BigNumber rhs)
        {
            return lhs.CompareTo(rhs) <= 0;
        }
        public static bool operator > (BigNumber lhs, BigNumber rhs)
        {
            return lhs.CompareTo(rhs) > 0;
        }
        public static bool operator >= (BigNumber lhs, BigNumber rhs)
        {
            return lhs.CompareTo(rhs) >= 0;
        }
        public static bool operator == (BigNumber lhs, BigNumber rhs)
        {
            return lhs.CompareTo(rhs) == 0;
        }
        public static bool operator != (BigNumber lhs, BigNumber rhs)
        {
            return lhs.CompareTo(rhs) != 0;
        }
        public static bool operator < (BigNumber lhs, int rhs)
        {
            return lhs.CompareTo(rhs) < 0;
        }
        public static bool operator < (int lhs, BigNumber rhs)
        {;
            return rhs.CompareTo(lhs) > 0;
        }
        public static bool operator <= (BigNumber lhs, int rhs)
        {
            return lhs.CompareTo(rhs) <= 0;
        }
        public static bool operator <= (int lhs, BigNumber rhs)
        {
            return rhs.CompareTo(lhs) >= 0;
        }
        public static bool operator > (BigNumber lhs, int rhs)
        {
            return lhs.CompareTo(rhs) > 0;
        }
        public static bool operator > (int lhs, BigNumber rhs)
        {
            return rhs.CompareTo(lhs) < 0;
        }
        public static bool operator >= (BigNumber lhs, int rhs)
        {
            return lhs.CompareTo(rhs) >= 0;
        }
        public static bool operator >=(int lhs, BigNumber rhs)
        {
            return rhs.CompareTo(lhs) <= 0;
        }
        public static bool operator == (BigNumber lhs, int rhs)
        {
            return lhs.CompareTo(rhs) == 0;
        }
        public static bool operator != (BigNumber lhs, int rhs)
        {
            return lhs.CompareTo(rhs) != 0;
        }

        public bool Equals(BigNumber other)
        {
            return raw.Equals(other.raw);
        }
        public bool Equals(int other)
        {
            return Equals(new BigNumber(other));
        }
        public override bool Equals(object obj)
        {
            return raw.Equals(obj);
        }
        //public static bool Approximately(BigNumber lhs, BigNumber rhs)
        //{
        //    return Approximately(lhs, rhs, deviation);
        //}
        //public static bool Approximately(BigNumber lhs, BigNumber rhs, BigNumber dev)
        //{
        //    return Math.Abs(lhs - rhs) <= dev;
        //}

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

        public static bool TryParse(string text,out BigNumber result)
        {
            result = BigNumber.zero;
            text = text.Trim();
            BigNumber sign = BigNumber.one;
            if (text.StartsWith("-"))
            {
                sign = -BigNumber.one;
                text = text.Substring(1);
            }
            string[] tokens = text.Split('.');
            if (tokens.Length == 1)
            {
                int i = 0;
                if (int.TryParse(tokens[0], out i))
                {
                    result = new BigNumber(i) * sign;
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
                    result = new BigNumber(i, f) * sign;
                    return true;
                }
            }
            return false;
          
        }
        
        public static BigNumber Parse(string text )
        {
            BigNumber result;
            bool b = TryParse(text, out result);
            if (b)
                return result;
            else
                throw new FormatException("BigNumber.Parse: Illegal format" + text);
        }

   
    }
}
