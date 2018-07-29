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
		string sceneFile;
		AnimatorStateAttribute attribute = (AnimatorStateAttribute)base.attribute;
		List<string> sceneNames = new List<string>();

		for (int i = 0; i < EditorBuildSettings.scenes.Length; i++)
		{

			if (EditorBuildSettings.scenes[i].enabled)
			{

				sceneFile = EditorBuildSettings.scenes[i].path.Substring(EditorBuildSettings.scenes[i].path.LastIndexOf("/") + 1);
				//sceneNames.Add(sceneFile.Replace(SCENE_EXTENSION, string.Empty));
			}
		}

		if (sceneNames.Count == 0)
		{

			//EditorGUI.LabelField(position, label.text, NOSCENE_TIP);

		}
		else
		{

			for (int i = 0; i < sceneNames.Count; i++)
			{

				if (sceneNames[i] == property.stringValue)
				{
					attribute.selected = i;
					break;
				}
			}

			attribute.selected = EditorGUI.Popup(position, label.text, attribute.selected, sceneNames.ToArray());
			property.stringValue = sceneNames[attribute.selected];
		}
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