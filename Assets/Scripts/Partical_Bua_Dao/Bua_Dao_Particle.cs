using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bua_Dao_Particle : MonoBehaviour
{
    public ParticleSystem particle;

    public void StartPartical()
    {
        particle.Play();
    }
}
