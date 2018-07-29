using UnityEngine;
using System.Collections;

namespace BadBird
{

namespace StringStrategy
{

public struct Parameter
{
    public const int UNLIMITED_SIZE = int.MaxValue;

    public bool allowDuplicate;
    public bool preserveOrder;
    public int maxSize;

    public Parameter(bool allowDuplicate, bool preserveOrder, int maxSize = UNLIMITED_SIZE)
    {
        this.allowDuplicate = allowDuplicate;
        this.preserveOrder = preserveOrder;
        this.maxSize = maxSize;
    }
}

}

}
