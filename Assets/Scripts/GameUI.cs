/***********************************************************************************
 * Script Name: GameUI
 * Description: 
 **********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour {

    /***********************************************************************************
    * Script Name: SelectWorker
    * Description: Show worker selection.
    **********************************************************************************/
    public void SelectWorker(PlayerWorker worker)
    {
        worker.SelectWorker();
    }

    /***********************************************************************************
    * Script Name: ResetSelection
    * Description: Reset worker selection.
    **********************************************************************************/
    public void ResetSelection(PlayerWorker worker)
    {
        worker.DeselectWorker();
    }


    /***********************************************************************************
    * Script Name: PlaceWorker
    * Description: Move a worker to a space
    **********************************************************************************/
    public void PlaceWorker(PlayerWorker worker, Space selectedSpace)
    {
        //TODO: Assert error if worker or space is null
        //Vector3 workerPosition = new Vector3(0.0f, 0.0f, 0.0f);
        //workerPosition.x = selectedSpace.transform.position.x;
        //workerPosition.z = selectedSpace.transform.position.z;

        // Transform to account for space currentFloorLevel
        if (selectedSpace.GetSpaceFloorLevel() == FloorLevel.ground)
        {
            worker.transform.position = new Vector3(selectedSpace.spacePosition.x, (float)0.0, selectedSpace.spacePosition.z);

        }
        else if (selectedSpace.GetSpaceFloorLevel() == FloorLevel.first)
        {
            worker.transform.position = new Vector3(selectedSpace.spacePosition.x,
                                                    L1_BuildingController.L1_Building_Height, selectedSpace.spacePosition.z);
        }
        else if (selectedSpace.GetSpaceFloorLevel() == FloorLevel.second)
        {
            worker.transform.position = new Vector3(selectedSpace.spacePosition.x,
                                                    L2_BuildingController.L2_Building_Height, selectedSpace.spacePosition.z);
        }
        else if (selectedSpace.GetSpaceFloorLevel() == FloorLevel.third)
        {
            worker.transform.position = new Vector3(selectedSpace.spacePosition.x,
                                                    L3_BuildingController.L3_Building_Height, selectedSpace.spacePosition.z);
        } else {
                // Notify player via UI that the move is invalid, as the space is blocked by tower. 
        }

    }


    /***********************************************************************************
    * Function Name: Start
    * Description: Used for initialization. 
    **********************************************************************************/
    /*
    void Start () {
        selectedSpace = null;
        spaceHighlight = GameObject.Find("TileSelector");
        spaceHighlight.SetActive(false);
        selectedSpacePosition = new Vector3(0.0f, 0.0f, 0.0f);
	}
    */


    /***********************************************************************************
    * Function Name: Update
    * Description: Called once per frame.
    **********************************************************************************/
    /*
    void Update () {
        // Highlight Selected Space///////////////////////////////////////////
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        //Raycast will only hit spaces
        if (Physics.Raycast(ray, out hitInfo, 25.0f, LayerMask.GetMask("Tile")))
        {
            //If it hits a space, select it
            selectedSpace = hitInfo.collider.gameObject;
            selectedSpacePosition.x = selectedSpace.transform.position.x;
            selectedSpacePosition.z = selectedSpace.transform.position.z;
        } else
        {
            selectedSpace = null;
            spaceHighlight.SetActive(false);
        }

        //Highlight the selected space if applicable
        if(selectedSpace)
        {
            spaceHighlight.transform.position = selectedSpacePosition;
            spaceHighlight.SetActive(true);
        }
    }
    */

}
