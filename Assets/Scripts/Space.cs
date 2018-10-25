/***********************************************************************************
 * Script Name: Space
 * Description: 
 **********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Please do not delete FloorLevel enum or any of the values 
public enum FloorLevel { ground, first, second, third, fullTower };

public class Space : MonoBehaviour {
                        // Current floor level
    //enum PlayerToken { playerOneA, playerOneB, playerTwoA, playerTwoB };         // Which player token may be occupying the space

    public bool blockedByDome;          // Blocked by dome bool

    FloorLevel currentFloorLevel;       // 0 for no building (Set by Lx_BuildingControllers
    public PlayerWorker workerAtSpace;  // Worker at the space
    //PlayerToken playerOccupying;                                 // Player1 (1 and 2)

    public Vector3 spacePosition;       // PLEASE DO NOT DELETE (used for building placement)


    /***********************************************************************************
    * Function Name: Start
    * Description: Used for initialization. 
    **********************************************************************************/
    void Start() {
        blockedByDome = false;                          // Game starts with no domes
        workerAtSpace = null;                        // Game starts prior to players choosing starting spaces

        // Record the address of this space
        spacePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        currentFloorLevel = FloorLevel.ground;
    }

    public List<Space> AdjSpaces;                          // This is an array storing adjacent spaces to this.Space



    /***********************************************************************************
    * Function Name: Update
    * Description: Called once per frame.
    **********************************************************************************/
    void Update () {


	}


    /***********************************************************************************
    * HELPER METHODS
    **********************************************************************************/


    /***********************************************************************************
    * Function Name: GetSpaceFloorLevel
    * Description: Returns an enum representing the current height of building on the
    *   space.
    **********************************************************************************/
    public FloorLevel GetSpaceFloorLevel() {
        return currentFloorLevel;
    }

    /***********************************************************************************
    * Function Name: SetSpaceFloorLevel
    * Description: 
    **********************************************************************************/
    public void SetSpaceFloorLevel(FloorLevel assignLevel)
    {
        currentFloorLevel = assignLevel;

    }

    /***********************************************************************************
    * Function Name: SetBlockedByDome
    * Description: 
    **********************************************************************************/
    public void SetBlockedByDome(bool blocked)
    {
        blockedByDome = blocked;
    }

}
