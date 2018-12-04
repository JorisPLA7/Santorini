using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public  class ModeSelect : MonoBehaviour {

    
    
    public void GodlessMode() {
        Settings.selectedMode = Settings.Modes.godless;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 2);
        }

    public void GodMode() {
        Settings.selectedMode = Settings.Modes.gods;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);       //should load a God selection scene once made
        }
}
