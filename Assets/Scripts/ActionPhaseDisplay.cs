using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ActionPhaseDisplay : MonoBehaviour {

    
    // Use this for initialization
    void Start ()
    {
        
    }
	
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.currentMode == GameManager.Mode.SettingUp)
        {
            GetComponent<Text>().text = "";
        }
        else if (GameManager.instance.currentPhase == GameManager.ActionPhase.Move)
        {
            GetComponent<Text>().text = "Action Phase: " + "Move";
        }
        else if (GameManager.instance.currentPhase == GameManager.ActionPhase.Build)
        {
            GetComponent<Text>().text = "Action Phase: " + "Build";
        }
        else if (GameManager.instance.currentPhase == GameManager.ActionPhase.Select)
        {
            GetComponent<Text>().text = "Action Phase: " + "Select Worker";
        }
    }
}
