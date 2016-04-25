using UnityEngine;
using System.Collections;

public class TestCosI : MonoBehaviour
{
    const int FACTOR = 100000;

	const int pi = (int)(3.14159265358979323846264338327950288 * FACTOR);

	const int two_pi = (int)(6.28318530717958647692528676655900576 * FACTOR);

	const int root_pi = (int)(1.772453850905516027 * FACTOR);

	const int half_pi = (int)(1.57079632679489661923132169163975144 * FACTOR);

    int cos_52s(int x)
	{
		long xx = (long)x * x / FACTOR;
        //Logger.Log(x + " " + xx);

        const int a = (int)(0.9999932946f * FACTOR);
        const int b = (int)(-0.4999124376f * FACTOR);
        const int c = (int)(0.0414877472f * FACTOR);
        const int d = (int)(-0.0012712095f * FACTOR);
        //Logger.Log(string.Format("{0} {1} {2} {3}", a, b, c, d));

        long r1 = c + xx * d / FACTOR;
        long r2 = b + xx * r1 / FACTOR;
        long r3 = a + xx * r2 / FACTOR;
        //Logger.Log(string.Format("{0} {1} {2}", r1, r2, r3));

        return (int)r3;
	}

    float to_radians(float degrees)
	{
		return degrees * 0.01745329251994329576923690768489f;
	}

	float to_degrees(float radians)
	{
		return radians * 57.295779513082320876798154814105f;
	}


	int fastCos(int x)
	{
		int angle = x;

		if(angle < half_pi)
			return cos_52s(angle);
		if(angle < pi)
			return -cos_52s(pi - angle);
		if(angle < (3 * half_pi))
			return -cos_52s(angle - pi);

		return cos_52s(two_pi - angle);
	}

    void Start()
    {
        for (int i = 0; i <= 90; ++i)
        {
            if (i == 50)
                Debug.DebugBreak();
            float rad = to_radians(i);
            float r1 = (float)System.Math.Cos(rad);
            int r2 = fastCos((int)(rad * FACTOR));
            float diff = System.Math.Abs(r1 - (float)r2/FACTOR);
            Logger.Log(i + " " + diff + " " + r1 + " " + r2);
        }
        //fastCos((int)(to_radians(50) * FACTOR));
    }
}
