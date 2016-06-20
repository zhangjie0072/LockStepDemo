using System;
using UE = UnityEngine;

namespace IM
{
    public struct Vector3 : IEquatable<Vector3>
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
        public static Vector3 gravity = new Vector3(Number.zero, -new Number(9, 800), Number.zero);

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

        public Vector2 xz
        {
            get
            {
               return new IM.Vector2(this.x, this.z);
            }
        }

        public Vector2 xy
        {
            get
            {
                return new IM.Vector2(this.x, this.y);
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

#if UNITY_EDITOR
        public static Vector3 FromUnity(UnityEngine.Vector3 vec)
        {
            Number x = Number.FromUnity(vec.x);
            Number y = Number.FromUnity(vec.y);
            Number z = Number.FromUnity(vec.z);
            return new Vector3(x,y,z);
        }
#endif

        public static Vector3 operator + (Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x + rhs.x, lhs.y + rhs.y, lhs.z + rhs.z);
        }

        public static Vector3 operator - (Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x - rhs.x, lhs.y - rhs.y, lhs.z - rhs.z);
        }

        public static Vector3 operator - (Vector3 lhs)
        {
            return new Vector3(-lhs.x, -lhs.y, -lhs.z);
        }

        public static Vector3 operator * (Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x * rhs.x, lhs.y * rhs.y, lhs.z * rhs.z);
        }

        public static Vector3 operator *(Vector3 lhs, Number rhs)
        {
            return new Vector3(lhs.x * rhs, lhs.y * rhs, lhs.z * rhs);
        }
        public static Vector3 operator *(Number lhs, Vector3 rhs)
        {
            return new Vector3(lhs * rhs.x, lhs * rhs.y, lhs * rhs.z);
        }

        public static Vector3 operator / (Vector3 lhs, Vector3 rhs)
        {
            return new Vector3(lhs.x / rhs.x, lhs.y / rhs.y, lhs.z / rhs.z);
        }

        public static Vector3 operator / (Vector3 lhs, Number rhs)
        {
            return new Vector3(lhs.x / rhs, lhs.y / rhs, lhs.z / rhs);
        }

        public static bool operator == (Vector3 lhs, Vector3 rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator != (Vector3 lhs, Vector3 rhs)
        {
            return !lhs.Equals(rhs);
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

        public static Number DotForNumber(Vector3 lhs,Vector3 rhs)
        {
            return Number.Raw(Dot(lhs, rhs) / Math.FACTOR);
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

        //计算从向量from到to的绕Y轴旋转角度。
        //当from和to反向平行时，计算结果可能不同于Unity中使用Quaternion.FromToRotation().eulerAngles.y
        public static Number FromToAngle(Vector3 from, Vector3 to)
        {
            Number angleRad = AngleRad(from, to);
            int y = from.z.raw * to.x.raw - to.z.raw * from.x.raw;
            if (y < 0)
                angleRad = Math.TWO_PI - angleRad;

            return Math.Rad2Deg(angleRad);
        }

        public static Number Angle(Vector3 lhs, Vector3 rhs)
        {
            Math.CheckRange(lhs);
            Math.CheckRange(rhs);

            return Math.Rad2Deg(AngleRad(lhs, rhs));
        }
        //弧度
        public static Number AngleRad(Vector3 lhs, Vector3 rhs)
        {
            Math.CheckRange(lhs);
            Math.CheckRange(rhs);

            lhs.Normalize();
            rhs.Normalize();
            //Number radians = Math.Acos(Number.Raw(Math.Clamp(Math.RndDiv(Dot(lhs, rhs), Math.FACTOR), -Math.FACTOR, Math.FACTOR)));
            Number radians = Math.Acos(Number.Raw(Math.Clamp(Dot(lhs, rhs) / Math.FACTOR, -Math.FACTOR, Math.FACTOR)));
            return radians;
        }

        public static Number Distance(Vector3 lhs, Vector3 rhs)
        {
            return (lhs - rhs).magnitude;
        }

        public static Vector3 Lerp(Vector3 from, Vector3 to, Number t)
        {
            t = Math.Clamp(t, Number.zero, Number.one);
            Vector3 vec = from * (Number.one - t) + to * t;
            return vec;
        }

        /*未能通过充分测试，仅限水平面向量的旋转，非水平面旋转无法保证与Unity结果一致
        水平面向量旋转的情况下，如果两个向量绝对平行反向，则与Unity结果一致，如果接近平行反向，则可能与Unity结果不同*/
        public static Vector3 RotateTowards(Vector3 current, Vector3 target, Number maxRadiansDelta, Number maxMagnitudeDelta)
        {
            Number magCurrent = current.magnitude;
            Number magTarget = target.magnitude;
            Vector3 dirCurrent = current.normalized;
            Vector3 dirTarget = target.normalized;
            Number deg = Vector3.Angle(dirCurrent, dirTarget);
            //float udeg = UE.Vector3.Angle((UE.Vector3)dirCurrent, (UE.Vector3)dirTarget);
            Vector3 dirNew;
            if (dirCurrent == dirTarget || Number.Approximately(deg, Number.zero, Number.one))
            {
                dirNew = dirTarget;
            }
            else
            {
                Vector3 rotationAxis = Vector3.CrossAndNormalize(dirCurrent, dirTarget);
                /*
                UE.Vector3 uecross = UE.Vector3.Cross((UE.Vector3)dirCurrent, (UE.Vector3)dirTarget);
                Test.Debug.DrawLine("Cross Unity", UE.Vector3.zero, (UE.Vector3)uecross.normalized * 10, UE.Color.red + UE.Color.yellow + UE.Color.blue);
                //*/
                if (rotationAxis == Vector3.zero)// || Number.Approximately(deg, new Number(180)))  //current is parallel with target
                {
                    if (dirCurrent == Vector3.up)
                        rotationAxis = Vector3.left;
                    else if (dirTarget == Vector3.up)
                        rotationAxis = Vector3.right;
                    else
                    {
                        Vector3 currentProj = current;
                        if (Math.Abs(current.x) < Math.Abs(current.z))
                        {
                            currentProj.x = Number.zero;
                            currentProj.Normalize();
                            rotationAxis = new Vector3(Number.zero, Math.Abs(currentProj.z), currentProj.y);
                            if (Math.Sign(current.y) != Math.Sign(current.z))
                                rotationAxis.z = -rotationAxis.z;
                            else
                                rotationAxis.y = -rotationAxis.y;
                        }
                        else
                        {
                            currentProj.z = Number.zero;
                            currentProj.Normalize();
                            rotationAxis = new Vector3(currentProj.y, -Math.Abs(currentProj.x), Number.zero);
                            if (Math.Sign(current.x) != Math.Sign(current.y))
                                rotationAxis.x = -rotationAxis.x;
                            else
                                rotationAxis.y = -rotationAxis.y;
                        }
                    }
                }
                //Test.Debug.DrawLine("Rotation Axis", UE.Vector3.zero, (UE.Vector3)rotationAxis * 10, UE.Color.magenta);
                Number maxDeg = Math.Rad2Deg(maxRadiansDelta);
                deg = Math.Min(deg, maxDeg);
                Quaternion quat = Quaternion.AngleAxis(deg, rotationAxis);
                dirNew = quat * dirCurrent;

                /*
                UE.Quaternion quatue = UE.Quaternion.AngleAxis((float)deg, (UE.Vector3)rotationAxis);
                UE.Vector3 vec = quatue * (UE.Vector3)dirCurrent;
                */
            }
            Number magDelta = magTarget - magCurrent;
            magDelta = Math.Sign(magDelta) * Math.Min(Math.Abs(magDelta), maxMagnitudeDelta);
            return dirNew * (magCurrent + magDelta);
        }

        public override string ToString()
        {
            return string.Format("({0}, {1}, {2})", x, y, z);
        }

        public static Vector3 Parse(string text)
        {
            if (text[0] == '(' || text[text.Length - 1] == ')')
                text = text.Substring(1, text.Length - 2);
            string[] tokens = text.Split(',');
            if (tokens.Length != 3)
                tokens = text.Split(' ');
            if (tokens.Length != 3)
                throw new System.FormatException("The input text must contains 3 elements.");
            Number x = Number.Parse(tokens[0]);
            Number y = Number.Parse(tokens[1]);
            Number z = Number.Parse(tokens[2]);
            return new Vector3(x, y, z);
        }

        public bool Equals(Vector3 other)
        {
            return x.Equals(other.x) && y.Equals(other.y) && z.Equals(other.z);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == this.GetType())
                return Equals((Vector3)obj);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode() ^ z.GetHashCode();
        }
    }
}
