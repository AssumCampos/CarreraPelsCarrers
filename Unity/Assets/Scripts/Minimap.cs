using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    public float smallSize, bigSize;
    public Transform playerTransform;
    private Vector3 targetPos;
    private IEnumerator zI, zO;
    private Camera cam;

    private bool isDown, isUp;
    // Start is called before the first frame update
    void Start()
    {
        
        isDown = false;
        isUp = false;
        transform.rotation = Quaternion.Euler(90, 0, 0);
        cam = transform.GetComponent<Camera>();
        cam.orthographicSize = smallSize;
    }

    // Update is called once per frame
    void Update()
    {
        targetPos = playerTransform.position;
        targetPos.y += 30;

        transform.position = targetPos;
        transform.LookAt(targetPos, Vector3.left);
        zO = zoomOut(cam.orthographicSize, bigSize, 0.5f);
        zI = zoomIn(cam.orthographicSize, smallSize, 0.5f);
        if (Input.GetButtonDown("MapZoom"))
        {
            isDown = true;
            StartCoroutine(zO);
            StopCoroutine(zI);
        }
        if(Input.GetButtonUp("MapZoom"))
        {
            isUp = true;
            StartCoroutine(zI);
            StopCoroutine(zO);
        }
        
    }

    private IEnumerator zoomOut(float oldSize, float newSize, float time)
    {
        isUp = false;
        float elapsed = 0;
        while (elapsed <= time && !isUp)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / time);
        
            cam.orthographicSize = Mathf.SmoothStep(oldSize, newSize, t);

            yield return null;
        }
        
    }
    
    private IEnumerator zoomIn(float oldSize, float newSize, float time)
    {
        isDown = false;
        float elapsed = 0;
        while (elapsed <= time && !isDown)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / time);
        
            cam.orthographicSize = Mathf.SmoothStep(oldSize, newSize, t);

            yield return null;
        }
    }

}
