using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEditor;
[ExecuteInEditMode]
public class generateMap : MonoBehaviour
{

    public int cols;
    public int rows;
    private Vector3 v = new Vector3(200, 0, 200);

    public GameObject[] prefabs2c;
    public GameObject[] prefabs3c;
    public GameObject[] prefabs4c;

    public void Generate()
    {

        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
            // Arriba esquina izquierda
            var index = Random.Range(0, prefabs2c.Length);
            GameObject g;
#if UNITY_EDITOR
            PrefabUtility.InstantiatePrefab(prefabs2c[index], transform);
#else
            Instantiate(prefabs2c[index], transform);
#endif
            // Arriba derecha
            index = Random.Range(0, prefabs2c.Length);
#if UNITY_EDITOR
            g = PrefabUtility.InstantiatePrefab(prefabs2c[index], transform) as GameObject;
#else
            g = Instantiate(prefabs2c[index], transform);
#endif           
            g.transform.Translate(v.x * (rows - 1), 0, 0);
            g.transform.Rotate(0,90,0);
            // abajo izquierda
            index = Random.Range(0,prefabs2c.Length);
#if UNITY_EDITOR
            g = PrefabUtility.InstantiatePrefab(prefabs2c[index], transform) as GameObject;
#else
            g = Instantiate(prefabs2c[index], transform);
#endif    
            g.transform.Translate(0,0,-v.z * (cols - 1));
            g.transform.Rotate(0,-90,0);
            // abajo derecha
            index = Random.Range(0,prefabs2c.Length);
#if UNITY_EDITOR
            g = PrefabUtility.InstantiatePrefab(prefabs2c[index], transform) as GameObject;
#else
            g = Instantiate(prefabs2c[index], transform);
#endif    
            g.transform.Translate(v.x * (rows - 1),0,-v.z * (cols - 1));
            g.transform.Rotate(0,180,0);
            for (int i = 0; i < cols; i++)
            {
                for (int j = 0; j < rows; j++)
                {
                    //GameObject g;
                    if (i == 0 && j != 0 && j != rows-1)
                    {
                        index = Random.Range(0,prefabs3c.Length);
                        Vector3 position = new Vector3( v.x * j ,0,0);
#if UNITY_EDITOR
                        g = PrefabUtility.InstantiatePrefab(prefabs3c[index], transform) as GameObject;
#else
                        g = Instantiate(prefabs3c[index], transform);
#endif
                        g.transform.Translate(position);
                        g.transform.Rotate(0,-90,0);
                    }
                    // CENTRO
                    // centro
                    else if (j != 0 && j != rows-1 && i != 0 && i != cols - 1)
                    {
                        index = Random.Range(0,(prefabs4c.Length));
                        int[] grades = {0,-90, 90,180};
                        var indexGrades = Random.Range(0, grades.Length );
#if UNITY_EDITOR
                        g = PrefabUtility.InstantiatePrefab(prefabs4c[index], transform) as GameObject;
#else
                        g = Instantiate(prefabs4c[index], transform);
#endif
                        g.transform.Translate(v.x * j,0,-v.z * i);
                        g.transform.Rotate(0,grades[indexGrades],0);
                    }
                    // derecha
                    else if (j == rows - 1 && i != 0 && i != cols - 1)
                    {
                        index = Random.Range(0, prefabs3c.Length);
#if UNITY_EDITOR
                        g = PrefabUtility.InstantiatePrefab(prefabs3c[index], transform) as GameObject;
#else
                        g = Instantiate(prefabs3c[index], transform);
#endif
                        g.transform.Translate(v.x * j,0,-v.z * i);
                        g.transform.Rotate(0,0,0);
                    }
                    // izquierda
                    else if (j == 0 && i != 0 && i != cols - 1)
                    {
                        index = Random.Range(0, prefabs3c.Length);
#if UNITY_EDITOR
                        g = PrefabUtility.InstantiatePrefab(prefabs3c[index], transform) as GameObject;
#else
                        g = Instantiate(prefabs3c[index], transform);
#endif
                        g.transform.Translate(0,0,-v.z * i);
                        g.transform.Rotate(0,180,0);
                    }
                    // Abajo medio
                    else if (i == cols - 1 && j != 0 && j != rows-1)
                    {
                        index = Random.Range(0,prefabs3c.Length);
#if UNITY_EDITOR
                        g = PrefabUtility.InstantiatePrefab(prefabs3c[index], transform) as GameObject;
#else
                        g = Instantiate(prefabs3c[index], transform);
#endif
                        g.transform.Translate(v.x * j,0,-v.z * i);
                        g.transform.Rotate(0,90,0);
                    }

                }
            }
    }

	public void DeleteMap()
	{
		while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
	}
}
