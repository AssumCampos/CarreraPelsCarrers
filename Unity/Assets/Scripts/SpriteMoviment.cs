using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteMoviment : MonoBehaviour
{
    public float factor = 1.0f;
    public bool meta;
    private Transform flecha;
    private Camera camera;

    private void Start()
    {
        camera = GameObject.FindGameObjectWithTag("camSprites").GetComponent<Camera>();
        flecha = GameObject.FindGameObjectWithTag("characterSprite").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 d = transform.position - flecha.position;
        Vector3 d_n = d.normalized;
        d_n.y = 0.3f;
    
        float dif = Mathf.Min(d.magnitude, camera.orthographicSize*0.75f);
        float d_mag = d.magnitude;
        
        // escalado de pequeño a grande y al reves
        if (transform.childCount > 0)
        {
            if (!meta)
            {
                float invLerp = Mathf.InverseLerp(50, 300, d_mag)/2;
                transform.GetChild(0).localScale = new Vector3(1.0f-invLerp,1.0f-invLerp,1.0f-invLerp) * camera.orthographicSize/8.0f ;
                transform.GetChild(0).position = flecha.position + (d_n * dif) * factor;
                transform.GetChild(0).position = new Vector3(transform.GetChild(0).position.x, 29, transform.GetChild(0).position.z);
                
            }
            else
            {
                transform.GetChild(0).localScale = new Vector3(1.0f,1.0f,1.0f) * camera.orthographicSize/7.0f ;
                transform.GetChild(0).position = flecha.position + (d_n * dif) * factor;
            }
        }
    }
}
