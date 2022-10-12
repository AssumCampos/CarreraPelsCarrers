using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(GenerateRosales))]
/**
 * Botones para generar los rosales
 */
public class GenerateRosalesEditor : Editor
{
    public override void OnInspectorGUI()
    {
        GenerateRosales rosales = (GenerateRosales) target;
        DrawDefaultInspector();
        if(GUILayout.Button("Generate Roses"))
            rosales.Generate();
        if(GUILayout.Button("Delete Roses"))
            rosales.Delete();
    }
}