using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(GenerateTrash))]
/**
 * Botones para generar la basura
 */
public class GenerateTrashEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GenerateTrash trash = (GenerateTrash) target;
        DrawDefaultInspector();
        if(GUILayout.Button("Generate Trash"))
            trash.Generate();
        if(GUILayout.Button("Delete Trash"))
            trash.Delete();
    }
}