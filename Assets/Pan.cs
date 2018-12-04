using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pan : GodCard {

    private bool movedDownTwoLevels;

    void Start()
    {
        name = "Pan";
        movedDownTwoLevels = false;
    }

    public override bool HasCurrentPlayerWon()
    {
        if (movedDownTwoLevels)
        {
            return true;
        }

        return base.HasCurrentPlayerWon();
    }

    public override void PlaceWorker(PlayerWorker worker, Space space)
    {

        if (worker.currentSpace.GetSpaceFloorLevel() - space.GetSpaceFloorLevel() >= 2)
        {
            movedDownTwoLevels = true;
        }

        base.PlaceWorker(worker, space);
    }

}
