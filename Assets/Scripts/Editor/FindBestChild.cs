using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(AnimatorStateSpawnFX))]
public class FindBestChild : Editor
{
    private AnimatorStateSpawnFX mAnimatorStateSpawnFX;
    public void OnEnable()
    {
        mAnimatorStateSpawnFX = (AnimatorStateSpawnFX)target;
    }

    public override void OnInspectorGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Label(" FindAttachName ");
        GUILayout.BeginHorizontal();
        GUILayout.Label("Speed : ");
        mAnimatorStateSpawnFX.AttachTransformName = EditorGUILayout.TextField(mAnimatorStateSpawnFX.AttachTransformName, GUILayout.Width(100));
        GUILayout.EndHorizontal();
        GUILayout.EndVertical();
    }
}
