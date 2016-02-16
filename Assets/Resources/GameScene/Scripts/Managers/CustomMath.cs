using UnityEngine;
using System.Collections;

public class CustomMath
{
    public static float Lerp(float ori, float target, float rate)
    {
        return ori + Mathf.Abs(ori - target) * rate;
    }
}
