using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine.Animations;
using System.Collections;
using System.Collections.Generic;

public class AnimatorStateSpawnFX : AnimatorStateBehaviour
{
	public string AttachTransformName;
	public GameObject FX;

	public override void OnTrigger(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		/*if(string.IsNullOrEmpty(AttachTransformName) || FX ==null)
		{
			return;
		}*/
		//GameObject AttachTransformGameobject = animator.gameObject.
		//Instantiate(FX, animator.transform.position,Quaternion.identity, animator.gameObject.transform);
	}

}
