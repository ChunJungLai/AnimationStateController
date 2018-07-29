using UnityEngine;
using System.Collections;

namespace BadBird
{

namespace StringStrategy
{

public class DuplicateAndNoOrder : DuplicateAndPreserveOrder
{
    public DuplicateAndNoOrder(int maxSize) : base(maxSize)
    {
        
    }

    override public int FindIndexOf(string text)
    {
        throw new System.Exception("Not Supported Operation");
    }

    override public string FindText(int index)
    {
        throw new System.Exception("Not Supported Operation");
    }
}

}

}
