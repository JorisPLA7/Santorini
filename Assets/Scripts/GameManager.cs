/***********************************************************************************
 * Script Name: GameManager
 * Description: This script serves as the control center of the game. 
 ***********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

    public enum ActionPhase { Move, Build, Select };
    public static GameManager instance;
    public GameUI gameUI;
    public Player currentPlayer;
    public Player otherPlayer;
    public Player PlayerOne;
    public Player PlayerTwo;
    public enum Mode {SettingUp,Playing,Ending };
    public Mode currentMode;
    public ActionPhase currentPhase;
    

    //God related variables
    public bool AthenaActivated;         //true if athena was activated
    public ApolloFlash flash;
    public AudioSource MinotaurCharge;
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
    IEnumerator Start ()
    {
       
    //GameCamera.ShowMainCamera();

        if(Settings.selectedMode==Settings.Modes.gods){
                Debug.Log("Setting up God Mode");
            PlayerOne.currentGodCard=Settings.playerOneChoice;
            PlayerTwo.currentGodCard=Settings.playerTwoChoice;
            //TODO: Uncomment if activating god cards is better here.
            //SetUpGodCards();
        }

        //TODO: For Debugging purposes. Can remove it and uncomment above statement, or keep it.
        SetUpGodCards();

        // Begin Setup mode Logic
        currentMode = Mode.SettingUp;

        yield return StartCoroutine(SetUpPlayers());

        // Begin core gameplay logic (In update method)
       
        currentMode = Mode.Playing;


        gameUI.GetComponent<WorkerSelector>().EnterState();
    }



    /***********************************************************************************
    * Function Name: Update
    * Description: Called once per frame.
    **********************************************************************************/
    void Update()
    {
        if(Input.GetKey("escape"))
        {
            Application.Quit();
        }
    }

    /***********************************************************************************
    * AUXILLIARY METHODS
    **********************************************************************************/
    /***********************************************************************************
    * Function Name: SetUpPlayers
    * Description: This is a wrapper function for Setting up workers
    ************************************************************************************/
    private IEnumerator SetUpPlayers()
    {
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
                    MoveWorkerToSpace(worker, clickedSpace);
            }
    }

    /***********************************************************************************
    * Function Name: SetUpGodCards
    * Description: This is a wrapper function for Setting up god cards
    ************************************************************************************/
    void SetUpGodCards()
    {
        PlayerOne.currentGodCard.Activate();
        PlayerTwo.currentGodCard.Activate();
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
    * Function Name: StartMovementPhase
    * Description:  This function determines how to proceed with the movement logic
    *  For instance, is this movement phase restricted in some way (Athena), or enhanced in someway
    *  i.e. (Apollo- allows swapping of positions, Minotaur- allows pushing players, Artemis- allows 2 spaces of movement)
    **********************************************************************************/

    public void StartMovementPhase(PlayerWorker worker, Space selectedSpace)
        {
        //Determine if there are any movement restrictions
        //TODO: Add Hermes conditional
        if (AthenaActivated)
            {
                worker.owner.currentGodCard.PlaceWorker(worker, selectedSpace);
                instance.AthenaActivated = false;            //turn off effect afterwards
            }
        else{
                //move with no restrictions, or move with god ability
                worker.owner.currentGodCard.PlaceWorker(worker, selectedSpace);
            
            }     
        }




    /***********************************************************************************
    * Function Name: DetermineAvailableMovement
    * Description: This function determines if there are any changes to available movements 
    *              for that worker and outputs a Space List of legal moves for that worker
    **********************************************************************************/
    public List<Space> GetPossibleMovementsForWorker(PlayerWorker worker) {
        List<Space> AvailableMoves = new List<Space>();

        //Get available moves allowed by player's god card
        AvailableMoves = worker.owner.currentGodCard.GetAvailableMoves(worker);

        //If Athena was activated, take out all moves that lead upward
        if (instance.AthenaActivated)
        {
            AvailableMoves.RemoveAll(moves => (int)moves.GetSpaceFloorLevel() - (int)worker.currentSpace.GetSpaceFloorLevel() > 0);
        }

        return AvailableMoves;

    }

    /***********************************************************************************
    * Function Name: MoveWorkerToSpace
    * Description: This is the normal movement function
    **********************************************************************************/
    public void MoveWorkerToSpace(PlayerWorker worker, Space selectedSpace)
    {
        if (instance.currentMode == Mode.Playing) {         //added this conditional to keep track of worker level changes, used for Athena ability
                    worker.owner.levelChangeDuringTurn=GetFloorLevelChange(worker.currentSpace, selectedSpace);                
            }
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
    public void BuildBlock()
    {
        Space selectedSpace = gameUI.GetComponent<TileSelector>().GetSelectedSpace();

        // Calls helper method in the gameUI class to build the block
        gameUI.BuildBlock(selectedSpace);

        selectedSpace.IncrementFloorLevel();

    }

    /***********************************************************************************
    * Function Name: GetAvailableMoves
    * Description: Get the typically available moves for a worker with no buffs/debuffs
    **********************************************************************************/
    public List<Space> GetAvailableMoves(PlayerWorker worker)
        {
        List<Space> availableMoves = new List<Space>();
        availableMoves.AddRange(worker.currentSpace.AdjSpaces);

        //Remove all spaces with worker
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


    /***********************************************************************************
    * Function Name: hasCurrentPlayerWon
    * Description: Check if the current player has won
    **********************************************************************************/
    public bool HasCurrentPlayerWon()
    {
        foreach (PlayerWorker worker in currentPlayer.workers)
        {   //TODO: ADD A CONDITIONAL FOR GOD 'PAN'
            if (worker.currentSpace.GetSpaceFloorLevel() == FloorLevel.third)
            {
                return true;
            }
        }

        return false;
    }

    /***********************************************************************************
    * Function Name: EndGame
    * Description: Check if the current player has won
    **********************************************************************************/
    public void EndGame()
        {
        currentMode = Mode.Ending;
        //TODO: display Winning Screen
        }

    /***********************************************************************************
    * Function Name: GetFloorLevelChange
    * Description: returns a floor level change of worker after movement
    **********************************************************************************/
    public int GetFloorLevelChange(Space starting, Space ending) {
        return ending.currentFloorLevel - starting.currentFloorLevel;
    }

    /***********************************************************************************
    * GODS STUFF
    * 
    **********************************************************************************/
    public void IsAthenaActive(Player athenaOwner)
        {
            AthenaActivated = athenaOwner.levelChangeDuringTurn > 0;
        }

    

    //END OF GameManager
    }

