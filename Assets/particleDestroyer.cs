using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class particleDestroyer : MonoBehaviour {
    public ParticleSystem particleOn;
    private float time= 2f;
    void Update()
        {

        }

   public void destroyParticle()
        {
            Destroy(particleOn,time);
        }

}
