namespace IM
{
    public struct Vector3
    {
        public Number x, y, z;

        public static Vector3 zero = new Vector3(Number.zero, Number.zero, Number.zero);
        public static Vector3 one = new Vector3(Number.one, Number.one, Number.one);
        public static Vector3 up = new Vector3(Number.zero, Number.one, Number.zero);
        public static Vector3 down = new Vector3(Number.zero, -Number.one, Number.zero);
        public static Vector3 left = new Vector3(-Number.one, Number.zero, Number.zero);
        public static Vector3 right = new Vector3(Number.one, Number.zero, Number.zero);
        public static Vector3 forward = new Vector3(Number.zero, Number.zero, Number.one);
        public static Vector3 back = new Vector3(Number.zero, Number.zero, -Number.one);

        public Number magnitude
        {
            get
            {
                Math.CheckRange(this);

                int dot = Dot(this, this);
                int sqrt = Math.Sqrt(dot);
                return Number.Raw(sqrt);
            }
        }

        public int sqrMagnitude
        {
            get
            {
                Math.CheckRange(this);

                return Dot(this, this);
            }
        }

        public Vector3 normalized
        {
            get
            {
                Math.CheckRange(this);

                Number mag = magnitude;
                if (mag == Number.zero)
                    return this;
                return this / mag;
            }
        }

        public Vector3(Number x)
        {
            this.x = x;
            this.y = x;
            this.z = x;
        }
        public Vector3(Number x, Number y)
        {
            this.x = x;
            this.y = y;
            this.z = Number.zero;
        }
        public Vector3(Number x, Number y, Number z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3 operator + (Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }

        public static Vector3 operator - (Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }

        public static Vector3 operator * (Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
        }

        public static Vector3 operator *(Vector3 lhs, Number rhs)
        {
            return new Vector3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
        }

        public static Vector3 operator / (Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);
        }

        public static Vector3 operator / (Vector3 lhs, Number rhs)
        {
            return new Vector3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
        }

        public static explicit operator UnityEngine.Vector3 (Vector3 lhs)
        {
            return new UnityEngine.Vector3((float)lhs.x, (float)lhs.y, (float)lhs.z);
        }

        public void Normalize()
        {
            this = normalized;
        }

        public static int Dot(Vector3 lhs, Vector3 rhs)
        {
            Math.CheckRange(lhs);
            Math.CheckRange(rhs);

            int xTmp = lhs.x.raw * rhs.x.raw;
            int yTmp = lhs.y.raw * rhs.y.raw;
            int zTmp = lhs.z.raw * rhs.z.raw;
            return xTmp + yTmp + zTmp;
        }

        public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
        {
            Math.CheckRange(lhs);
            Math.CheckRange(rhs);

            int x = lhs.y.raw * rhs.z.raw - rhs.y.raw * lhs.z.raw;
            int y = lhs.z.raw * rhs.x.raw - rhs.z.raw * lhs.x.raw;
            int z = lhs.x.raw * rhs.y.raw - rhs.x.raw * lhs.y.raw;          //2 power of factor
            return new Vector3(
                Number.Raw(Math.RndDiv(x, Math.FACTOR)),
                Number.Raw(Math.RndDiv(y, Math.FACTOR)),
                Number.Raw(Math.RndDiv(z, Math.FACTOR)));
        }

        public static Vector3 CrossAndNormalize(Vector3 lhs, Vector3 rhs)
        {
            Math.CheckRange(lhs);
            Math.CheckRange(rhs);

            int x = lhs.y.raw * rhs.z.raw - rhs.y.raw * lhs.z.raw;
            int y = lhs.z.raw * rhs.x.raw - rhs.z.raw * lhs.x.raw;
            int z = lhs.x.raw * rhs.y.raw - rhs.x.raw * lhs.y.raw;          //2 power of factor
            long sqrMag = (long)(x) * x + (long)(y) * y + (long)(z) * z;        //4 power of factor
            if (sqrMag > (long)int.MaxValue)
            {
                long sqrMag1 = Math.RndDiv(sqrMag, Math.SQR_FACTOR); //2 power of factor
                if (sqrMag1 > (long)int.MaxValue)
                {
                    int sqrMag2 = (int)Math.RndDiv(sqrMag1, Math.SQR_FACTOR);   // 0 power of factor
                    int mag = Math.Sqrt(sqrMag2);       //0 power of factor
                    x = (int)Math.RndDiv(x, mag * Math.FACTOR);
                    y = (int)Math.RndDiv(y, mag * Math.FACTOR);
                    z = (int)Math.RndDiv(z, mag * Math.FACTOR);
                }
                else
                {
                    int mag = Math.Sqrt((int)sqrMag1);       //1 power of factor
                    x = (int)Math.RndDiv(x, mag);
                    y = (int)Math.RndDiv(y, mag);
                    z = (int)Math.RndDiv(z, mag);
                }
            }
            else
            {
                int mag = Math.Sqrt((int)sqrMag);    //2 power of factor
                x = (int)(Math.RndDiv((long)x * Math.FACTOR, mag));     //1 power of factor
                y = (int)(Math.RndDiv((long)y * Math.FACTOR, mag));     //1 power of factor
                z = (int)(Math.RndDiv((long)z * Math.FACTOR, mag));     //1 power of factor
            }
            return new Vector3(Number.Raw(x), Number.Raw(y), Number.Raw(z));
        }

        /*
        public static int Angle(Vector3 lhs, Vector3 rhs)
        {
            int dot = Dot(lhs, rhs);    //2 POF
            int dot1 = Dot(lhs, rhs);   //2 POF
            int dot2 = Dot(lhs, rhs);   //2 POF
            long dot1xdot2 = (long)dot1 * dot2; //4 POF
            int cos = 0;
            if (dot1xdot2 > (long)int.MaxValue)
            {
                dot1xdot2 = Math.RndDiv(dot1xdot2, Math.SQR_FACTOR);    //2 POF
                if (dot1xdot2 > (long)int.MaxValue)
                {
                    dot1xdot2 = Math.RndDiv(dot1xdot2, Math.SQR_FACTOR);    //0 POF
                    int d1xd2 = (int)dot1xdot2;
                    int sqrtd1xd2 = Math.Sqrt(d1xd2);   //0 POF
                    cos = Math.RndDiv(dot, sqrtd1xd2);  //2 POF
                    cos = Math.RndDiv(cos, Math.FACTOR);    //1 POF
                }
                else
                {
                    int d1xd2 = (int)dot1xdot2;     //2 POF
                    int sqrtd1xd2 = Math.Sqrt(d1xd2);   //1 POF
                    cos = Math.RndDiv(dot, sqrtd1xd2);  //1 POF
                }
            }
            else
            {
                int d1xd2 = (int)dot1xdot2;
                int sqrtd1xd2 = Math.Sqrt(d1xd2);   //2 POF
                cos = (int)Math.RndDiv((long)dot * Math.FACTOR, sqrtd1xd2);  //1 POF
            }
            int radians = Math.Acos(Math.Clamp(cos, -Math.FACTOR, Math.FACTOR));
            return Math.Rad2Deg(radians);
        }
        //*/

        //*
        public static Number Angle(Vector3 lhs, Vector3 rhs)
        {
            Math.CheckRange(lhs);
            Math.CheckRange(rhs);

            lhs.Normalize();
            rhs.Normalize();
            Number radians = Math.Acos(Number.Raw(Math.Clamp(Math.RndDiv(Dot(lhs, rhs), Math.FACTOR), -Math.FACTOR, Math.FACTOR)));
            return Math.Rad2Deg(radians);
        }
        //*/

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", x, y, z);
        }

        public static Vector3 Parse(string text)
        {
            if (text[0] != '(' || text[text.Length - 1] != ')')
                throw new System.FormatException("The input text must starts with '(', and ends with ')'.");
            string[] tokens = text.Substring(1, text.Length - 2).Split(',');
            if (tokens.Length != 3)
                throw new System.FormatException("The input text must contains 3 elements.");
            int x, y, z;
            x = y = z = 0;
            if (!int.TryParse(tokens[0], out x))
                throw new System.FormatException("The element 1 is not a integer.");
            if (!int.TryParse(tokens[1], out y))
                throw new System.FormatException("The element 2 is not a integer.");
            if (!int.TryParse(tokens[2], out z))
                throw new System.FormatException("The element 3 is not a integer.");
            return new Vector3(Number.Raw(x), Number.Raw(y), Number.Raw(z));
        }
    }
}
