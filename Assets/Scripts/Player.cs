using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public enum GodCard { apollo, artemis, athena, atlas, demeter, hephaestus, hermes, minotaur, pan, prometheus };

public class Player : MonoBehaviour {

    public List<PlayerWorker> workers;                      //Player's workers
    public bool isSettingUp;
    public int playerId;

    public GodCard currentGodCard;                          //Player's God Card
   

    //stats
    public int numMovesPerTurn;
    public int levelChangeDuringTurn;          //0 for moving from ground to ground, //1 for going up one level, //-1 for going down // -2 etc

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
