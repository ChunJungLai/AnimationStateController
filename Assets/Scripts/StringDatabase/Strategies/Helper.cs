using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace BadBird
{

namespace StringStrategy
{

public class Helper
{
    private List<string> queries = new List<string>();
    private readonly List<string> empty = new List<string>();

    public void Clear()
    {
        queries.Clear();
    }

    public ReadOnlyCollection<string> ParseResults(IEnumerable<string> results, int count)
    {
        if (results.Any<string>() == false)
        {
            return empty.AsReadOnly();
        }

        IEnumerator<string> it = results.GetEnumerator();
        queries.Clear();
        for (int i = 0; i < count; ++i)
        {
            if (it.MoveNext() == false)
            {
                break;
            }
            queries.Add(it.Current);
        }
        return queries.AsReadOnly();
    }
}

}

}
