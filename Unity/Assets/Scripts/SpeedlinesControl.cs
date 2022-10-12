using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;

[RequireComponent(typeof(ParticleSystem))]
public class SpeedlinesControl : MonoBehaviour
{
    public ThirdPersonController tpc;
    
    private ParticleSystem ps;
    
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        ParticleSystem.EmissionModule em = ps.emission;
        // Si el personaje no se mueve no se generan partículas
        if (tpc._input.move == Vector2.zero)
        {
            em.rateOverTime = 0;
        }
        else
        {
            em.rateOverTime  =100;
        }
    }
}
