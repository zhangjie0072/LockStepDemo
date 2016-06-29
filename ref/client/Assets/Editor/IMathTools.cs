namespace IM.Editor
{
    public static class Tools
    {
        public static Number Convert(float f)
        {
             return Number.Raw(UnityEngine.Mathf.RoundToInt(f * Math.FACTOR));
        }

        public static Vector2 Convert(UnityEngine.Vector2 vec)
        {
            Number x = Convert(vec.x);
            Number y = Convert(vec.y);
            return new Vector2(x,y);
        }

        public static Vector3 Convert(UnityEngine.Vector3 vec)
        {
            Number x = Convert(vec.x);
            Number y = Convert(vec.y);
            Number z = Convert(vec.z);
            return new Vector3(x,y,z);
        }

        public static Quaternion Convert(UnityEngine.Quaternion quat)
        {
            Number x = Convert(quat.x);
            Number y = Convert(quat.y);
            Number z = Convert(quat.z);
            Number w = Convert(quat.w);
            return new Quaternion(x, y, z, w);
        }
    }
}
