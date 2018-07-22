using UnityEngine;
using System.Collections;

public static class MethodExtensionForArray
{
	public static string Print<T>(this T[] data, string delimiter = ", ", string prefix = "{", string suffix = "}")
	{
		string[] strData = new string[data.Length];
		for(int i = 0; i < data.Length; ++i)
		{
			strData[i] = data[i].ToString();
		}
		return prefix + string.Join(delimiter, strData) + suffix;
	}
	
	public static T[] SubArray<T>(this T[] data, int index, int length)
	{
		T[] result = new T[length];
		for(int i = 0; i < length && index + i < data.Length; ++i)
		{
			result[i] = data[index + i];
		}
		return result;
	}

	public static T[] Resize<T>(this T[] data, int newSize)
	{
		System.Array.Resize(ref data, newSize);
		return data;
	}

    public static T[] Add<T>(this T[] data, T newData)
    {
        data = data.Resize(data.Length + 1);
        data[data.Length - 1] = newData;
        return data;
    }

    public static T[] Remove<T>(this T[] data, T removeData, System.Func<T, T, bool> equality = null)
    {
        int removeIndex = -1;
        for(int i = 0; i < data.Length; ++i)
		{
            if((equality == null && data[i].Equals(removeData)) || (equality != null && equality(data[i], removeData)))
            {
                removeIndex = i;
                break;
            }
        }

        T[] newArray = System.Array.CreateInstance(typeof(T), data.Length - 1) as T[];
        if(removeIndex != -1)
        {   
            for(int i = 0; i < removeIndex; ++i)
            {
                newArray[i] = data[i];
            }
            for(int i = removeIndex + 1; i < data.Length; ++i)
            {
                newArray[i - 1] = data[i];
            }
        }

        return removeIndex != -1 ? newArray : data;
    }
}