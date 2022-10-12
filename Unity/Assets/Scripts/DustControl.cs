using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StarterAssets;
/**
 * Este archivo controla las partículas que le salen de los pies al personaje
 */
[RequireComponent(typeof(ParticleSystem))]
public class DustControl : MonoBehaviour
{
    public ThirdPersonController tpc;
    public float maxRate;
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
            em.rateOverTime  =tpc._speed * maxRate;
        }
    }
}
