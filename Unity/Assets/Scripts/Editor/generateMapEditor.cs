using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CustomEditor(typeof(generateMap))]
/**
 * Botones para generar el mapa
 */
public class generateMapEditor : Editor
{
    public override void OnInspectorGUI()
    {
        generateMap map = (generateMap) target;
        DrawDefaultInspector();
        if(GUILayout.Button("Generate Map"))
            map.Generate();
        if(GUILayout.Button("Delete Map"))
            map.DeleteMap();
    }
}