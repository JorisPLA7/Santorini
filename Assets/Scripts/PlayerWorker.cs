/***********************************************************************************
 * Script Name: PlayerWorker
 * Description: 
 **********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorkerID { pOneFemale, pOneMale, pTwoFemale, pTwoMale }


public class PlayerWorker : MonoBehaviour {

    private Component[] renderers;
    public Material selected;
    public Material normal;
    public WorkerID myWorkerID;
    public Player owner;

    public string workerName;
    GameManager instance;

    public Space currentSpace;

    /***********************************************************************************
    * Function Name: Start
    * Description: Used for initialization.
    ************************************************************************************/
    void Start () {

        renderers = GetComponentsInChildren<MeshRenderer>();

    }

    /***********************************************************************************
    * Function Name: Update
    * Description: Called once per frame.
    **********************************************************************************/
    /*
    void Update () 
    {

	}
    */

    /***********************************************************************************
    * AUXILLIARY METHODS
    **********************************************************************************/

    void OnMouseUp()
    {
        Debug.Log("clicked on " + this.workerName);

       
    }

    /***********************************************************************************
    * Function Name: SelectWorker
    * Description: Indicate a worker is selected
    **********************************************************************************/
    public void SelectWorker()
    {
        foreach (MeshRenderer renderer in renderers)
            renderer.material = selected;
    }

    /***********************************************************************************
    * Function Name: DeselectWorker
    * Description: Indicate a worker is selected
    **********************************************************************************/
    public void DeselectWorker()
    {
        foreach (MeshRenderer renderer in renderers)
            renderer.material = normal;
    }


    /***********************************************************************************
    * Function Name: getWorkerID
    * Description: Returns the selected worker's ID.
    **********************************************************************************/
    public WorkerID getWorkerID() { return myWorkerID; }

}
