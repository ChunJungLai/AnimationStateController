using UnityEngine;
using System.Collections.Generic;

public static partial class ExtensionMethods
{
	static List<GameObject> recycleList = new List<GameObject>();

	public static void DestroyAllChilds(this GameObject targetObj, bool destroyImmediate = true)
	{
		foreach(Transform child in targetObj.transform)
		{
			recycleList.Add(child.gameObject);
		}
		
		if(destroyImmediate)
		{
			recycleList.ForEach(child => GameObject.DestroyImmediate(child));
		}
		else
		{
			recycleList.ForEach(child => GameObject.Destroy(child));
		}
		
		recycleList.Clear();
	}

	public static GameObject InstantiateWithParent(this GameObject targetObj, Transform parent)
	{
		GameObject gObj = GameObject.Instantiate(targetObj) as GameObject;
		Transform trans = gObj.transform;
		if(parent != null)
		{
			trans.parent = parent;
		}
		
		return gObj;
	}

	public static GameObject InstantiateWithParent(this GameObject targetObj, 
	                                               Transform parent, 
	                                               Vector3 localPosition, 
	                                               Quaternion localRotation, 
	                                               Vector3 localScale)
	{
		GameObject gObj = GameObject.Instantiate(targetObj) as GameObject;
		Transform trans = gObj.transform;
		if(parent != null)
		{
			trans.parent = parent;
		}

		trans.localPosition = localPosition;
		trans.localRotation = localRotation;
		trans.localScale = localScale;

		return gObj;
	}

	public static GameObject FindChildByName(this GameObject target, string name, bool recursive = true, bool onlyActiveChild = false, bool containIsMatch = false)
	{		

        if (target == null)
            return null;

        Transform targetTrans = target.transform;
        for (int i = targetTrans.childCount - 1; i >= 0; --i)
		{
			GameObject objChild = targetTrans.GetChild(i).gameObject;

            if (onlyActiveChild && !objChild.activeInHierarchy)
                continue;

            if (!containIsMatch)
            {
                if (objChild.name == name)
                {
                    return objChild;
                }
            }
            else
            {
                if (objChild.name.Contains( name ))
                {
                    return objChild;
                }
            }

			if (recursive)
			{
				GameObject objGrandChild = objChild.FindChildByName(name, recursive, onlyActiveChild, containIsMatch);
				if(objGrandChild != null)
				{
					return objGrandChild;
				}
			}
		}

		return null;
	}		
}
