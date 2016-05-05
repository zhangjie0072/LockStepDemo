using UE = UnityEngine;
namespace IM
{
    public struct Quaternion
    {
        public Number x, y, z, w;

        public static Quaternion identity = new Quaternion(Number.zero, Number.zero, Number.zero, Number.one);

        public Number pitch
        {
            get
            {
                //*
                int Y = (y.raw * z.raw + w.raw * x.raw) * 2;    // 2 POF
                int X = w.raw * w.raw - x.raw * x.raw - y.raw * y.raw + z.raw * z.raw;  // 2 POF
                Number pitch = Math.Atan2(Number.Raw(Y), Number.Raw(X));
                if (pitch < Number.zero)
                    pitch += Math.TWO_PI;
                Number deg = Math.Rad2Deg(pitch);
                Math.CheckRange(deg, Number.zero, new Number(360), "pitch");
                return deg;
                //return pitch;
                //*/
                /*
                UnityEngine.Quaternion q = (UnityEngine.Quaternion)this;
                float Y = (q.y * q.z + q.w * q.x) * 2;
                float X = (q.w * q.w - q.x * q.x - q.y * q.y + q.z * q.z);
                //float X = 1 - 2 * (q.x * q.x + q.y * q.y);
                float pitch = UnityEngine.Mathf.Atan2(Y, X);
                if (pitch < 0)
                    pitch += UnityEngine.Mathf.PI * 2;
                float deg = pitch * UnityEngine.Mathf.Rad2Deg;
                return (int)(deg * Math.FACTOR);
                */
            }
        }

        public Number yaw
        {
            get
            {
                long Y = (x.raw * z.raw - w.raw * y.raw) * -2;   // 2 POF
                Y = Math.RndDiv(Y, Math.FACTOR);
                Number yaw = Math.Asin(Number.Raw((int)Y));
                if (yaw < Number.zero)
                    yaw += Math.TWO_PI;
                Number deg = Math.Rad2Deg(yaw);
                Math.CheckRange(deg, Number.zero, new Number(360), "yaw");
                return deg;
                //return yaw;
            }
        }

        public Number roll
        {
            get
            {
                int Y = (x.raw * y.raw + w.raw * z.raw) * 2;    // 2 POF
                int X = w.raw * w.raw + x.raw * x.raw - y.raw * y.raw - z.raw * z.raw;  // 2 POF
                Number roll = Math.Atan2(Number.Raw(Y), Number.Raw(X));
                if (roll < Number.zero)
                    roll += Math.TWO_PI;
                Number deg = Math.Rad2Deg(roll);
                Math.CheckRange(deg, Number.zero, new Number(360), "roll");
                return deg;
                //return roll;
            }
        }

        public Vector3 eulerAngles
        {
            get
            {
                //No value range check for a vector that represent euler angles
                Vector3 vec = new Vector3();
                vec.x = pitch;
                vec.y = yaw;
                vec.z = roll;
                return vec;
            }
        }

        public int sqrMagnitude
        {
            get
            {
                return x.raw * x.raw + y.raw * y.raw + z.raw * z.raw + w.raw * w.raw;
            }
        }

        public Number magnitude
        {
            get
            {
                return Number.Raw(Math.Sqrt(sqrMagnitude));
            }
        }

        public Quaternion normalized
        {
            get
            {
                int sqrMag = sqrMagnitude;
                if (sqrMag <= 0)
                    return Quaternion.identity;
                return new Quaternion(
                    Number.Raw(Math.Sqrt((int)Math.RndDiv((long)x.raw * x.raw * Math.SQR_FACTOR, sqrMag))) * Math.Sign(x),
                    Number.Raw(Math.Sqrt((int)Math.RndDiv((long)y.raw * y.raw * Math.SQR_FACTOR, sqrMag))) * Math.Sign(y),
                    Number.Raw(Math.Sqrt((int)Math.RndDiv((long)z.raw * z.raw * Math.SQR_FACTOR, sqrMag))) * Math.Sign(z),
                    Number.Raw(Math.Sqrt((int)Math.RndDiv((long)w.raw * w.raw * Math.SQR_FACTOR, sqrMag))) * Math.Sign(w));
            }
        }

        public Quaternion(Number x, Number y, Number z, Number w)
        {
            Math.CheckLengthRange(x, "Quaternion.x");
            Math.CheckLengthRange(y, "Quaternion.y");
            Math.CheckLengthRange(z, "Quaternion.z");
            Math.CheckLengthRange(w, "Quaternion.w");

            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static Quaternion operator * (Quaternion lhs, Number rhs)
        {
            Math.CheckLengthRange(rhs);

            return new Quaternion(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs);
        }
        public static Vector3 operator * (Quaternion lhs, Vector3 rhs)
        {
            Math.CheckRange(rhs);

            Vector3 vec = new Vector3(lhs.x, lhs.y, lhs.z);
            Vector3 uv = Vector3.Cross(vec, rhs);
            Vector3 uuv = Vector3.Cross(vec, uv);

            Vector3 uvw = uv * lhs.w;
            Vector3 result = rhs + (uvw + uuv) * new Number(2);
            return result;
        }
        public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
        {
            long w = lhs.w.raw * rhs.w.raw - lhs.x.raw * rhs.x.raw - lhs.y.raw * rhs.y.raw - lhs.z.raw * rhs.z.raw;
            long x = lhs.w.raw * rhs.x.raw + lhs.x.raw * rhs.w.raw + lhs.y.raw * rhs.z.raw - lhs.z.raw * rhs.y.raw;
            long y = lhs.w.raw * rhs.y.raw + lhs.y.raw * rhs.w.raw + lhs.z.raw * rhs.x.raw - lhs.x.raw * rhs.z.raw;
            long z = lhs.w.raw * rhs.z.raw + lhs.z.raw * rhs.w.raw + lhs.x.raw * rhs.y.raw - lhs.y.raw * rhs.x.raw;
            w = Math.RndDiv(w, Math.FACTOR);
            x = Math.RndDiv(x, Math.FACTOR);
            y = Math.RndDiv(y, Math.FACTOR);
            z = Math.RndDiv(z, Math.FACTOR);
            return new Quaternion(Number.Raw((int)x), Number.Raw((int)y), Number.Raw((int)z), Number.Raw((int)w));
        }

        public static Quaternion operator / (Quaternion lhs, Number rhs)
        {
            return new Quaternion(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs, lhs.w / rhs);
        }

        public static explicit operator UnityEngine.Quaternion(Quaternion lhs)
        {
            return new UnityEngine.Quaternion( (float)lhs.x, (float)lhs.y, (float)lhs.z, (float)lhs.w);
        }

        public static Quaternion Euler(Vector3 euler)
        {
            Math.CheckRange(euler);

            Number eulerX = Math.Deg2Rad(euler.x) / new Number(2);
            Number eulerY = Math.Deg2Rad(euler.y) / new Number(2);
            Number eulerZ = Math.Deg2Rad(euler.z) / new Number(2);
            Vector3 c = new Vector3(Math.Cos(eulerX), Math.Cos(eulerY), Math.Cos(eulerZ));
            Vector3 s = new Vector3(Math.Sin(eulerX), Math.Sin(eulerY), Math.Sin(eulerZ));

            int w = (int)Math.RndDiv((long)c.x.raw * c.y.raw * c.z.raw + (long)s.x.raw * s.y.raw * s.z.raw, Math.SQR_FACTOR);
            int x = (int)Math.RndDiv((long)s.x.raw * c.y.raw * c.z.raw + (long)c.x.raw * s.y.raw * s.z.raw, Math.SQR_FACTOR);
            int y = (int)Math.RndDiv((long)c.x.raw * s.y.raw * c.z.raw - (long)s.x.raw * c.y.raw * s.z.raw, Math.SQR_FACTOR);
            int z = (int)Math.RndDiv((long)c.x.raw * c.y.raw * s.z.raw - (long)s.x.raw * s.y.raw * c.z.raw, Math.SQR_FACTOR);
            return new Quaternion(Number.Raw(x), Number.Raw(y), Number.Raw(z), Number.Raw(w));
        }

        public static UE.Quaternion Eulerf(UE.Vector3 euler)
        {
            UE.Vector3 c = new UE.Vector3(UE.Mathf.Cos(euler.x * 0.5f), UE.Mathf.Cos(euler.y * 0.5f), UE.Mathf.Cos(euler.z * 0.5f));
            UE.Vector3 s = new UE.Vector3(UE.Mathf.Sin(euler.x * 0.5f), UE.Mathf.Sin(euler.y * 0.5f), UE.Mathf.Sin(euler.z * 0.5f));

            float w = c.x * c.y * c.z + s.x * s.y * s.z;
            float x = s.x * c.y * c.z + c.x * s.y * s.z;
            float y = c.x * s.y * c.z - s.x * c.y * s.z;
            float z = c.x * c.y * s.z - s.x * s.y * c.z;
            return new UE.Quaternion(x, y, z, w);
        }

        public static Quaternion AngleAxis(Number angle, Vector3 axis)
        {
            Math.CheckLengthRange(angle);
            Math.CheckRange(axis);

            Number rad = Math.Deg2Rad(angle);
            Number s = Math.Sin(rad / new Number(2));
            Number w = Math.Cos(rad / new Number(2));
            Number x = axis.x * s;
            Number y = axis.y * s;
            Number z = axis.z * s;
            return new Quaternion(x, y, z, w);
        }
    }
}
