/***********************************************************************************
 * Script Name: WorkerSelector
 * Description: 
 **********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class WorkerSelector : MonoBehaviour {

    public Camera GameCamera;

    private PlayerWorker selectedWorker;



    /***********************************************************************************
    * Function Name: Start
    * Description: Used for initialization.
    **********************************************************************************/
    void Start () {
        enabled = false;
        selectedWorker = null;
	}


    /***********************************************************************************
    * Function Name: Update
    * Description: Called once per frame.
    **********************************************************************************/
    void Update()
    {
        Ray ray = GameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        //Raycast will only hit player 1
        //TODO: Add functionality for player 2
        if (Physics.Raycast(ray, out hitInfo, 25.0f, LayerMask.GetMask("Worker")))
        {
            //If worker is clicked, "select" it and indicate it with a material change.
            if (Input.GetMouseButtonDown(0))
            {
                //TODO: ADD A CONDITIONAL FOR PROMETHEUS -  workers can choose to build, move(but cannot move up), then build again
                PlayerWorker tempWorker = hitInfo.collider.gameObject.GetComponent<PlayerWorker>();
                //UpdateCamera(selectedWorker);

                //Check if worker belongs to player
                if (GameManager.instance.WorkerCurrentPlayer(tempWorker))
                {
                    selectedWorker = tempWorker;
                    GameManager.instance.SelectWorker(selectedWorker);
                    selectedWorker.owner.currentGodCard.ExitState();
                }
            }
        }

    }

    /***********************************************************************************
    * Function Name: EnterState
    * Description: Called when worker selection state is entered
    **********************************************************************************/
    public void EnterState()
    {
        //added by Brian for ActionPhase UI
        GameManager.instance.currentPhase = GameManager.ActionPhase.Select;

        enabled = true;
    }

    /***********************************************************************************
  * Function Name: ExitState
  * Description: Called when worker selection exited -> tile selector state for movement/building
  **********************************************************************************/
    public void ExitState()
    {
        enabled = false;
        selectedWorker.owner.currentGodCard.EnterMoveState(selectedWorker);
        
    }

    //BRIAN'S FUNCTION FOR PROMETHEUS
    /***********************************************************************************
  * Function Name: ExitState
  * Description: ExitState for Prometheus, he can choose to 
  **********************************************************************************/
  /*
    private void ExitStatePrometheus(PlayerWorker worker)
        {
            enabled = false;
            TileSelector tileSelect = GetComponent<TileSelector>();
            tileSelect.EnterBuildState(worker);

        }
 */

    /***********************************************************************************
* Function Name: GetSelectedWorker
* Description: Gets the currently selected worker
**********************************************************************************/
    public PlayerWorker GetSelectedWorker()
    {
        return selectedWorker;
    }

    /***********************************************************************************
    * Function Name: ExitState
    * Description: Contextually shifts the view to that of select worker.
    *   (added by Chris)
    **********************************************************************************/
    // public void UpdateCamera(PlayerWorker CurrWorker) 
    /*{
        GameObject CameraSelector = GameObject.Find("CameraController");
        Debug.Log("Selected Worker Name: " + CurrWorker.name);

        // Contextually swith the camera to the 1st person view of the 
        // selected player 
        if (CurrWorker.name == "Player1-Worker Male") {
            // Player 1 Male view
            CameraSelector.GetComponent<CameraController>().ShowP1MaleCamera();
        } else if (CurrWorker.name == "Player1-Worker Female") {
            // Player 1 Female view
            CameraSelector.GetComponent<CameraController>().ShowP1FemaleCamera();
        } else if (CurrWorker.name == "Player2-Worker Male") {
            // Player 2 Male view
            CameraSelector.GetComponent<CameraController>().ShowP2MaleCamera();
        } else {
            // Player 2 Female view
            CameraSelector.GetComponent<CameraController>().ShowP1FemaleCamera();
        }
    }*/

}
