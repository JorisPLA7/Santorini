using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Artemis : GodCard {

    //Allows a worker to move twice, but not back to the original spot
    public override List<Space> GetAvailableMoves(PlayerWorker worker)
    {

        Debug.Log("Inside Artemis's GetAvailableMoves");
        List<Space> finalAvailableMoves = new List<Space>();
        List<Space> tempMoves;
        finalAvailableMoves = base.GetAvailableMoves(worker);

        //If Athena was activated, take out all moves that lead upward
        if (GameManager.instance.AthenaActivated)
        {
            finalAvailableMoves.RemoveAll(moves => (int)moves.GetSpaceFloorLevel() - (int)worker.currentSpace.GetSpaceFloorLevel() > 0);
        }

        //Get all moves possible for a second move
        foreach (Space moves in finalAvailableMoves)
        {
            tempMoves = new List<Space>();

            tempMoves.AddRange(moves.AdjSpaces);

            //Remove all spaces with worker
            tempMoves.RemoveAll(temp => temp.workerAtSpace != null);

            //Remove all spaces that are too high or too low
            tempMoves.RemoveAll(temp => (int)temp.GetSpaceFloorLevel() - (int)moves.GetSpaceFloorLevel() > 1);

            //Remove all spaces that are blocked by domes
            tempMoves.RemoveAll(temp => temp.blockedByDome);

            //Union with available moves
            finalAvailableMoves = finalAvailableMoves.Union(tempMoves).ToList();
        }
        //removing any space with domes (vs Atlas)
        
        return finalAvailableMoves;

    }
}
