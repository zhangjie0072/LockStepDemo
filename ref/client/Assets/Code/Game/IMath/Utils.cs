namespace IM
{
    public static class Utils
    {
        public static bool EnableTrace = false;

        static public void Trace(object message)
        {
            Trace(message, null);
        }
        static public void Trace(object message, UnityEngine.Object context)
        {
            if (EnableTrace)
            {
                UnityEngine.Debug.Log(message, context);
            }
        }
    }
}
