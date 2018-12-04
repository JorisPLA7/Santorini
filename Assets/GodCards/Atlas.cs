using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Atlas : GodCard {
    private ModalPanel modalPanel;
    public L4_Dome domePrefab;     //changed this to dome from Gameobject
    private L4_Dome dome;
    private UnityAction button1Action;
    private UnityAction button2Action;

    // Use this for initialization
    void Start()
    {
        name = "Atlas";
    }
    public override void Activate()
    {
        modalPanel = ModalPanel.Instance();

        button1Action = new UnityAction(BuildLevel);
        button2Action = new UnityAction(BuildDome);

        //need to grab domePrefab
        domePrefab = L4_Dome.Instance();
    }

    //Choose whether to build a level or dome
    public override void BuildBlock()
    {
        if (GameManager.instance.gameUI.GetComponent<TileSelector>().GetSelectedSpace().GetSpaceFloorLevel() < FloorLevel.third)
        {
            modalPanel.Choice("Build a block or a dome?\n", "Block", "Dome", button1Action, button2Action);
        } else
        {
            BuildLevel();
        }

    }

    public override void ExitBuildState() {}

    //Build a level
    void BuildLevel()
    {
        base.BuildBlock();
        base.ExitBuildState();
    }

    //Build a dome
    void BuildDome()
    {
        Space space = GameManager.instance.gameUI.GetComponent<TileSelector>().GetSelectedSpace();
        Instantiate(domePrefab, new Vector3(space.transform.position.x, space.GetHeight(), space.transform.position.z), domePrefab.transform.rotation);
        space.SetBlockedByDome(true);
        base.ExitBuildState();
    }
}
