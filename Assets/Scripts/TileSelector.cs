/***********************************************************************************
 * Script Name: TileSelector
 * Description: 
 **********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum State {move, build};


public class TileSelector : MonoBehaviour {

    public State state;

    public GameObject availablePrefab;

    public Camera gameCamera;
    public GameObject spaceHighlight;
    private Vector3 highlightPosition;
    private Space spaceToHighlight;
    private PlayerWorker selectedWorker;
    private Space selectedSpace;

    private List<Space> availableSpaces;
    private List<GameObject> availableHighlight;

    public AudioSource buildSound;                  //to add a building sound after click
    public AudioSource confirmSound;


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
            highlightPosition.y = 0.1f + spaceToHighlight.GetHeight();
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

                selectedSpace = spaceToHighlight;

                //Remove highlights
                RemoveHighlights();

                if (state == State.move)
                {
                    confirmSound.Play();
                    //GameManager.instance.currentPlayer.currentGodCard.PlaceWorker(selectedWorker, selectedSpace);
                    //TODO
                    //TRYING OUT NEW MOVEMENT FUNCTION
                    GameManager.instance.StartMovementPhase(selectedWorker, selectedSpace);
                    selectedWorker.owner.currentGodCard.ExitMoveState();
                } else if (state == State.build)
                {
                    PlayHammerSound(0, 2);                              
                    selectedWorker.owner.currentGodCard.BuildBlock();
                    selectedWorker.owner.currentGodCard.ExitBuildState();
                    }
            }
        }
        else
        {
            //If not, disable the highlighter
            spaceHighlight.SetActive(false);
        }

    }


    /***********************************************************************************
    * Function Name: EnterMoveState
    * Description: Enter Move State
    **********************************************************************************/
    public void EnterMoveState(PlayerWorker worker)
    {
        //added by Brian for ActionPhase UI
        GameManager.instance.currentPhase = GameManager.ActionPhase.Move;

        //Set state to move
        state = State.move;

        selectedWorker = worker;
        this.enabled = true;

 
        availableSpaces = GameManager.instance.GetPossibleMovementsForWorker(worker);
        //Highlight availableSpaces
        highlightAvailableSpaces();

    }

    /***********************************************************************************
    * Function Name: EnterBuildState
    * Description: Enter Build State
    **********************************************************************************/
    public void EnterBuildState(PlayerWorker worker)
    {
        //added by Brian for ActionPhase UI
        GameManager.instance.currentPhase = GameManager.ActionPhase.Build;

        //Set state to build
        state = State.build;

        selectedWorker = worker;
        this.enabled = true;

        availableSpaces = selectedWorker.owner.currentGodCard.GetAvailableBuilds(worker);

        //Highlight availableSpaces
        highlightAvailableSpaces();

    }

    /***********************************************************************************
   * Function Name: ExitMoveState
   * Description: Exit Move State and Enter Build state
   **********************************************************************************/
    public void ExitMoveState()
    {
        //Reset space selection
        selectedSpace = null;

        //Check win condition
        if (selectedWorker.owner.currentGodCard.HasCurrentPlayerWon())
        {
            //Reset worker selection
            GameManager.instance.ResetSelection(selectedWorker);
            selectedWorker = null;

            //End Game
            GameManager.instance.EndGame();

        } else
        {
            //Enter build state
            selectedWorker.owner.currentGodCard.EnterBuildState(selectedWorker);
        }

    }

    /***********************************************************************************
    * Function Name: ExitBuildState
    * Description: Exit Build State and Enter Select Worker state for next player
    **********************************************************************************/
    public void ExitBuildState()
    {
        //Reset worker + space selection
        GameManager.instance.ResetSelection(selectedWorker);
        selectedWorker = null;
        selectedSpace = null;

        //End current player's turn
        GameManager.instance.EndTurn();

        //Enter worker selector state for other player
        WorkerSelector workerSelect = GetComponent<WorkerSelector>();
        workerSelect.EnterState();

    }


    /***********************************************************************************
    * Function Name: highlightAvailableSpaces
    * Description: Highlight available spaces to move to
    **********************************************************************************/
    private void highlightAvailableSpaces()
    {
        //Highlight spaces available to move to
        availableHighlight = new List<GameObject>();
        foreach (Space space in availableSpaces)
        {
            GameObject available;

            available = Instantiate(availablePrefab, new Vector3(space.transform.position.x, space.GetHeight() + 0.05f, space.transform.position.z), Quaternion.identity, gameObject.transform);
            availableHighlight.Add(available);
        }
    }

    /***********************************************************************************
    * Function Name: removeHighlights
    * Description: Remove available space and selector highlights
    **********************************************************************************/
    public void RemoveHighlights()
    {
        spaceHighlight.SetActive(false);
        this.enabled = false;

        //Remove available space highlights
        foreach (GameObject available in availableHighlight)
        {
            Destroy(available);
        }
    }

    /***********************************************************************************
    * Function Name: PlayBuildSound
    * Description: This Plays the Building Sound after user clicks
    **********************************************************************************/
    public void PlayHammerSound(float fromSeconds, float toSeconds)
        {
            buildSound.Play();
            buildSound.SetScheduledEndTime(AudioSettings.dspTime + (toSeconds - fromSeconds));
        }


    /***********************************************************************************
    * Function Name: GetSelectedWorker
    * Description: Gets the currently selected worker
    **********************************************************************************/
    public PlayerWorker GetSelectedWorker()
    {
        return selectedWorker;
    }


    /***********************************************************************************
    * Function Name: GetSelectedSpace
    * Description: Gets the currently selected worker
    **********************************************************************************/
    public Space GetSelectedSpace()
    {
        return selectedSpace;
    }
}
