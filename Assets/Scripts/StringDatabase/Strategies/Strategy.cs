using UnityEngine;
using System;
using System.Collections;
using System.Collections.ObjectModel;

namespace BadBird
{

namespace StringStrategy
{

abstract public class Strategy
{
    abstract public int MaxSize { get; set; } // if max size is reached, kill the first element to accpect new one
    abstract public bool Add(string text);
    abstract public bool Remove(string text);
    abstract public bool Has(string text);
    abstract public void Clear();
    abstract public int FindIndexOf(string text);
    abstract public string FindText(int index);
    abstract public ReadOnlyCollection<string> Get();
    abstract public ReadOnlyCollection<string> Find(Func<string, bool> predicate, int count = Parameter.UNLIMITED_SIZE);
}

}

}
