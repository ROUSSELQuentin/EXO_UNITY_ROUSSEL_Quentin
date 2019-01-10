using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CanEditMultipleObjects, CustomEditor(typeof(Transform))]

public class ResetTransform2D : Editor
{
    public override void OnInspectorGUI()
    {
        Transform transform = (Transform)target;

        Vector3 position = EditorGUILayout.Vector2Field("Position", transform.localPosition);
        Vector3 rotation = new Vector3(0, 0, EditorGUILayout.FloatField("Rotation", transform.localEulerAngles.z));
        Vector3 scale = EditorGUILayout.Vector2Field("Scale", transform.localScale);

        if (GUI.changed)
        {
            Undo.RecordObject(transform, "Transform Changed");
            transform.localPosition = position;
            transform.localEulerAngles = rotation;
            transform.localScale = scale;
        }

        EditorGUILayout.Space();
        EditorGUILayout.Space();

        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button("Reset Position"))
        {
            resetPos2D();
        }

        if (GUILayout.Button("Reset Rotation"))
        {
            resetRot2D();
        }

        if (GUILayout.Button("Reset Scale"))
        {
            resetScale2D();
        }

        EditorGUILayout.EndHorizontal();
    }

    void resetPos2D()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            obj.transform.position = Vector3.zero;
        }
    }

    void resetRot2D()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            obj.transform.rotation = Quaternion.identity;
        }
    }

    void resetScale2D()
    {
        foreach (GameObject obj in Selection.gameObjects)
        {
            obj.transform.localScale = new Vector2(1, 1);
        }
    }
}
