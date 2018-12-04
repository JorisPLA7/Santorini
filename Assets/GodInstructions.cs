using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GodInstructions : MonoBehaviour {
    public Text instructions;
	// Use this for initialization
    public string instructionsText;

    public void DisplayInstructions(){
            instructions.text= instructionsText;

            }
}
