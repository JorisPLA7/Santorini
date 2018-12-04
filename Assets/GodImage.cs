using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GodImage : MonoBehaviour {
    public Sprite Apollo;
    public Sprite Athena;
    public Sprite Hermes;
	public Sprite Pan;
    public Sprite Artemis;
    public Sprite Minotaur;
    public Sprite Prometheus;
    public Sprite Atlas;
    public Sprite Demeter;
    public Sprite Hephaestus;

	// Update is called once per frame
	void Update () {
        if (GameManager.instance.currentPlayer.currentGodCard.name == "Apollo")
            {
                GetComponent<Image>().sprite = Apollo;
            }
        else if (GameManager.instance.currentPlayer.currentGodCard.name == "Athena") {
                GetComponent<Image>().sprite = Athena;
            }
        else if (GameManager.instance.currentPlayer.currentGodCard.name == "Hermes") {
                GetComponent<Image>().sprite = Hermes;
            }
        else if (GameManager.instance.currentPlayer.currentGodCard.name == "Pan") {
                GetComponent<Image>().sprite = Pan;
            }
        else if (GameManager.instance.currentPlayer.currentGodCard.name == "Artemis") {
                GetComponent<Image>().sprite = Artemis;
            }
        else if (GameManager.instance.currentPlayer.currentGodCard.name == "Minotaur") {
                GetComponent<Image>().sprite = Minotaur;
            }
        else if (GameManager.instance.currentPlayer.currentGodCard.name == "Prometheus")
            {
                GetComponent<Image>().sprite = Prometheus;
            }
        else if (GameManager.instance.currentPlayer.currentGodCard.name == "Atlas")
            {
                GetComponent<Image>().sprite = Atlas;
            }
        else if (GameManager.instance.currentPlayer.currentGodCard.name == "Hephaestus")
            {
                GetComponent<Image>().sprite = Hephaestus;
            }
        else if (GameManager.instance.currentPlayer.currentGodCard.name == "Demeter")
            {
                GetComponent<Image>().sprite = Demeter;
            }
	}
}
