using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public List<PlayerWorker> workers;                      //Player's workers
    public bool isSettingUp;
    public int playerId;
	// Use this for initialization
	void Start () {
        isSettingUp = true;             //all players need to set up
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
