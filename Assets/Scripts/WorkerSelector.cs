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


    /***********************************************************************************
    * Function Name: Start
    * Description: Used for initialization.
    **********************************************************************************/
    void Start () {
        enabled = false;
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
            //REMOVE LATER: If mouse hovers over a worker, note it in the console
            Debug.Log("Mouse over worker " + hitInfo.collider.gameObject.name);

            //If worker is clicked, "select" it and indicate it with a material change.
            if (Input.GetMouseButtonDown(0))
            {
                //REMOVE LATER: Verify correct object is being clicked.
                Debug.Log("Worker " + hitInfo.collider.gameObject.name + "clicked!");

                PlayerWorker selectedWorker = hitInfo.collider.gameObject.GetComponent<PlayerWorker>();

                //Check if worker belongs to player
                if (GameManager.instance.WorkerCurrentPlayer(selectedWorker))
                {
                    GameManager.instance.SelectWorker(selectedWorker);
                    ExitState(selectedWorker);

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
        enabled = true;
    }

    /***********************************************************************************
  * Function Name: ExitState
  * Description: Called when worker selection exited -> tile selector state for movement/building
  **********************************************************************************/
    private void ExitState(PlayerWorker worker)
    {
        enabled = false;
        TileSelector tileSelect = GetComponent<TileSelector>();
        tileSelect.EnterState(worker);
    }


}
