using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine.Animations;
using System.Collections;
using System.Collections.Generic;
using BadBird.StringStrategy;

public class AnimatorStateSpawnFX : AnimatorStateBehaviour
{
	
	public string AttachTransformName;
	public GameObject FX;
	public List<string> ChildName = new List<string>();

	public override void OnTrigger(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
        List<Transform> ChildGameobject = new List<Transform>();
		ChildGameobject.AddRange(animator.gameObject.GetComponentsInChildren<Transform>());
		for(int aIndex=0; aIndex< ChildGameobject.Count;aIndex++)
		{
			ChildName.Add(ChildGameobject[aIndex].name);
            string aTarget = ChildGameobject[aIndex].name.ToLower();

            if (aTarget.IndexOf(AttachTransformName.ToLower()) != -1)
            {
                //Debug.Log(ChildGameobject[aIndex].name);
            }
        }

		if (string.IsNullOrEmpty(AttachTransformName) || FX ==null)
		{
			return;
		}
		//GameObject AttachTransformGameobject = animator.gameObject.FindChildByName(AttachTransformName);
		//Instantiate(FX, AttachTransformGameobject.transform.position, Quaternion.identity, animator.gameObject.transform);
	}

}
