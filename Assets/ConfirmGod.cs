using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ConfirmGod : MonoBehaviour {

	// Use this for initialization
    public GodCard Apollo;
    public Button button;
    public AudioSource sound;
    public void SelectedApollo(){
            PlaySoundInterval();
            if(Settings.playerOneChoice==null){         //if player one didn't pick yet
                    Settings.playerOneChoice=Apollo;
                    GodSelection.instance.oneDone=true;
                    button.interactable=false;
                }
            else{ 
                    Debug.Log("Inside p2");
                    Settings.playerTwoChoice=Apollo;
                    GodSelection.instance.twoDone=true;
                    button.interactable=false;
                }
            }

    void PlaySoundInterval()
        {
            sound.time = 0;
            sound.Play();
            sound.SetScheduledEndTime(AudioSettings.dspTime+(7.0f-0.0f));
        }

	

}
