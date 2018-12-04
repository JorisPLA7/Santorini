using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Prometheus : GodCard {
    private ModalPanel modalPanel;
    private bool buildBeforeMove;

    private UnityAction button1Action;
    private UnityAction button2Action;

        void Start()
        {
            name = "Prometheus";
        }
 

	// Use this for initialization
	public override void Activate () {
        modalPanel = ModalPanel.Instance();
        buildBeforeMove = false;

        button1Action = new UnityAction(BuildBeforeMove);
        button2Action = new UnityAction(base.ExitState);

	}
	
	// The player decides to build before moving
	void BuildBeforeMove()
    {
        buildBeforeMove = true;
        base.EnterBuildState(GameManager.instance.gameUI.GetComponent<WorkerSelector>().GetSelectedWorker());
    }

    public override void ExitBuildState()
    {
        if (buildBeforeMove)
        {
            //Enter move state as player decided to build before moving
            base.EnterMoveState(GameManager.instance.gameUI.GetComponent<TileSelector>().GetSelectedWorker());
        } else
        {
            base.ExitBuildState();
        }
    }

    public override void ExitMoveState()
    {
        if (buildBeforeMove)
        {
            buildBeforeMove = false;
        }
        base.ExitMoveState();
    }

    public override List<Space> GetAvailableMoves(PlayerWorker worker)
    {
        List<Space> AvailableMoves = base.GetAvailableMoves(worker);

        //Cannot move upward if player built before moving
        if (buildBeforeMove)
        {
            AvailableMoves.RemoveAll(moves => (int)moves.GetSpaceFloorLevel() - (int)worker.currentSpace.GetSpaceFloorLevel() > 0);
        }

        return AvailableMoves;
    }

    public override void ExitState()
    {
        GameManager.instance.gameUI.GetComponent<WorkerSelector>().enabled = false;

        //Give player the choice to build before moving through a dialog window
        modalPanel.Choice("Build before Moving?\n(If yes, cannot move up this turn.)", "Yes", "No", button1Action, button2Action);

    }
}
