namespace IM
{
    public struct Vector3
    {
        public int x, y, z;

        public static Vector3 zero = new Vector3(0, 0, 0);
        public static Vector3 one = new Vector3(Math.FACTOR, Math.FACTOR, Math.FACTOR);
        public static Vector3 up = new Vector3(0, Math.FACTOR, 0);
        public static Vector3 down = new Vector3(0, -Math.FACTOR, 0);
        public static Vector3 left = new Vector3(-Math.FACTOR, 0, 0);
        public static Vector3 right = new Vector3(Math.FACTOR, 0, 0);
        public static Vector3 forward = new Vector3(0, 0, Math.FACTOR);
        public static Vector3 back = new Vector3(0, 0, -Math.FACTOR);

        public int magnitude
        {
            get
            {
                int dot = Dot(this, this);
                int sqrt = Math.Sqrt(dot);
                return sqrt;
            }
        }

        public int sqrMagnitude
        {
            get
            {
                return Dot(this, this);
            }
        }

        public Vector3 normailzed
        {
            get
            {
                int mag = magnitude;
                if (mag == 0)
                    return this;
                long xTmp = Math.RndDiv((long)x * Math.FACTOR, mag);
                long yTmp = Math.RndDiv((long)y * Math.FACTOR, mag);
                long zTmp = Math.RndDiv((long)z * Math.FACTOR, mag);
                return new Vector3((int)xTmp, (int)yTmp, (int)zTmp);
            }
        }

        public Vector3(int x)
        {
            Math.CheckRange(x, "x");

            this.x = x;
            this.y = x;
            this.z = x;
        }
        public Vector3(int x, int y)
        {
            Math.CheckRange(x, "x");
            Math.CheckRange(y, "y");

            this.x = x;
            this.y = y;
            this.z = 0;
        }
        public Vector3(int x, int y, int z)
        {
            Math.CheckRange(x, "x");
            Math.CheckRange(y, "y");
            Math.CheckRange(z, "z");

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

        public static Vector3 operator *(Vector3 lhs, int factor)
        {
            return new Vector3(lhs.x * factor, lhs.y * factor, lhs.z * factor);
        }

        public static Vector3 operator / (Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(Math.RndDiv(lhs.x, rhs.x), Math.RndDiv(lhs.y, rhs.y), Math.RndDiv(lhs.z, rhs.z));
        }

        public static Vector3 operator / (Vector3 lhs, int factor)
        {
            return new Vector3(Math.RndDiv(lhs.x, factor), Math.RndDiv(lhs.y, factor), Math.RndDiv(lhs.z, factor));
        }

        public static explicit operator UnityEngine.Vector3 (Vector3 lhs)
        {
            return new UnityEngine.Vector3((float)lhs.x / Math.FACTOR, (float)lhs.y / Math.FACTOR, (float)lhs.z / Math.FACTOR);
        }

        public void Normalize()
        {
            this = normailzed;
        }

        public static int Dot(Vector3 lhs, Vector3 rhs)
        {
            int xTmp = lhs.x * rhs.x;
            int yTmp = lhs.y * rhs.y;
            int zTmp = lhs.z * rhs.z;
            return xTmp + yTmp + zTmp;
        }

        static long dot_long(Vector3 lhs, Vector3 rhs)
        {
            long xTmp = (long)lhs.x * (long)rhs.x;
            long yTmp = (long)lhs.y * (long)rhs.y;
            long zTmp = (long)lhs.z * (long)rhs.z;
            //return (int)(Math.RndDiv(xTmp + yTmp + zTmp, Math.FACTOR));
            return (long)(xTmp + yTmp + zTmp);
        }

        public static Vector3 Cross(Vector3 lhs, Vector3 rhs)
        {
            //return new Vector3(
            //    (int)((long)lhs.y * (long)rhs.z - (long)rhs.y * (long)lhs.z),
            //    (int)((long)lhs.z * (long)rhs.x - (long)rhs.z * (long)lhs.x),
            //    (int)((long)lhs.x * (long)rhs.y - (long)rhs.x * (long)lhs.y));
            //return new Vector3(
            //    Math.RndDiv((lhs.y * rhs.z - rhs.y * lhs.z), Math.FACTOR),
            //    Math.RndDiv((lhs.z * rhs.x - rhs.z * lhs.x), Math.FACTOR),
            //    Math.RndDiv((lhs.x * rhs.y - rhs.x * lhs.y), Math.FACTOR));
            int x = lhs.y * rhs.z - rhs.y * lhs.z;
            int y = lhs.z * rhs.x - rhs.z * lhs.x;
            int z = lhs.x * rhs.y - rhs.x * lhs.y;          //2 power of factor
            long sqrMag = (long)(x) * x + (long)(y) * y + (long)(z) * z;        //4 power of factor
            if (sqrMag > (long)int.MaxValue)
            {
                long sqrMag1 = Math.RndDiv(sqrMag, Math.SQR_FACTOR); //2 power of factor
                if (sqrMag1 > (long)int.MaxValue)
                {
                    int sqrMag2 = (int)Math.RndDiv(sqrMag1, Math.SQR_FACTOR);   // 0 power of factor
                    int mag = Math.Sqrt(sqrMag2);       //0 power of factor
                    x = Math.RndDiv(x, mag * Math.FACTOR);
                    y = Math.RndDiv(y, mag * Math.FACTOR);
                    z = Math.RndDiv(z, mag * Math.FACTOR);
                }
                else
                {
                    int mag = Math.Sqrt((int)sqrMag1);       //1 power of factor
                    x = Math.RndDiv(x, mag);
                    y = Math.RndDiv(y, mag);
                    z = Math.RndDiv(z, mag);
                }
            }
            else
            {
                int mag = Math.Sqrt((int)sqrMag);    //2 power of factor
                x = (int)(Math.RndDiv((long)x * Math.FACTOR, mag));     //1 power of factor
                y = (int)(Math.RndDiv((long)y * Math.FACTOR, mag));     //1 power of factor
                z = (int)(Math.RndDiv((long)z * Math.FACTOR, mag));     //1 power of factor
            }
            return new Vector3(x, y, z);
        }

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
            return new Vector3(x, y, z);
        }
    }
}
