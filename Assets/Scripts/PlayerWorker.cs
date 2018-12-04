/***********************************************************************************
 * Script Name: PlayerWorker
 * Description: 
 **********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum WorkerID { pOneFemale, pOneMale, pTwoFemale, pTwoMale }


public class PlayerWorker : MonoBehaviour {

    public Component[] renderers;

    public Material selected;

    public WorkerID myWorkerID;
    public Player owner;
    public int audioToPlay;
    public string workerName;
    GameManager instance;
    public Space currentSpace;
    public AudioSource[] greetings;

    // List for material alternation
    public IList<Material> origMaterials = new List<Material>();

    //God Power Particles
    public ParticleSystem HermesDust;

    /***********************************************************************************
    * Function Name: Start
    * Description: Used for initialization.
    ************************************************************************************/
    void Start () {

        renderers = GetComponentsInChildren<SkinnedMeshRenderer>();

        audioToPlay = 0;
     
    }

    /***********************************************************************************
    * Function Name: Update
    * Description: Called once per frame.
    **********************************************************************************/

    void Update()
    {
      
    }


    /***********************************************************************************
    * AUXILLIARY METHODS
    **********************************************************************************/

    void OnMouseUp()
    {
        //Debug.Log("clicked on " + this.workerName);
        playNextGreeting();

     }

    /***********************************************************************************
    * Function Name: SelectWorker
    * Description: Indicate a worker is selected by substituting the selected material
    *   for the player object's original materials. 
    **********************************************************************************/
    public void SelectWorker()
    {
        origMaterials.Clear();

        foreach (SkinnedMeshRenderer renderer in renderers)
        {
            origMaterials.Add(renderer.material); // save for deselection
            renderer.material = selected;
        }
    }

    /***********************************************************************************
    * Function Name: DeselectWorker
    * Description: This method restores the player objects original materials. 
    **********************************************************************************/
    public void DeselectWorker()
    {
        for (int i = 0; i < renderers.Length; i++)
        {
            // Apply original materials from the list 
            renderers[i].GetComponent<SkinnedMeshRenderer>().material = origMaterials[i];
        }
    }


    /***********************************************************************************
    * Function Name: getWorkerID
    * Description: Returns the selected worker's ID.
    **********************************************************************************/
    public WorkerID getWorkerID() { return myWorkerID; }



    /***********************************************************************************
    * Function Name: playNextGreeting
    * Description: Plays a worker greeting from the public array of greetings
    **********************************************************************************/

    public void playNextGreeting()
    {

        if (audioToPlay >= greetings.Length)
            {
            audioToPlay = 0;
            }

        greetings[audioToPlay++].Play();
        }

}
