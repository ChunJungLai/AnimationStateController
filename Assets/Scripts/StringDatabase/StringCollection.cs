using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using BadBird.StringStrategy;

namespace BadBird
{

public class StringCollection // so C# has a StringCollection already
{
    private readonly Strategy strategy = null;

    public StringCollection(Parameter parameter)
    {
        strategy = Factory.Create(parameter);
    }

    public bool Add(string text)
    {
        return strategy.Add(text);
    }

    public bool Remove(string text)
    {
        return strategy.Remove(text);
    }

    public bool Has(string text)
    {
        return strategy.Has(text);
    }

    public void Clear()
    {
        strategy.Clear();
    }

    public void SetMaxSize(int maxSize)
    {
        strategy.MaxSize = maxSize;
    }

    public ReadOnlyCollection<string> Get()
    {
        return strategy.Get();
    }

    public ReadOnlyCollection<string> Find(Func<string, bool> predicate, int count = Parameter.UNLIMITED_SIZE)
    {
        return strategy.Find(predicate, count);
    }

    #region Easy-to-use Operations

    public ReadOnlyCollection<string> FindAllWithPrefix(string prefix, int count = Parameter.UNLIMITED_SIZE)
    {
        return strategy.Find(s => s.StartsWith(prefix), count);
    }

    public ReadOnlyCollection<string> FindAllThatContains(string input, int count = Parameter.UNLIMITED_SIZE)
    {
        return strategy.Find(s => s.Contains(input), count);
    }

    // expand for more operations here

    #endregion
}

} // end of namespace BadBird
