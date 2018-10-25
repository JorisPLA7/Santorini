using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CurrentPlayerUI : MonoBehaviour {
    GameManager instance;
    private int currentPlayerId;
	// Use this for initialization
	void Start () {
        instance = FindObjectOfType<GameManager>();
        currentPlayerId = instance.currentPlayer.playerId;
        Debug.Log(currentPlayerId );
        GetComponent<Text>().text = "Current Turn: Player " + currentPlayerId;

    }
	
	// Update is called once per frame
	void Update () {
        if (instance.currentMode==GameManager.Mode.SettingUp) {
                currentPlayerId = instance.currentPlayer.playerId;
                GetComponent<Text>().text = "Setting up Player " + currentPlayerId;
        }
        else if (instance.currentMode == GameManager.Mode.Playing)
        {
            currentPlayerId = instance.currentPlayer.playerId;
            GetComponent<Text>().text = "Current Turn: Player " + instance.currentPlayer.playerId;
        }

    }
}
