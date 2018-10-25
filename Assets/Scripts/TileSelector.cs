/***********************************************************************************
 * Script Name: TileSelector
 * Description: 
 **********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class TileSelector : MonoBehaviour {

    public GameObject availablePrefab;

    public Camera gameCamera;
    public GameObject spaceHighlight;
    private Vector3 highlightPosition;
    private Space spaceToHighlight;
    private PlayerWorker selectedWorker;

    private List<Space> availableSpaces;
    private List<GameObject> availableHighlight;




    /***********************************************************************************
    * Function Name: Start
    * Description: Used for initialization.
    **********************************************************************************/
    void Start () {
        enabled = false;
        spaceHighlight = GameObject.Find("TileHighlight");
        spaceHighlight.SetActive(false);
        highlightPosition = new Vector3(0.0f, 0.1f, 0.0f);
    }


    /***********************************************************************************
    * Function Name: Update
    * Description: Called once per frame.
    **********************************************************************************/
    void Update () {
        // Highlight Selected Space///////////////////////////////////////////
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        //Raycast will only hit spaces
        if (Physics.Raycast(ray, out hitInfo, 25.0f, LayerMask.GetMask("Tile")))
        {
            //If it hits a space, highlight it
            spaceToHighlight = hitInfo.collider.gameObject.GetComponent<Space>();
            highlightPosition.x = spaceToHighlight.transform.position.x;
            highlightPosition.z = spaceToHighlight.transform.position.z;
            spaceHighlight.transform.position = highlightPosition;
            spaceHighlight.SetActive(true);

            //If highlighted space is clicked
            if (Input.GetMouseButtonDown(0))
            {
                if (!availableSpaces.Contains(spaceToHighlight))
                {
                    return;
                }

                Space selectedSpace = spaceToHighlight;
                //TODO: Implement check for available move/build locations
                //TODO: Implement moving/building conditional statements

                GameManager.instance.PlaceWorker(selectedWorker, selectedSpace);
                ExitState();
            }
        }
        else
        {
            //If not, disable the highlighter
            spaceHighlight.SetActive(false);
        }

    }

    /***********************************************************************************
    * Function Name: EnterState
    * Description: Enter Build/Move state from select worker state
    **********************************************************************************/
    public void EnterState(PlayerWorker worker)
    {
        selectedWorker = worker;
        this.enabled = true;

        //TODO: Implement valid move/build location list builder in GameManager and call it here
        availableSpaces = GameManager.instance.GetAvailableMoves(worker);

        //TODO: If a move/build is not available, cancel

        //Highlight spaces available to move to
        availableHighlight = new List<GameObject>();
        foreach (Space space in availableSpaces)
        {
            GameObject available;
            //TODO: Implement building logic for y axis (space.currentFloorLevel * buildingHeight)
            available = Instantiate(availablePrefab, new Vector3(space.transform.position.x, 0.0f, space.transform.position.z), Quaternion.identity, gameObject.transform);
            availableHighlight.Add(available);
        }
    }

    /***********************************************************************************
   * Function Name: ExitState
   * Description: Exit State and Enter Select Worker state for next player
   **********************************************************************************/
    public void ExitState()
    {
        spaceHighlight.SetActive(false);
        this.enabled = false;

        //Remove available space highlights
        foreach (GameObject available in availableHighlight)
        {
            Destroy(available);
        }

        //Reset worker selection
        GameManager.instance.ResetSelection(selectedWorker);
        selectedWorker = null;

        //End current player's turn
        GameManager.instance.EndTurn();

        //Enter worker selector state for other player
        WorkerSelector workerSelect = GetComponent<WorkerSelector>();
        workerSelect.EnterState();

    }

    /***********************************************************************************
      * Function Name: ColorValidator
      * Description: Changes selector color depending on whether the space represents a
      *   valid choice for a player move or build operation. 
      **********************************************************************************/
    // To be implemented
    // Move/Build Tile Selection Validation Logic ////
}
