using UnityEngine;
using System.Collections;

public static class MethodExtensionForMonoBehaviour 
{
	/// <summary>
	/// Gets or add a component. Usage example:
	/// BoxCollider boxCollider = transform.GetOrAddComponent<BoxCollider>();
	/// </summary>
	static public T GetOrAddComponent<T> (this Component child) where T: Component 
	{
		T result = child.GetComponent<T>();
		if (result == null) 
		{
			result = child.gameObject.AddComponent<T>();
		}
		return result;
	}

	static public T GetOrAddComponent<T> (this GameObject child) where T: Component
	{
		T result = child.GetComponent<T>();
		if (result == null) 
		{
			result = child.AddComponent<T>();
		}
		return result;
	}

	public static Component DeepCloneTo(this Component source, GameObject target)
	{
		System.Type type = source.GetType();
		Component copy = target.AddComponent(type);

		System.Reflection.FieldInfo[] fields = type.GetFields(); 
		foreach (System.Reflection.FieldInfo field in fields)
		{
			field.SetValue(copy, field.GetValue(source));
		}

		return copy;
	}
}