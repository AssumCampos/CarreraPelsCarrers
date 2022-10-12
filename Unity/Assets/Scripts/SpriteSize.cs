using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteSize : MonoBehaviour
{
    public Camera camera;
    private Vector3 scaleChange;
    // Start is called before the first frame update
    void Start()
    {
        camera = GameObject.FindGameObjectWithTag("camSprites").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.localScale = new Vector3(1,1,1) * camera.orthographicSize/13 ;
    }
}
