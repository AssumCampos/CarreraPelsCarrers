using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEditor;
//using UnityEditor.SceneManagement;
using UnityEngine;
using Random = UnityEngine.Random;
using Vector3 = UnityEngine.Vector3;

[ExecuteInEditMode]
public class GenerateTrash : MonoBehaviour
{
    public int numBasuras;
    public GameObject trash;
    private int _basurasGeneradas;
    private bool _creaBasura;
    private List<Vector3> basuras = new List<Vector3>();
    public int tamMapa; 
    private float randomX, randomZ;


    public void Generate()
    {
        Delete();
        
        randomX = 0.0f;
        randomZ = 0.0f;
        _creaBasura = true;
        _basurasGeneradas = 0;
        
        while (_basurasGeneradas <= numBasuras)
        {
            randomX = Random.Range(0, tamMapa);
            randomZ = Random.Range(0, tamMapa);
            Vector3 ranPos = new Vector3(randomX-100, 30, 100-randomZ);
            if (!Physics.Raycast(ranPos, Vector3.down, 40, LayerMask.GetMask("FloorDecoration")) && 
                Physics.Raycast(ranPos, Vector3.down, 40, LayerMask.GetMask("Floor")))
            {
                
                _creaBasura = true;
                ranPos.y = 0;
                int i = 0;
                int randDist = Random.Range(10, 15);
                while (i < basuras.Count && _creaBasura)
                {
                    if (Vector3.Distance(ranPos, basuras[i]) < randDist)
                    {
                        _creaBasura = false;
                    }

                    i++;
                }

                if (_creaBasura)
                {
#if UNITY_EDITOR
                    GameObject g = PrefabUtility.InstantiatePrefab(trash, transform) as GameObject;
#else
                    GameObject g = Instantiate(trash, transform);
#endif
                    g.transform.position = ranPos;
                    g.transform.parent = transform;
                    basuras.Add(ranPos);
                    _basurasGeneradas++;
                }
                
            }
        }
    }

    public void Delete()
    {
        basuras.Clear();
        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        
    }
}
