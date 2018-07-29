using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BadBird
{

namespace StringStrategy
{

public class DuplicateAndPreserveOrder : Strategy
{
    private List<string> strings = new List<string>();
    private int maxSize = Parameter.UNLIMITED_SIZE;
    private Helper helper = new Helper();

    public DuplicateAndPreserveOrder(int maxSize)
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
        if (strings.Count == maxSize)
        {
            strings.RemoveAt(0);
        }

        strings.Add(text);
        return true;
    }

    override public bool Remove(string text)
    {
        IEnumerable<string> results = strings.Where(s => s.Equals(text) == false);
        
        if (results.Count<string>() == strings.Count)
        {
            return false;
        }
        else
        {
            strings = new List<string>(results);
            return true;
        }
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
        return strings.IndexOf(text);
    }

    override public string FindText(int index)
    {
        try
        {
            string result = strings[index];
            return result;
        }
        catch
        {
            return string.Empty;
        }
    }

    override public ReadOnlyCollection<string> Get()
    {
        return strings.AsReadOnly();
    }

    override public ReadOnlyCollection<string> Find(Func<string, bool> predicate, int count = Parameter.UNLIMITED_SIZE)
    {
        IEnumerable<string> results = strings.Where(predicate);
        return helper.ParseResults(results, count);
    }
}

}

}
