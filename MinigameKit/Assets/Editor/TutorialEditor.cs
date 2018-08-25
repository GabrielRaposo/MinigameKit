using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

//[CustomEditor(typeof(TutorialObject))]
public class TutorialEditor : Editor {
    public override void OnInspectorGUI() {
        serializedObject.Update();

        var textStyle = new GUIStyle("TextArea");
        textStyle.wordWrap = true;
        var minigameName = serializedObject.FindProperty("minigameName");
        var codename = serializedObject.FindProperty("codename");
        var gameRules = serializedObject.FindProperty("gameRules");
        var controls = serializedObject.FindProperty("controls");
        var image = serializedObject.FindProperty("image");

        GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Minigame's name: ", EditorStyles.boldLabel);
                minigameName.stringValue = EditorGUILayout.TextField(minigameName.stringValue);
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Codename: ", EditorStyles.boldLabel);
                codename.stringValue = EditorGUILayout.TextField(codename.stringValue);
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
                image.objectReferenceValue = (Texture2D)EditorGUILayout.ObjectField("Preview image: ", image.objectReferenceValue, typeof(Texture2D), false);
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Minigame's rules: ");
                gameRules.stringValue = EditorGUILayout.TextArea(gameRules.stringValue, textStyle, GUILayout.Width(300), GUILayout.Height(150));
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Minigame's controls: ");
                controls.stringValue = EditorGUILayout.TextArea(controls.stringValue, textStyle, GUILayout.Width(300), GUILayout.Height(150));
            GUILayout.EndHorizontal();
        GUILayout.EndVertical();

        serializedObject.ApplyModifiedProperties();
    }
}
