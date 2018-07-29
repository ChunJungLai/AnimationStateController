using UnityEngine;
using System.Collections;

namespace BadBird
{

namespace StringStrategy
{

public static class Factory
{
    static public Strategy Create(Parameter parameter)
    {
        if (parameter.maxSize <= 0)
        {
            throw new System.Exception("Invalid Size Detected : " + parameter.maxSize);
        }

        if (parameter.allowDuplicate)
        {
            if (parameter.preserveOrder)
            {
                return new DuplicateAndPreserveOrder(parameter.maxSize);
            } 
            else
            {
                return new DuplicateAndNoOrder(parameter.maxSize);
            }
        }
        else
        {
            if (parameter.preserveOrder)
            {
                return new UniqueAndPreserveOrder(parameter.maxSize);
            }
            else
            {
                return new UniqueAndNoOrder(parameter.maxSize);
            }
        }
    }
}

}

}
