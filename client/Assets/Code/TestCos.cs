using UnityEngine;
using System.Collections;

public class TestCos : MonoBehaviour
{
	float pi()
	{
		return 3.14159265358979323846264338327950288f;
	}

	float two_pi()
	{
		return 6.28318530717958647692528676655900576f;
	}

	float root_pi()
	{
		return 1.772453850905516027f;
	}

	float half_pi()
	{
		return 1.57079632679489661923132169163975144f;
	}


    float cos_52s(float x)
	{
		float xx = x * x;
        Logger.Log(x + " " + xx);

        const float a = 0.9999932946f;
        const float b = -0.4999124376f;
        const float c = 0.0414877472f;
        const float d = -0.0012712095f;
        Logger.Log(string.Format("{0} {1} {2} {3}", a, b, c, d));

        float r1 = c + xx * d;
        float r2 = b + xx * r1;
        float r3 = a + xx * r2;
        Logger.Log(string.Format("{0} {1} {2}", r1, r2, r3));

        return r3;
	}

    float to_radians(float degrees)
	{
		return degrees * 0.01745329251994329576923690768489f;
	}

	float to_degrees(float radians)
	{
		return radians * 57.295779513082320876798154814105f;
	}


	float fastCos(float x)
	{
		float angle = x;

		if(angle < half_pi())
			return cos_52s(angle);
		if(angle < pi())
			return -cos_52s(pi() - angle);
		if(angle < (3 * half_pi()))
			return -cos_52s(angle - pi());

		return cos_52s(two_pi() - angle);
	}

    void Start()
    {
        //for (int i = 0; i <= 90; ++i)
        //{
        //    float rad = to_radians(i);
        //    float r1 = (float)System.Math.Cos(rad);
        //    float r2 = fastCos(rad);
        //    float diff = System.Math.Abs(r1 - r2);
        //    Logger.Log(i + " " + diff + " " + r1 + " " + r2);
        //}
        fastCos(to_radians(50));
    }
}
