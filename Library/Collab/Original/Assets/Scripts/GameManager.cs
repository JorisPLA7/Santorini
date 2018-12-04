/***********************************************************************************
 * Script Name: GameManager
 * Description: This script serves as the control center of the game. 
 ***********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    public enum ActionPhase { Move, Build };
    public static GameManager instance;

    public GameUI gameUI;
    public Player currentPlayer;
    public Player otherPlayer;
    public Player PlayerOne;
    public Player PlayerTwo;
    public enum Mode {SettingUp,Playing,Ending };
    public Mode currentMode;
    public ActionPhase currentPhase;
    public int Playerturn; // 1 or 2
    


    void Awake()
    {
        instance = this;
    }

    /***********************************************************************************
    * Function Name: Start
    * Description: Used for initialization.
    *   - This Start method uses auxilliary methods to facilitate each player placing
    *       his or her workers in their strating positions. Therein in doing, it  uses
    *       'yield return StartRoutine(myfunc())', which dictates that myfunc must 
    *       finish executing prior to proceeding. 
    *           
    ************************************************************************************/
    IEnumerator Start()
    {

        // Begin Setup mode Logic
        currentMode = Mode.SettingUp;

         yield return StartCoroutine(SetUpPlayers());

        // Begin core gameplay logic (In update method)
        currentMode = Mode.Playing;
        
        gameUI.GetComponent<WorkerSelector>().EnterState();
        StartPlaying();
    }


    /***********************************************************************************
    * Function Name: Update
    * Description: Called once per frame.
    **********************************************************************************/
    void Update()
    {
        if (Input.GetMouseButtonUp(0)) {
            if (currentMode == Mode.Playing) {
                Debug.Log("we are in play Mode ");
            }

        }
    }

    /***********************************************************************************
    * AUXILLIARY METHODS
    **********************************************************************************/

    /***********************************************************************************
    * Function Name: SetUpPlayers
    * Description: This is a wrapper function for the SetUp Workers coroutine
    ************************************************************************************/
    private IEnumerator SetUpPlayers() {
        // Player 1 place workers in starting positions
        yield return StartCoroutine(SetUpWorkers());


        // Player 2 place workers in starting positions
        yield return StartCoroutine(SetUpWorkers());

    }

    /***********************************************************************************
    * Function Name: SetUpWorkers
    * Description: This method is called from the Start() method twice, so that
    *   each player can place their players in their starting spaces. There is
    *   a while loop that keeps waiting for a user to click on a space.
    ************************************************************************************/
    private IEnumerator SetUpWorkers()
    {
        Debug.Log("SetUpWorkers [GameManager.cs]");
        for (int j = 0; j < currentPlayer.workers.Count; j++)
        {
            while (currentPlayer.workers[j].currentSpace == null) {                                                 //ask for a another left click if user didn't init their worker's currentSpace
                yield return new WaitUntil(() => Input.GetMouseButtonUp(0));
                SetWorkerStartingSpace(currentPlayer.workers[j]);
            }
        }
        // Call to EndTurn(), which is used to update the current player variable
        EndTurn();
    }

    /***********************************************************************************
    * Function Name: SetWorkerStartingSpace
    * Description: Setter function set PlayerWorker's starting space
    **********************************************************************************/
    void SetWorkerStartingSpace(PlayerWorker worker)
    {
       RaycastHit hit;
       Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit, 25.0f, LayerMask.GetMask("Tile")))
            {
                Space clickedSpace = hit.collider.gameObject.GetComponent<Space>();
                    if (clickedSpace.workerAtSpace != null)
                    {                                                           //did the user click on a space that's already occupied by another worker? then do not assign space to worker's currentSpace
                        return;
                    }
                    PlaceWorker(worker, clickedSpace);
            }
    }

    /***********************************************************************************
    * Function Name: EndTurn
    * Description: This method is used to cycle the current player. It updateds the
    *   currentPlayer property (i.e. a public variable declared at the top of the file.)
    ************************************************************************************/
    public void EndTurn() {
        Player temp;
        // Update Current Player
        temp = currentPlayer;
        currentPlayer = otherPlayer;
        otherPlayer = temp;
        Debug.Log("current player is now Player " + currentPlayer.playerId);
    }
   

    /***********************************************************************************
    * Function Name: WorkerCurrentPlayer
    * Description: Determines if a worker belongs to the current player
    **********************************************************************************/
    public bool WorkerCurrentPlayer(PlayerWorker worker)
    {
        return currentPlayer.workers.Contains(worker);
    }

    /***********************************************************************************
    * Function Name: SelectWorker
    * Description: Selects a worker by changing its color
    **********************************************************************************/
    public void SelectWorker(PlayerWorker worker)
    {
        gameUI.SelectWorker(worker);
    }


    /***********************************************************************************
    * Function Name: ResetSelection
    * Description: Resets a worker selection
    **********************************************************************************/

    public void ResetSelection(PlayerWorker worker)
    {
        gameUI.ResetSelection(worker);
    }

    /***********************************************************************************
    * Function Name: placeWorker
    * Description: Helper function to place worker at correct height on a given 
    *   space.
    **********************************************************************************/
    public void PlaceWorker(PlayerWorker worker, Space selectedSpace)
    {
        if (worker.currentSpace)
        {
            worker.currentSpace.workerAtSpace = null;
        }
        selectedSpace.workerAtSpace = worker;
        worker.currentSpace = selectedSpace;
        // Calls helper method in the gameUI class to place worker
        gameUI.PlaceWorker(worker, selectedSpace);

        // Moving floor placement offset logic to GameUI.cs

    }

    /***********************************************************************************
    * Function Name: BuildBlock
    * Description: Helper function to build a block on a space
    **********************************************************************************/
    public void BuildBlock(Space selectedSpace)
    {
        // Calls helper method in the gameUI class to build the block
        gameUI.BuildBlock(selectedSpace);

        selectedSpace.IncrementFloorLevel();

    }

    /***********************************************************************************
    * Function Name: GetAvailableMoves
    * Description: Get the available moves for a worker
    **********************************************************************************/
    public List<Space> GetAvailableMoves(PlayerWorker worker)
    {
        List<Space> availableMoves = new List<Space>();
        availableMoves.AddRange(worker.currentSpace.AdjSpaces);

        //Remove all spaces with workers 
        //TODO: Conditionals for God cards
        availableMoves.RemoveAll(moves => moves.workerAtSpace != null);

        //Remove all spaces that are too high or too low
        availableMoves.RemoveAll(moves => (int)moves.GetSpaceFloorLevel() - (int)worker.currentSpace.GetSpaceFloorLevel() > 1);

        //Remove all spaces that are blocked by domes
        availableMoves.RemoveAll(moves => moves.blockedByDome);

        return availableMoves;
    }

    /***********************************************************************************
    * Function Name: GetAvailableBuilds
    * Description: Get the available moves for a worker
    **********************************************************************************/
    public List<Space> GetAvailableBuilds(PlayerWorker worker)
    {
        List<Space> availableBuilds = new List<Space>();
        availableBuilds.AddRange(worker.currentSpace.AdjSpaces);

        //Remove all spaces with workers 
        //TODO: Conditionals for God cards
        availableBuilds.RemoveAll(moves => moves.workerAtSpace != null);

        //Remove all spaces that are blocked by domes
        availableBuilds.RemoveAll(moves => moves.blockedByDome);

        return availableBuilds;
    }

    public void StartPlaying() {
        //do the bottom while we are playing

        //current player moves
        //select a worker (check if click is on legal worker)
        //move the worker (after checking if that move is legal)

        //start build phase 
        //build at a space (check if building at the space is legal)

        //end turn
        Debug.Log("Staring Playing");
        //while (currentMode == Mode.Playing) {

        //}
        Debug.Log("Playing Mode ended");
    }


    public void StartTurn() {
        //
        Debug.Log("In StartTurn() [GameManager.cs]");
        //player cur moves
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 25.0f, LayerMask.GetMask("Worker")))
        {
            PlayerWorker clickedWorker = hit.collider.gameObject.GetComponent<PlayerWorker>();
            if (clickedWorker.owner == currentPlayer)
            {                                                           //did the user click on a space that's already occupied by another worker? then do not assign space to worker's currentSpace
                Debug.Log("clicked on a current Player's worker");
            }
            else if(clickedWorker.owner == otherPlayer) {
                Debug.Log("clicked on a other Player's worker");
            }
        }
    }

}

