using System.Collections;

namespace IM.Test
{
    public class TestVector : UnitTest
    {
        public override string Name()
        {
            return "Vector";
        }

        public override void PrepareSteps()
        {
            AddStep("Vector3.normalized", TestNormalize);
            AddStep("Vector3.Cross", TestCross);
        }

        bool TestNormalize()
        {
            var tester = Utils.GenerateTester("Normalize",
                (Vector3 vec) => vec.normailzed, (UnityEngine.Vector3 vec) => vec.normalized,
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, 1, DevMode.Absolute, 0.001f);
            return Utils.TestCritical(tester) && Utils.TestRandom(tester, 100);
        }

        bool TestCross()
        {
            //const float DEV = 0.01f;
            ////for (int i = 0; i <= 100; ++i)
            //{
            //    int x1 = UnityEngine.Random.Range(Math.SUPPORTED_MIN, Math.SUPPORTED_MAX);
            //    int y1 = UnityEngine.Random.Range(Math.SUPPORTED_MIN, Math.SUPPORTED_MAX);
            //    int z1 = UnityEngine.Random.Range(Math.SUPPORTED_MIN, Math.SUPPORTED_MAX);
            //    int x2 = UnityEngine.Random.Range(Math.SUPPORTED_MIN, Math.SUPPORTED_MAX);
            //    int y2 = UnityEngine.Random.Range(Math.SUPPORTED_MIN, Math.SUPPORTED_MAX);
            //    int z2 = UnityEngine.Random.Range(Math.SUPPORTED_MIN, Math.SUPPORTED_MAX);
            //    x1 = 7338; y1 = -11283; z1 = 3762;
            //    x2 = 12269; y2 = -22961; z2 = 6129;
            //    Vector3 vec1 = new Vector3(x1, y1, z1);
            //    Vector3 vec2 = new Vector3(x2, y2, z2);
            //    vec1.Normalize();
            //    vec2.Normalize();
            //    Vector3 vec3 = Vector3.Cross(vec1, vec2);
            //    //vec3.Normalize();

            //    UnityEngine.Vector3 vector1 = new UnityEngine.Vector3((float)x1 / Math.FACTOR, (float)y1 / Math.FACTOR, (float)z1 / Math.FACTOR);
            //    UnityEngine.Vector3 vector2 = new UnityEngine.Vector3((float)x2 / Math.FACTOR, (float)y2 / Math.FACTOR, (float)z2 / Math.FACTOR);
            //    vector1.Normalize();
            //    vector2.Normalize();
            //    UnityEngine.Vector3 vector3 = UnityEngine.Vector3.Cross(vector1, vector2);
            //    vector3.Normalize();
            //    vector3 *= Math.FACTOR;

            //    int diffX = Math.Abs((int)vector3.x - vec3.x);
            //    float devX = (float)diffX / Math.FACTOR;
            //    int diffY = Math.Abs((int)vector3.y - vec3.y);
            //    float devY = (float)diffY / Math.FACTOR;
            //    int diffZ = Math.Abs((int)vector3.z - vec3.z);
            //    float devZ = (float)diffZ / Math.FACTOR;
            //    if (devX > DEV || devY > DEV || devZ > DEV)
            //    {
            //        Logger.LogError(string.Format("TestNormalize: ({0}, {1}, {2}) ({3}, {4}, {5})", x1, y1, z1, x2, y2, z2));
            //        Logger.LogError(string.Format("TestNormalize: {0}, {1} Cross: {2} {3}", vec1, vec2, vec3, vector3));
            //        return false;
            //    }
            //}
            //return true;
            var tester = Utils.GenerateTester("Cross", Vector3.Cross, 
                (UnityEngine.Vector3 v1, UnityEngine.Vector3 v2) => UnityEngine.Vector3.Cross(v1, v2).normalized,
                Math.SUPPORTED_MIN, Math.SUPPORTED_MAX, 1, DevMode.Absolute, 0.02f);
            return Utils.TestCritical(tester) && Utils.TestRandom(tester, 1000);
        }
    }
}
