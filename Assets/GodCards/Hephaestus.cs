using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Hephaestus : GodCard {
    private ModalPanel modalPanel;

    private UnityAction button1Action;
    private UnityAction button2Action;

    // Use this for initialization
    void Start()
        {
            name = "Hephaestus";
        }
    public override void Activate()
    {
        modalPanel = ModalPanel.Instance();

        button1Action = new UnityAction(buildAdditionalBlock);
        button2Action = new UnityAction(base.ExitBuildState);
        
    }

    public override void ExitBuildState()
    {
        if (GameManager.instance.gameUI.GetComponent<TileSelector>().GetSelectedSpace().GetSpaceFloorLevel() < FloorLevel.third) 
        {
            modalPanel.Choice("Build a second block on top of the first build?\n", "Yes", "No", button1Action, button2Action);
        }
        else
        {
            base.ExitBuildState();
        }
    }

    public override void PlaceWorker(PlayerWorker worker, Space space)
    {
            base.PlaceWorker(worker, space);

    }

    void buildAdditionalBlock()
    {
        base.BuildBlock();
        base.ExitBuildState();
    }
}
