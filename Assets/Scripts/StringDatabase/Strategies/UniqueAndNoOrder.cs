using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BadBird
{

namespace StringStrategy
{

public class UniqueAndNoOrder : Strategy
{
    private HashSet<string> strings = new HashSet<string>();
    private int maxSize = Parameter.UNLIMITED_SIZE;
    private Helper helper = new Helper();

    public UniqueAndNoOrder(int maxSize)
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
            string first = strings.First<string>();
            Remove(first);
        }

        bool result = strings.Add(text);
        return result;
    }

    override public bool Remove(string text)
    {
        bool result = strings.Remove(text);
        return result;
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
        throw new System.Exception("Not Supported Operation");
    }

    override public string FindText(int index)
    {
        throw new System.Exception("Not Supported Operation");
    }

    override public ReadOnlyCollection<string> Get()
    {
        return strings.ToList<string>().AsReadOnly();
    }

    override public ReadOnlyCollection<string> Find(Func<string, bool> predicate, int count = Parameter.UNLIMITED_SIZE)
    {
        IEnumerable<string> results = strings.Where(predicate);
        return helper.ParseResults(results, count);
    }
}

}

}
