using UnityEngine;
using System.Collections;

public static class MethodExtensionForVector
{
	public static Vector2 Abs(this Vector2 vec)
	{
		vec.x = Mathf.Abs(vec.x);
		vec.y = Mathf.Abs(vec.y);
		return vec;
	}
	
	public static Vector3 Abs(this Vector3 vec)
	{
		vec.x = Mathf.Abs(vec.x);
		vec.y = Mathf.Abs(vec.y);
		vec.z = Mathf.Abs(vec.z);
		return vec;
	}
	
	public static Vector4 Abs(this Vector4 vec)
	{
		vec.x = Mathf.Abs(vec.x);
		vec.y = Mathf.Abs(vec.y);
		vec.z = Mathf.Abs(vec.z);
		vec.w = Mathf.Abs(vec.w);
		return vec;
	}

    public static bool EqualValues(this Vector2 vec, Vector2 other, float tolerance = 0f)
    {
        if(tolerance == 0f)
        {
            return float.Equals(vec.x, other.x) && float.Equals(vec.y, other.y);
        }
        else
        {
            return Mathf.Abs(vec.x - other.x) < Mathf.Abs(tolerance) && Mathf.Abs(vec.y - other.y) < Mathf.Abs(tolerance);
        }
    }

    public static bool EqualValues(this Vector3 vec, Vector3 other, float tolerance = 0f)
    {
        if(tolerance == 0f)
        {
            return float.Equals(vec.x, other.x) && float.Equals(vec.y, other.y) && float.Equals(vec.z, other.z);
        }
        else
        {
            return Mathf.Abs(vec.x - other.x) < Mathf.Abs(tolerance) && Mathf.Abs(vec.y - other.y) < Mathf.Abs(tolerance) && Mathf.Abs(vec.z - other.z) < Mathf.Abs(tolerance);
        }
    }

    public static bool EqualValues(this Vector4 vec, Vector4 other, float tolerance = 0f)
    {
        if(tolerance == 0f)
        {
            return float.Equals(vec.x, other.x) && float.Equals(vec.y, other.y) && float.Equals(vec.z, other.z) && float.Equals(vec.w, other.w);
        }
        else
        {
            return Mathf.Abs(vec.x - other.x) < Mathf.Abs(tolerance) && Mathf.Abs(vec.y - other.y) < Mathf.Abs(tolerance) && Mathf.Abs(vec.z - other.z) < Mathf.Abs(tolerance) && Mathf.Abs(vec.w - other.w) < Mathf.Abs(tolerance);
        }
    }
}