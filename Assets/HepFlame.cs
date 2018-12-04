using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HepFlame : MonoBehaviour {
    private float time= 2f;
    private static HepFlame hepFlame;
    public GameObject hepFlameObject;
	// Use this for initialization
    public ParticleSystem FlameEffect;
    public static HepFlame Instance()
        {
        if (!hepFlame)
            {
                hepFlame = FindObjectOfType(typeof(HepFlame)) as HepFlame;
            if (!hepFlame)
                {
                    Debug.LogError("No MinotaurBlood script");
                }
            }
            return hepFlame;
        }
	
    public void PlayEffectOnce(Vector3 position) {
        if (FlameEffect == null) { return ; }
        ParticleSystem ps = Instantiate(FlameEffect, position, Quaternion.identity) as ParticleSystem;
        Destroy(ps,time);
    }
}
