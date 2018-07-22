using UnityEngine;
using System.Collections;

public static class MethodExtensionForMaterial
{
	public static void OnOffShaderKeyword(this Material material, string keyword, bool switchOn)
	{
		if(switchOn)
		{
			material.EnableKeyword(keyword);
		}
		else
		{
			material.DisableKeyword(keyword);
		}
	}
}