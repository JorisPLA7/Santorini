using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GodCard : MonoBehaviour {

    public string name;
    void Start() 
    {
            
    }

    public virtual void Activate() {}
    public virtual void PlaceWorker(PlayerWorker worker, Space space)
    {
        GameManager.instance.MoveWorkerToSpace(worker, space);
    }

    public virtual bool HasCurrentPlayerWon()
    {
        return GameManager.instance.HasCurrentPlayerWon();
    }

    public virtual List<Space> GetAvailableMoves(PlayerWorker worker)
    {
        return GameManager.instance.GetAvailableMoves(worker);
    }

    public virtual List<Space> GetAvailableBuilds(PlayerWorker worker)
    {
        return GameManager.instance.GetAvailableBuilds(worker);
    }

    public virtual void EndTurn() {

        GameManager.instance.EndTurn();
    }

    public virtual void EnterBuildState(PlayerWorker worker)
    {
        GameManager.instance.gameUI.GetComponent<TileSelector>().EnterBuildState(worker);
    }

    public virtual void ExitBuildState()
    {
        GameManager.instance.gameUI.GetComponent<TileSelector>().ExitBuildState();
    }

    public virtual void EnterMoveState(PlayerWorker worker)
    {
        GameManager.instance.gameUI.GetComponent<TileSelector>().EnterMoveState(worker);
    }

    public virtual void ExitMoveState()
    {
        GameManager.instance.gameUI.GetComponent<TileSelector>().ExitMoveState();
    }

    public virtual void ExitState()
    {
        GameManager.instance.gameUI.GetComponent<WorkerSelector>().ExitState();
    }

    public virtual void BuildBlock()
    {
        GameManager.instance.BuildBlock();
    }
}
