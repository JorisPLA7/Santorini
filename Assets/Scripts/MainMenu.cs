/***********************************************************************************
 * Script Name: MainMenu
 * Description: 
 **********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour {


    /***********************************************************************************
    * Function Name: PlayGame
    * Description: 
    **********************************************************************************/
    public void PlayGame() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame() {
        Application.Quit();
        }


}
