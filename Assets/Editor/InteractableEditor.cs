using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(Interactable))]
[CanEditMultipleObjects]
public class InteractableEditor : Editor
{
    SerializedProperty _radius;
    void OnEnable()
    {
        _radius = serializedObject.FindProperty("_radius");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        EditorGUILayout.PropertyField(_radius);
        (target as Interactable).ApplyRadius();
        serializedObject.ApplyModifiedProperties();
    }
}