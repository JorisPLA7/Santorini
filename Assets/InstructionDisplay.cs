using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InstructionDisplay : MonoBehaviour {

    public GodInstructions Apollo;
    public GodInstructions Athena;
    public GodInstructions Hermes;
	public GodInstructions Pan;
    public GodInstructions Artemis;
    public GodInstructions Minotaur;
    public GodInstructions Prometheus;
    public GodInstructions Atlas;
    public GodInstructions Demeter;
    public GodInstructions Hephaestus;
    public Text InstructionText;
	// Update is called once per frame
	void Update () {
        if (GameManager.instance.currentPlayer.currentGodCard.name == "Apollo")
            {
                InstructionText.text = Apollo.instructionsText;
            }
        else if (GameManager.instance.currentPlayer.currentGodCard.name == "Athena") {
               InstructionText.text = Athena.instructionsText;
            }
        else if (GameManager.instance.currentPlayer.currentGodCard.name == "Hermes") {
                InstructionText.text = Hermes.instructionsText;
            }
        else if (GameManager.instance.currentPlayer.currentGodCard.name == "Pan") {
                InstructionText.text = Pan.instructionsText;
            }
        else if (GameManager.instance.currentPlayer.currentGodCard.name == "Artemis") {
               InstructionText.text = Artemis.instructionsText;
            }
        else if (GameManager.instance.currentPlayer.currentGodCard.name == "Minotaur") {
                InstructionText.text = Minotaur.instructionsText;
            }
        else if (GameManager.instance.currentPlayer.currentGodCard.name == "Prometheus")
            {
                InstructionText.text = Prometheus.instructionsText;
            }
        else if (GameManager.instance.currentPlayer.currentGodCard.name == "Atlas")
            {
               InstructionText.text = Atlas.instructionsText;
            }
        else if (GameManager.instance.currentPlayer.currentGodCard.name == "Hephaestus")
            {
               InstructionText.text = Hephaestus.instructionsText;
            }
        else if (GameManager.instance.currentPlayer.currentGodCard.name == "Demeter")
            {
                InstructionText.text = Demeter.instructionsText;
            }
	}
}
