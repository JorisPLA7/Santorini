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

    public FloorLevel currentFloorLevel;       // 0 for no building (Set by Lx_BuildingControllers
    public PlayerWorker workerAtSpace;  // Worker at the space
   

    public Vector3 spacePosition;       // PLEASE DO NOT DELETE (used for building placement)

    public GameObject building;     //Pointer to building

    public int xLocation;
    public int yLocation;

    //neighbors
    public Space north;
    public Space west;
    public Space east;
    public Space south;
    public Space northwest;
    public Space northeast;
    public Space southwest;
    public Space southeast;
    /***********************************************************************************
    * Function Name: Start
    * Description: Used for initialization. 
    **********************************************************************************/
    void Start() {
        blockedByDome = false;                          // Game starts with no domes
        workerAtSpace = null;                        // Game starts prior to players choosing starting spaces
        building = null;

        // Record the address of this space
        spacePosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        currentFloorLevel = FloorLevel.ground;
    }

    public List<Space> AdjSpaces;                          // This is an array storing adjacent spaces to this.Space


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
    * Function Name: IncrementFloorLevel
    * Description: Increment the floor level by 1
    **********************************************************************************/
    public void IncrementFloorLevel()
    {
        currentFloorLevel++;

        if (currentFloorLevel == FloorLevel.fullTower)
        {
            blockedByDome = true;
        }
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
    * Function Name: GetHeight
    * Description: Get the height of the building on the space
    **********************************************************************************/
    public float GetHeight()
    {
        switch(currentFloorLevel)
        {
            case FloorLevel.first:
                return L1_BuildingController.L1_Building_Height;
            case FloorLevel.second:
                return L2_BuildingController.L2_Building_Height;
            case FloorLevel.third:
                return L3_BuildingController.L3_Building_Height;
            default:
                return 0.0f;
        }
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
