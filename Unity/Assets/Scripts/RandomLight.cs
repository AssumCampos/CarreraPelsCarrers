using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomLight : MonoBehaviour
{
    public GameObject[] lights;

    public Material[] skyboxs;
    // Start is called before the first frame update
    void Start()
    {
        var index = Random.Range(0, lights.Length);
        GameObject lightSelected = lights[index];
        lightSelected.SetActive(true);
        //RenderSettings.skybox = skyboxs[index];
        foreach (var l in lights)
        {
            if (l != lightSelected)
            {
                l.SetActive(false);
            }
        }
    }
}
