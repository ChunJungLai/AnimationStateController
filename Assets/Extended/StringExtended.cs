using UnityEngine;
using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

public static class MethodExtensionForString
{
    static StringBuilder sb = new StringBuilder();

    public static void Clear(this StringBuilder sb)
    {
        sb.Length = 0;
    }

    public static string ToUnityStyle(this string str)
    {
        // Replace all special characters to space
        str = Regex.Replace(str, @"[\W_]+", " ");

        // Initial string builder
        sb.Clear();
        sb.Append(str);

        // Remove "m_" or "M_"
        if(str.StartsWith("m ") || str.StartsWith("M "))
        {
            sb.Remove(0, 2);
        }

        // Change first alphabat to uppercase
        bool isFirst = true;
        for(int i = 0; i < sb.Length; ++i)
        {
            if(isFirst)
            {
                sb.Replace(sb[i], char.ToUpper(sb[i]), i, 1);
                isFirst = false;
            }

            if(sb[i] == ' ')
            {
                isFirst = true;
            }
        }

        // Split Uppercase alphabat by " "
        str = sb.ToString();
        str = Regex.Replace(str, @"[\w][A-Z]+", m => m.Value.Insert(1, " "));

        return str;
    }

    public static bool Contains(this string source, string toCheck, StringComparison comp)
    {
        return source.IndexOf(toCheck, comp) >= 0;
    }
}