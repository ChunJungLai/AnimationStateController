using UnityEngine;
using UnityEditor;
using UnityEditor.Animations;
using UnityEngine.Animations;

public class AnimatorStateEventBehaviourCopier
{
	public static AnimatorStateBehaviour copiedBehaviours = null;
}

public class AnimatorStateBehaviour : StateMachineBehaviour
{
	[Tooltip("Normalized event trigger animation time (0~1)")]
	[Range(0.0f, 1.0f)]
	public float TriggerPercentage = 0.0f;

	[Tooltip("Normalized event exit animation time (0~1)")]
	[Range(0.0f, 1.0f)]
	public float ExitPercentage = 1.0f;

	[Tooltip("Ensure that event is triggered in case of state interuption")]
	public bool mustTrigger = false;


	bool isRunning = false;
	int currentStateHash = 0;
	AnimatorStateInfo currentStateInfo;
	int currentLayerIndex = -1;
	bool isCurrLoopTriggered = false;
	int lastLoopTime = 0;
	bool isEnterMustTrigger = false;

	protected bool IsEnterMustTrigger
	{
		get
		{
			return isEnterMustTrigger;
		}
	}

	public void CheckInterrupted(Animator animator, int layerIndex, AnimatorStateInfo currInfo, AnimatorStateInfo nextInfo)
	{
		if (!isRunning)
		{
			return;
		}

		if (currentLayerIndex != layerIndex)
		{
			return;
		}

		if (currentStateHash == currInfo.fullPathHash || (nextInfo.fullPathHash != 0 && currentStateHash == nextInfo.fullPathHash))
		{
			return;
		}

		OnExit(animator, currentStateInfo, currentLayerIndex, true);
		isRunning = false;
	}

	#region Method
	public virtual void OnInit(Animator animator)
	{
	}

	public virtual void OnActive(Animator animator)
	{
	}

	public virtual void OnExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, bool isInterrupted)
	{
	}

	public virtual void OnTrigger(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}

	public virtual void OnUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
	}

	public virtual void OnRelease()
	{
	}
	#endregion

	#region UnityFollow
	void OnExitInternal(Animator animator, AnimatorStateInfo currentStateInfo, AnimatorStateInfo nextStateInfo, int layerIndex, bool isInterrupted)
	{
		if (!isRunning)
		{
			return;
		}

		if (mustTrigger && isCurrLoopTriggered == false)
		{
			isEnterMustTrigger = true;
			OnTrigger(animator, currentStateInfo, layerIndex);
		}

		OnExit(animator, currentStateInfo, layerIndex, isInterrupted);

		isRunning = false;
	}

	public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		isRunning = true;
		currentStateHash = stateInfo.fullPathHash;
		currentStateInfo = stateInfo;
		currentLayerIndex = layerIndex;
		isCurrLoopTriggered = false;
		lastLoopTime = 0;
		isEnterMustTrigger = false;
	}

	public sealed override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
	{
		// Do nothing
	}

	public sealed override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		AnimatorStateInfo nextStateInfo = animator.GetNextAnimatorStateInfo(layerIndex);
		OnExitInternal(animator, stateInfo, nextStateInfo, layerIndex, true);
	}

	public sealed override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
	{
		// Do nothing
	}

	public sealed override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		currentStateInfo = stateInfo;

		if (!isRunning)
		{
			return;
		}

		OnUpdate(animator, stateInfo, layerIndex);

		// Calculate loop time and animation time
		bool isLooping = stateInfo.loop;
		int timeDecimalPart = (int)stateInfo.normalizedTime;
		float timeFractionPart = stateInfo.normalizedTime - timeDecimalPart;

		// Check loop 
		if (isLooping && timeDecimalPart > lastLoopTime && timeFractionPart != 0f/*Avoid boundary condition*/)
		{
			isCurrLoopTriggered = false;
			lastLoopTime = timeDecimalPart;
		}

		// Transition process
		if (animator.IsInTransition(layerIndex))
		{
			AnimatorStateInfo nextStateInfo = animator.GetNextAnimatorStateInfo(layerIndex);
			AnimatorTransitionInfo transitionInfo = animator.GetAnimatorTransitionInfo(layerIndex);
			if (nextStateInfo.fullPathHash != stateInfo.fullPathHash
				|| (transitionInfo.anyState && nextStateInfo.normalizedTime != stateInfo.normalizedTime))
			{
				OnExitInternal(animator, stateInfo, nextStateInfo, layerIndex, transitionInfo.anyState);
				return;
			}
		}

		if (!isCurrLoopTriggered && timeFractionPart >= TriggerPercentage)
		{
			OnTrigger(animator, stateInfo, layerIndex);
			isCurrLoopTriggered = true;
		}

		if (!isLooping && ExitPercentage != 1.0f && (timeFractionPart >= ExitPercentage))
		{
			OnExitInternal(animator, stateInfo, new AnimatorStateInfo(), layerIndex, false);
		}
	}

	public sealed override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex, AnimatorControllerPlayable controller)
	{
		// Do nothing
	}
	#endregion

#if UNITY_EDITOR
	[ContextMenu("Copy")]
	protected void Copy()
	{
		AnimatorStateEventBehaviourCopier.copiedBehaviours = CreateInstance(GetType()) as AnimatorStateBehaviour;
		EditorUtility.CopySerialized(this, AnimatorStateEventBehaviourCopier.copiedBehaviours);
	}

	[ContextMenu("Paste as Value")]
	protected void PasteAsValue()
	{
		if (AnimatorStateEventBehaviourCopier.copiedBehaviours == null || AnimatorStateEventBehaviourCopier.copiedBehaviours.GetType() != GetType())
		{
			return;
		}

		EditorUtility.CopySerialized(AnimatorStateEventBehaviourCopier.copiedBehaviours, this);
	}

	[ContextMenu("Paste as New")]
	protected void PasteAsNew()
	{
		if (AnimatorStateEventBehaviourCopier.copiedBehaviours == null)
		{
			return;
		}

		StateMachineBehaviourContext[] contexts = AnimatorController.FindStateMachineBehaviourContext(this);
		if (contexts == null)
		{
			return;
		}

		StateMachineBehaviour copiedTarget = null;

		AnimatorState state = contexts[0].animatorObject as AnimatorState;
		if (state != null)
		{
			copiedTarget = state.AddStateMachineBehaviour(AnimatorStateEventBehaviourCopier.copiedBehaviours.GetType());
		}
		else
		{
			AnimatorStateMachine subMachine = contexts[0].animatorObject as AnimatorStateMachine;
			if (subMachine != null)
			{
				copiedTarget = subMachine.AddStateMachineBehaviour(AnimatorStateEventBehaviourCopier.copiedBehaviours.GetType());
			}
		}

		if (copiedTarget != null)
		{
			EditorUtility.CopySerialized(AnimatorStateEventBehaviourCopier.copiedBehaviours, copiedTarget);
		}
	}
#endif
}
