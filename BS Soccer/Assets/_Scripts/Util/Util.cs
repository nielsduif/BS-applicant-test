using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Util
{
    /// <summary>
    /// Converting a float3 to a vector3, static to call whereever
    /// </summary>
    /// <param name="_values"></param>
    /// <returns></returns>
    public static Vector3 Float3ToVector(float[] _values)
    {
        return new Vector3(_values[0], _values[1], _values[2]);
    }
}
