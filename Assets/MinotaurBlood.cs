using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinotaurBlood : MonoBehaviour {

    private static MinotaurBlood minotaurBlood;
    public ParticleSystem BloodSprayEffect;
    private float time= 2f;
    public GameObject minotaurbloodObject;
    public static MinotaurBlood Instance()
        {
        if (!minotaurBlood)
            {
                minotaurBlood = FindObjectOfType(typeof(MinotaurBlood)) as MinotaurBlood;
            if (!minotaurBlood)
                {
                    Debug.LogError("No MinotaurBlood script");
                }
            }
            return minotaurBlood;
        }

    public void PlayEffectOnce(Vector3 position) {
        if (BloodSprayEffect == null) { return ; }
        ParticleSystem ps = Instantiate(BloodSprayEffect, position, Quaternion.identity) as ParticleSystem;
         Destroy(ps,time);
    }
}
