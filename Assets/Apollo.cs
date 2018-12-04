using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Apollo : GodCard {
    
    void Start()
    {
        name = "Apollo";
        
    }
    public override List<Space> GetAvailableMoves(PlayerWorker worker)
    {

        Debug.Log("Inside Apollo's GetAvailableMoves");
        List<Space> availableMoves = new List<Space>();
        availableMoves.AddRange(worker.currentSpace.AdjSpaces);

        //Keep spaces with opposing workers
        availableMoves.RemoveAll(moves => moves.workerAtSpace && moves.workerAtSpace.owner == GameManager.instance.currentPlayer);

        //Remove all spaces that are too high or too low
        availableMoves.RemoveAll(moves => (int)moves.GetSpaceFloorLevel() - (int)worker.currentSpace.GetSpaceFloorLevel() > 1);

        //Remove all spaces that are blocked by domes
        availableMoves.RemoveAll(moves => moves.blockedByDome);

        return availableMoves;
    }

    public override void PlaceWorker(PlayerWorker worker, Space space)
    {
        Debug.Log("Using Apollo Place Worker");
        //If not at game start and there is an opposing worker at the space to move to
        if (worker.currentSpace && space.workerAtSpace)
        {

            //Force the opposing worker to the space the current worker is occupying
            worker.enabled = false;
            base.PlaceWorker(space.workerAtSpace, worker.currentSpace);
            //Move the worker, don't use base function because current space's worker is not set to null because opposing worker was forced there
            space.workerAtSpace = worker;
            worker.currentSpace = space;
            // Calls helper method in the gameUI class to place worker
           
            GameManager.instance.gameUI.PlaceWorker(worker, space);
             GameManager.instance.flash.FlashAttack();
            worker.enabled = true;
            
        }
        else //Use default move if not
        {
            base.PlaceWorker(worker, space);
        }
    }
    



    }
