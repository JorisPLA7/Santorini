using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AthenaRestriction : GodCard {


    //TODO THIS IS NOT OVERRIDING THE APOLLO GetAvailableMoves()... would have to hardcode Side effect to all gods....
    //restrict movement
    public override List<Space> GetAvailableMoves(PlayerWorker worker)
        {
        Debug.Log("Athena restriction called");
            List<Space> availableMoves = new List<Space>();
            availableMoves.AddRange(worker.currentSpace.AdjSpaces);
            //Remove all spaces that are higher
            availableMoves.RemoveAll(moves => (int)moves.GetSpaceFloorLevel() - (int)worker.currentSpace.GetSpaceFloorLevel() >= 1);
            //Remove all spaces that are blocked by domes
            availableMoves.RemoveAll(moves => moves.blockedByDome);
            return availableMoves;
        }




    }
