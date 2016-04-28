using UE = UnityEngine;
namespace IM
{
    public struct Quaternion
    {
        public int x;
        public int y;
        public int z;
        public int w;

        public static Quaternion identity = new Quaternion(0, 0, 0, Math.FACTOR);

        public int pitch
        {
            get
            {
                //*
                int Y = (y * z + w * x) * 2;
                int X = w * w - x * x - y * y + z * z;
                int pitch = Math.Atan2(Y, X);
                if (pitch < 0)
                    pitch += Math.TWO_PI;
                int deg = Math.Rad2Deg(pitch);
                Math.CheckRange(deg, 0, 360 * Math.FACTOR, "pitch");
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

        public int yaw
        {
            get
            {
                int Y = (x * z - w * y) * -2;
                int yaw = Math.Asin(Math.RndDiv(Y, Math.FACTOR));
                if (yaw < 0)
                    yaw += Math.TWO_PI;
                int deg = Math.Rad2Deg(yaw);
                Math.CheckRange(deg, 0, 360 * Math.FACTOR, "yaw");
                return deg;
                //return yaw;
            }
        }

        public int roll
        {
            get
            {
                int Y = (x * y + w * z) * 2;
                int X = w * w + x * x - y * y - z * z;
                int roll = Math.Atan2(Y, X);
                if (roll < 0)
                    roll += Math.TWO_PI;
                int deg = Math.Rad2Deg(roll);
                Math.CheckRange(deg, 0, 360 * Math.FACTOR, "roll");
                return deg;
                //return roll;
            }
        }

        public Vector3 eulerAngles
        {
            get
            {
                //No value range check for a vector that represent euler angles
                Vector3 vec = new Vector3(0);
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
                return x * x + y * y + z * z + w * w;
            }
        }

        public int magnitude
        {
            get
            {
                return Math.Sqrt(sqrMagnitude);
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
                    Math.Sqrt((int)Math.RndDiv((long)x * x * Math.SQR_FACTOR, sqrMag)) * Math.Sign(x),
                    Math.Sqrt((int)Math.RndDiv((long)y * y * Math.SQR_FACTOR, sqrMag)) * Math.Sign(y),
                    Math.Sqrt((int)Math.RndDiv((long)z * z * Math.SQR_FACTOR, sqrMag)) * Math.Sign(z),
                    Math.Sqrt((int)Math.RndDiv((long)w * w * Math.SQR_FACTOR, sqrMag)) * Math.Sign(w));
            }
        }

        public Quaternion(int x, int y, int z, int w)
        {
            Math.CheckRange(x, "Quaternion.x");
            Math.CheckRange(y, "Quaternion.y");
            Math.CheckRange(z, "Quaternion.z");
            Math.CheckRange(w, "Quaternion.w");

            this.x = x;
            this.y = y;
            this.z = z;
            this.w = w;
        }

        public static Quaternion operator * (Quaternion lhs, int rhs)
        {
            Math.CheckRange(rhs);

            return new Quaternion(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs, lhs.w * rhs);
        }
        public static Vector3 operator * (Quaternion lhs, Vector3 rhs)
        {
            Math.CheckRange(rhs);

            Vector3 vec = new Vector3(lhs.x, lhs.y, lhs.z);
            Vector3 uv = Vector3.Cross(vec, rhs);
            Vector3 uuv = Vector3.Cross(vec, uv);

            Vector3 uvw = uv * lhs.w;
            uvw /= Math.FACTOR;
            Vector3 result = rhs + (uvw + uuv) * 2;
            return result;
        }
        public static Quaternion operator *(Quaternion lhs, Quaternion rhs)
        {
            int w = lhs.w * rhs.w - lhs.x * rhs.x - lhs.y * rhs.y - lhs.z * rhs.z;
            int x = lhs.w * rhs.x + lhs.x * rhs.w + lhs.y * rhs.z - lhs.z * rhs.y;
            int y = lhs.w * rhs.y + lhs.y * rhs.w + lhs.z * rhs.x - lhs.x * rhs.z;
            int z = lhs.w * rhs.z + lhs.z * rhs.w + lhs.x * rhs.y - lhs.y * rhs.x;
            w = Math.RndDiv(w, Math.FACTOR);
            x = Math.RndDiv(x, Math.FACTOR);
            y = Math.RndDiv(y, Math.FACTOR);
            z = Math.RndDiv(z, Math.FACTOR);
            return new Quaternion(x, y, z, w);
        }

        public static Quaternion operator / (Quaternion lhs, int rhs)
        {
            return new Quaternion(Math.RndDiv(lhs.x, rhs), Math.RndDiv(lhs.y, rhs), Math.RndDiv(lhs.z, rhs), Math.RndDiv(lhs.w, rhs));
        }

        public static explicit operator UnityEngine.Quaternion(Quaternion lhs)
        {
            return new UnityEngine.Quaternion(
                (float)lhs.x / Math.FACTOR,
                (float)lhs.y / Math.FACTOR,
                (float)lhs.z / Math.FACTOR,
                (float)lhs.w / Math.FACTOR);
        }

        public static Quaternion Euler(Vector3 euler)
        {
            Math.CheckRange(euler);

            int eulerX = Math.Deg2Rad(euler.x) >> 1;
            int eulerY = Math.Deg2Rad(euler.y) >> 1;
            int eulerZ = Math.Deg2Rad(euler.z) >> 1;
            Vector3 c = new Vector3(Math.Cos(eulerX), Math.Cos(eulerY), Math.Cos(eulerZ));
            Vector3 s = new Vector3(Math.Sin(eulerX), Math.Sin(eulerY), Math.Sin(eulerZ));

            int w = (int)Math.RndDiv((long)c.x * c.y * c.z + (long)s.x * s.y * s.z, Math.SQR_FACTOR);
            int x = (int)Math.RndDiv((long)s.x * c.y * c.z + (long)c.x * s.y * s.z, Math.SQR_FACTOR);
            int y = (int)Math.RndDiv((long)c.x * s.y * c.z - (long)s.x * c.y * s.z, Math.SQR_FACTOR);
            int z = (int)Math.RndDiv((long)c.x * c.y * s.z - (long)s.x * s.y * c.z, Math.SQR_FACTOR);
            return new Quaternion(x, y, z, w);
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

        public static Quaternion AngleAxis(int angle, Vector3 axis)
        {
            Math.CheckRange(angle);
            Math.CheckRange(axis);

            int rad = Math.Deg2Rad(angle);
            int s = Math.Sin(rad >> 1);
            int w = Math.Cos(rad >> 1);
            int x = Math.RndDiv(axis.x * s, Math.FACTOR);
            int y = Math.RndDiv(axis.y * s, Math.FACTOR);
            int z = Math.RndDiv(axis.z * s, Math.FACTOR);
            return new Quaternion(x, y, z, w);
        }
    }
}
