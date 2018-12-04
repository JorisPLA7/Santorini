using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Demeter : GodCard {
    private ModalPanel modalPanel;
    private bool firstBuild;
    private Space firstBuildSpace;

    private UnityAction button1Action;
    private UnityAction button2Action;

    // Use this for initialization
        void Start()
        {
            name = "Demeter";
        }
    public override void Activate()
    {
        firstBuild = true;
        firstBuildSpace = null;
        modalPanel = ModalPanel.Instance();

        button1Action = new UnityAction(ExitFirstBuildState);
        button2Action = new UnityAction(base.ExitBuildState);
    }

    public override void ExitBuildState()
    {
        if (firstBuild)
        {
            modalPanel.Choice("Build a second time?\n(If yes, cannot build on the same space.)", "Yes", "No", button1Action, button2Action);
        } else
        {
            firstBuild = true;
            firstBuildSpace = null;
            base.ExitBuildState();
        }        
    }

    void ExitFirstBuildState()
    {
        //Set firstBuild to false
        firstBuild = false;

        //Get first build's space
        firstBuildSpace = GameManager.instance.gameUI.GetComponent<TileSelector>().GetSelectedSpace();

        //Enter second build phase
        base.EnterBuildState(GameManager.instance.gameUI.GetComponent<TileSelector>().GetSelectedWorker());
    }

    public override List<Space> GetAvailableBuilds(PlayerWorker worker)
    {
        List<Space> AvailableBuilds = base.GetAvailableBuilds(worker);

        //Remove the space that was just built on
        if (!firstBuild)
        {
            AvailableBuilds.Remove(firstBuildSpace);
        }

        return AvailableBuilds;
    }
}
