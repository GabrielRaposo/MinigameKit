using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SpriteColorReplacement))]
public class SpriteColorReplacementEditor : Editor 
{
	public override void OnInspectorGUI ()
	{
		EditorGUILayout.HelpBox ("Lembrando que aguenta 10 cores.", MessageType.Warning);
		DrawDefaultInspector();

		var scr = (SpriteColorReplacement)target;

		if (GUILayout.Button ("Apply")) 
		{
			scr.Apply ();
			Debug.Log ("fuck");
		}
	}
}
