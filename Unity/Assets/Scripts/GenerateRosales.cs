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
public class GenerateRosales : MonoBehaviour
{
    public int numRosales;
    public GameObject roses;
    private int _rosalesGenerados;
    private bool _creaRosal;
    private List<Vector3> rosales  = new List<Vector3>();
    public int tamMapa; 
    private float randomX, randomZ;


    public void Generate()
    {
        Delete();
        
        randomX = 0.0f;
        randomZ = 0.0f;
        _creaRosal = true;
        _rosalesGenerados = 0;
        
        while (_rosalesGenerados <= numRosales)
        {
            randomX = Random.Range(0, tamMapa);
            randomZ = Random.Range(0, tamMapa);
            Vector3 ranPos = new Vector3(randomX-100, 30, 100-randomZ);
            if (!Physics.Raycast(ranPos, Vector3.down, 40, LayerMask.GetMask("FloorDecoration")) && 
                Physics.Raycast(ranPos, Vector3.down, 40, LayerMask.GetMask("Floor")))
            {
                _creaRosal = true;
                CalculateExtents(ranPos);
                ranPos.y = 0;
                int i = 0;
                int randDist = Random.Range(30, 40);
                while (_creaRosal && i < rosales.Count )
                {
                    if (Vector3.Distance(ranPos, rosales[i]) < randDist)
                    {
                        _creaRosal = false;
                    }

                    i++;
                }

                if (_creaRosal)
                {
#if UNITY_EDITOR
                    GameObject g = PrefabUtility.InstantiatePrefab(roses, transform) as GameObject;
#else
                    GameObject g = Instantiate(roses, transform);
#endif
                    g.transform.position = ranPos;
                    g.transform.parent = transform;
                    rosales.Add(ranPos);
                    _rosalesGenerados++;
                }
            }
        }
    }

    public void Delete()
    {
        rosales.Clear();
        while (transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
        
    }

    public void CalculateExtents(Vector3 pos)
    {
        BoxCollider rose = roses.GetComponent<BoxCollider>();
        Vector3 offset = rose.size;
        Vector3 newPos = new Vector3(pos.x - offset.x*5.0f, pos.y, pos.z + offset.z*5.0f); // Esquina de arriba izquierda

        if (Physics.Raycast(newPos, Vector3.down, 40, LayerMask.GetMask("FloorDecoration")) ||
            !Physics.Raycast(newPos, Vector3.down, 40, LayerMask.GetMask("Floor")))
        {
            _creaRosal = false;
        }
        
        newPos = new Vector3(pos.x - offset.x*5.0f, pos.y, pos.z - offset.z*5.0f);        // Esquina de abajo izquierda
        
        if(_creaRosal && 
           (Physics.Raycast(newPos, Vector3.down, 40, LayerMask.GetMask("FloorDecoration")) ||
           !Physics.Raycast(newPos, Vector3.down, 40, LayerMask.GetMask("Floor"))))
        {
            _creaRosal = false;
        }
        
        newPos = new Vector3(pos.x + offset.x*5.0f, pos.y, pos.z + offset.z*5.0f);        // Esquina de arriba derecha
        
        if(_creaRosal && 
           (Physics.Raycast(newPos, Vector3.down, 40, LayerMask.GetMask("FloorDecoration")) ||
           !Physics.Raycast(newPos, Vector3.down, 40, LayerMask.GetMask("Floor"))))
        {
            _creaRosal = false;
        }
        
        newPos = new Vector3(pos.x + offset.x*5.0f, pos.y, pos.z - offset.z*5.0f);        // Esquina de abajo derecha
        
        if(_creaRosal && 
           (Physics.Raycast(newPos, Vector3.down, 40, LayerMask.GetMask("FloorDecoration")) ||
           !Physics.Raycast(newPos, Vector3.down, 40, LayerMask.GetMask("Floor"))))
        {
            _creaRosal = false;
        }
        
    }
}

