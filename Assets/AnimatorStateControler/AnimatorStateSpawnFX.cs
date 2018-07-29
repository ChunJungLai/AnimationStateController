using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine.Animations;
using System.Collections;
using System.Collections.Generic;
using BadBird.StringStrategy;

public class AnimatorStateSpawnFX : AnimatorStateBehaviour
{
	[AnimatorState]
	public string AttachTransformName;
	public GameObject FX;
	public List<string> ChildName = new List<string>();

	public override void OnTrigger(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		List<Object> ChildGameobject = new List<Object>();
		ChildGameobject.AddRange(animator.gameObject.GetComponentsInChildren<Object>());
		for(int aIndex=0; aIndex< ChildGameobject.Count;aIndex++)
		{
			ChildName.Add(ChildGameobject[aIndex].name);
		}
		if (string.IsNullOrEmpty(AttachTransformName) || FX ==null)
		{
			return;
		}
		GameObject AttachTransformGameobject = animator.gameObject.FindChildByName(AttachTransformName);
		Instantiate(FX, AttachTransformGameobject.transform.position, Quaternion.identity, animator.gameObject.transform);
	}

}
