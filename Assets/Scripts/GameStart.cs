/*********************************************************************************** * Script Name: TileSelector * Description:  **********************************************************************************/using System.Collections;using System.Collections.Generic;using System;using UnityEngine;public class GameStart : MonoBehaviour {    public event Action GameStarted;    public GameObject button;




    /***********************************************************************************    * Function Name: Start    * Description: Used for intialization.    **********************************************************************************/
    void Start () {        button = GameObject.Find("Button");	}


    /***********************************************************************************    * Function Name: Update    * Description: Called once per frame.    **********************************************************************************/
    void Update () {			}


    /***********************************************************************************    * Function Name: startGame    * Description:    **********************************************************************************/
    public void startGame()    {        Debug.Log("Disable: " + button.name);        button.SetActive(false);        GameStarted();    }}