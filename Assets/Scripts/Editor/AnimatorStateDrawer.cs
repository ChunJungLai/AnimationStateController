using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(AnimatorStateAttribute))]
public class AnimatorStateDrawer : PropertyDrawer
{
	private const float LINE_HEIGHT = 18f;
	private const float PADDING = 2f;
	static private float currentPropertyHeight = LINE_HEIGHT;

    public override void OnGUI(UnityEngine.Rect position, SerializedProperty property, UnityEngine.GUIContent label)
    {
        AnimatorStateAttribute attribute = (AnimatorStateAttribute)base.attribute;
        List<string> sceneNames = new List<string>();
        //Debug.Log(property.serializedObject.targetObject.name);
        // Debug.Log(attribute);
        //attribute.character = GameObject.Find("Package04_animChanger");
        //GameObject test = GameObject.Find(property.objectReferenceInstanceIDValue.ToString());
       // Debug.Log(attribute.character.name);
       Debug.Log(property.objectReferenceInstanceIDValue);
       attribute.selected = EditorGUI.Popup(position, label.text, attribute.selected, sceneNames.ToArray());
        //property.stringValue = sceneNames[0];
    }

	static private void DrawBackground(Rect position)
	{
		position.x -= PADDING; // go a little left
		position.width += (PADDING * 2); // and grow a little wider
		GUI.Box(position, "");
	}

	static private void DrawControls(Rect position, SerializedProperty property)
	{
		position.y += PADDING; // go a little down
		position.height = LINE_HEIGHT;
	}
}