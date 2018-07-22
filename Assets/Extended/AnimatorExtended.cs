using UnityEngine;
using System.Collections;

public static class MethodExtensionForAnimator
{
    static bool IsStatePlayingInternal(string layerName, string stateName, AnimatorStateInfo stateInfo)
    {
        if(!stateName.Contains(".") && stateInfo.shortNameHash == Animator.StringToHash(stateName))
        {
            return true;
        }
        else if(stateName.StartsWith(layerName) && stateInfo.fullPathHash == Animator.StringToHash(stateName))
        {
            return true;
        }
        else
        {
            string fullPathStateName = layerName + stateName;
            if(stateInfo.fullPathHash == Animator.StringToHash(fullPathStateName))
            {
                return true;
            }
        }

        return false;
    }

    public static bool IsStatePlaying(this Animator animator, string stateName, out int playingLayerIndex, bool checkNext = false)
    {
        for(int i = 0; i < animator.layerCount; ++i)
        {
            string layerName = animator.GetLayerName(i);
            layerName = layerName + ".";

            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(i);
            if(IsStatePlayingInternal(layerName, stateName, stateInfo))
            {
                playingLayerIndex = i;
                return true;
            }

            if(checkNext && animator.IsInTransition(i))
            {
                AnimatorStateInfo nextStateInfo = animator.GetNextAnimatorStateInfo(i);
                if(IsStatePlayingInternal(layerName, stateName, nextStateInfo))
                {
                    playingLayerIndex = i;
                    return true;
                }
            }
        }

        playingLayerIndex = -1;
        return false;
    }

    public static void ResetAllTriggers(this Animator animator, bool includeBoolParamaters = true)
    {
        for(int i = 0; i < animator.parameterCount; ++i)
        {
            AnimatorControllerParameter param = animator.parameters[i];
            if(param.type == AnimatorControllerParameterType.Bool && includeBoolParamaters)
            {
                animator.SetBool(param.name, false);
            }
            else if(param.type == AnimatorControllerParameterType.Trigger)
            {
                animator.ResetTrigger(param.name);
            }
        }
    }

    public static void WrappedCrossFade(this Animator animator, int stateNameHash, float transitionDuration)
    {
        animator.CrossFade(stateNameHash, transitionDuration);
    }

    public static void WrappedCrossFade(this Animator animator, string stateName, float transitionDuration)
    {
        animator.CrossFade(stateName, transitionDuration);
    }

    public static void WrappedCrossFade(this Animator animator, int stateNameHash, float transitionDuration, int layer)
    {
        animator.CrossFade(stateNameHash, transitionDuration, layer);
    }

    public static void WrappedCrossFade(this Animator animator, string stateName, float transitionDuration, int layer)
    {
        animator.CrossFade(stateName, transitionDuration, layer);
    }

    public static void WrappedCrossFade(this Animator animator, int stateNameHash, float transitionDuration, int layer, float normalizedTime)
    {
        animator.CrossFade(stateNameHash, transitionDuration, layer, normalizedTime);
    }

    public static void WrappedCrossFade(this Animator animator, string stateName, float transitionDuration, int layer, float normalizedTime)
    {
        animator.CrossFade(stateName, transitionDuration, layer, normalizedTime);
    }

    public static void WrappedPlay(this Animator animator, int stateNameHash)
    {
        animator.Play(stateNameHash);
    }

    public static void WrappedPlay(this Animator animator, string stateName)
    {
        animator.Play(stateName);
    }

    public static void WrappedPlay(this Animator animator, int stateNameHash, int layer)
    {
        animator.Play(stateNameHash, layer);
    }

    public static void WrappedPlay(this Animator animator, string stateName, int layer)
    {
        animator.Play(stateName, layer);
    }

    public static void WrappedPlay(this Animator animator, int stateNameHash, int layer, float normalizedTime)
    {
        animator.Play(stateNameHash, layer, normalizedTime);
    }

    public static void WrappedPlay(this Animator animator, string stateName, int layer, float normalizedTime)
    {
        animator.Play(stateName, layer, normalizedTime);
    }
}