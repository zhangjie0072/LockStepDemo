using System;
using UE = UnityEngine;

namespace IM
{
    public struct Vector2 : IEquatable<Vector2>
    {
        public Number x, y;

        public static Vector2 zero = new Vector2(Number.zero, Number.zero);
        public static Vector2 one = new Vector2(Number.one, Number.one);
        public static Vector2 up = new Vector2(Number.zero, Number.one);
        public static Vector2 down = new Vector2(Number.zero, -Number.one);
        public static Vector2 left = new Vector2(-Number.one, Number.zero);
        public static Vector2 right = new Vector2(Number.one, Number.zero);

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
        public Vector3 xz
        {
            get
            {
                return new IM.Vector3(this.x, IM.Number.zero,this.y);
            }
        }

        public Vector3 xy
        {
            get
            {
                return new IM.Vector3(this.x, this.y, IM.Number.zero);
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

        public Vector2 normalized
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

        public Vector2(Number x)
        {
            this.x = x;
            this.y = x;
        }
        public Vector2(Number x, Number y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2 operator + (Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x + rhs.x, lhs.y + rhs.y);
        }

        public static Vector2 operator - (Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x - rhs.x, lhs.y - rhs.y);
        }

        public static Vector2 operator - (Vector2 lhs)
        {
            return new Vector2(-lhs.x, -lhs.y);
        }

        public static Vector2 operator * (Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x * rhs.x, lhs.y * rhs.y);
        }

        public static Vector2 operator *(Vector2 lhs, Number rhs)
        {
            return new Vector2(lhs.x * rhs, lhs.y * rhs);
        }
        public static Vector2 operator *(Number lhs, Vector2 rhs)
        {
            return new Vector2(lhs * rhs.x, lhs * rhs.y);
        }

        public static Vector2 operator / (Vector2 lhs, Vector2 rhs)
        {
            return new Vector2(lhs.x / rhs.x, lhs.y / rhs.y);
        }

        public static Vector2 operator / (Vector2 lhs, Number rhs)
        {
            return new Vector2(lhs.x / rhs, lhs.y / rhs);
        }

        public static bool operator == (Vector2 lhs, Vector2 rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator != (Vector2 lhs, Vector2 rhs)
        {
            return !lhs.Equals(rhs);
        }

        public static explicit operator UnityEngine.Vector2 (Vector2 lhs)
        {
            return new UnityEngine.Vector2((float)lhs.x, (float)lhs.y);
        }

        public void Normalize()
        {
            this = normalized;
        }

        public static int Dot(Vector2 lhs, Vector2 rhs)
        {
            Math.CheckRange(lhs);
            Math.CheckRange(rhs);

            int xTmp = lhs.x.raw * rhs.x.raw;
            int yTmp = lhs.y.raw * rhs.y.raw;
            return xTmp + yTmp;
        }


        public static Number Distance(Vector2 lhs, Vector2 rhs)
        {
            return (lhs - rhs).magnitude;
        }

        public static Number Angle(Vector2 lhs,Vector2 rhs)
        {
            Math.CheckRange(lhs);
            Math.CheckRange(rhs);

            return Math.Rad2Deg(AngleRad(lhs, rhs));
        }

        public static Number AngleRad(Vector2 lhs, Vector2 rhs)
        {
            Math.CheckRange(lhs);
            Math.CheckRange(rhs);

            lhs.Normalize();
            rhs.Normalize();
            Number radians = Math.Acos(Number.Raw(Math.Clamp(Dot(lhs, rhs) / Math.FACTOR, -Math.FACTOR, Math.FACTOR)));
            return radians;
        }

        public static Vector2 Lerp(Vector2 from, Vector2 to, Number t)
        {
            t = Math.Clamp(t, Number.zero, Number.one);
            Vector2 vec = from * (Number.one - t) + to * t;
            return vec;
        }

        public override string ToString()
        {
            return string.Format("({0}, {1})", x, y);
        }

        public static Vector2 Parse(string text)
        {
            if (text[0] == '(' || text[text.Length - 1] == ')')
                text = text.Substring(1, text.Length - 2);
            string[] tokens = text.Split(',');
            if (tokens.Length != 2)
                tokens = text.Split(' ');
            if (tokens.Length != 2)
                throw new System.FormatException("The input text must contains 2 elements.");
            Number x = Number.Parse(tokens[0]);
            Number y = Number.Parse(tokens[1]);
            return new Vector2(x, y);
        }

        public bool Equals(Vector2 other)
        {
            return x.Equals(other.x) && y.Equals(other.y);
        }

        public override bool Equals(object obj)
        {
            if (obj.GetType() == this.GetType())
                return Equals((Vector2)obj);
            else
                return false;
        }

        public override int GetHashCode()
        {
            return x.GetHashCode() ^ y.GetHashCode();
        }
    }
}
