using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;

namespace BadBird
{

namespace StringStrategy
{

public class UniqueAndPreserveOrder : Strategy
{
    private OrderedDictionary strings = new OrderedDictionary(); // {Key : Value} = {string : index}
    private int maxSize = Parameter.UNLIMITED_SIZE;
    private Helper helper = new Helper();
    private List<string> results = new List<string>();

    public UniqueAndPreserveOrder(int maxSize)
    {
        MaxSize = maxSize;
    }

    override public int MaxSize
    {
        get
        {
            return maxSize;
        }
        set
        {
            maxSize = value;
        }
    }

    override public bool Add(string text)
    {
        if (strings.Contains(text))
        {
            return false;
        }

        if (strings.Count == maxSize)
        {
            strings.RemoveAt(0);
        }

        strings.Add(text, strings.Count); // {Key : Value} = {string : index}
        return true;
    }

    override public bool Remove(string text)
    {
        if (strings.Contains(text) == false)
        {
            return false;
        }

        strings.Remove(text);
        return true;
    }

    override public bool Has(string text)
    {
        return strings.Contains(text);
    }

    override public void Clear()
    {
        strings.Clear();
        helper.Clear();
    }

    override public int FindIndexOf(string text)
    {
        if (strings.Contains(text) == false)
        {
            return -1;
        }

        int index = (int)(strings[text]);
        return index;
    }

    override public string FindText(int index)
    {
        if (index < 0 || index >= strings.Count)
        {
            return string.Empty;
        }

        return (string)(strings[index]);
    }

    override public ReadOnlyCollection<string> Get()
    {
        results.Clear();
        var keys = strings.AsReadOnly().Keys;
        foreach (object o in keys)
        {
            string s = (string)(o);
            results.Add(s);
        }
        return results.AsReadOnly();
    }

    override public ReadOnlyCollection<string> Find(Func<string, bool> predicate, int count = Parameter.UNLIMITED_SIZE)
    {
        results.Clear();
        for (int i = 0, size = strings.Count; i < size; ++i)
        {
            string s = (string)(strings[i]);
            if (predicate(s))
            {
                results.Add(s);
            }
        }
        return helper.ParseResults(results, count);
    }
}

}

}
