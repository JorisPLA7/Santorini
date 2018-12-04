/***********************************************************************************
 * Script Name: GameUI
 * Description: 
 **********************************************************************************/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameUI : MonoBehaviour {

    public L1_BuildingController l1_build;
    public L2_BuildingController l2_build;
    public L3_BuildingController l3_build;
    public L4_BuildingController l4_build;

    public ParticleSystem HermesDust;
    public ParticleSystem HepFlame;
    public AudioSource HermesSpeed;
    
    /***********************************************************************************
    * Script Name: SelectWorker
    * Description: Show worker selection.
    **********************************************************************************/
    public void SelectWorker(PlayerWorker worker)
    {
        worker.SelectWorker();
    }

    /***********************************************************************************
    * Script Name: ResetSelection
    * Description: Reset worker selection.
    **********************************************************************************/
    public void ResetSelection(PlayerWorker worker)
    {
        worker.DeselectWorker();
    }


    /***********************************************************************************
    * Script Name: PlaceWorker
    * Description: Move a worker to a space (Animation related)
    **********************************************************************************/
    public void PlaceWorker(PlayerWorker worker, Space selectedSpace)
    {
        worker.transform.position = new Vector3(selectedSpace.spacePosition.x, selectedSpace.GetHeight(), selectedSpace.spacePosition.z);

        if (worker.owner.currentGodCard.name == "Hermes" && GameManager.instance.currentPlayer == worker.owner && GameManager.instance.currentMode != GameManager.Mode.SettingUp)
            {
            HermesSpeed.Play();
            PlayEffectOnce(HermesDust, worker.transform.position);
            }
    }


    /***********************************************************************************
    * Script Name: BuildBlock
    * Description: Visually build a block on a space at its current floor level
    **********************************************************************************/
    public void BuildBlock(Space selectedSpace)
    {

        //Destroy building on the space
        Destroy(selectedSpace.building);

        // Transform to account for space currentFloorLevel
        if (selectedSpace.GetSpaceFloorLevel() == FloorLevel.ground)
        {
            //Spawn L1_building
            l1_build.Spawn_L1(selectedSpace);
        }
        else if (selectedSpace.GetSpaceFloorLevel() == FloorLevel.first)
        {
            //Spawn L2_building
            l2_build.Spawn_L2(selectedSpace);
        }
        else if (selectedSpace.GetSpaceFloorLevel() == FloorLevel.second)
        {
            //Spawn L3_building
            l3_build.Spawn_L3(selectedSpace);
        }
        else if (selectedSpace.GetSpaceFloorLevel() == FloorLevel.third)
        {
            //Spawn L4_building
            l4_build.Spawn_L4(selectedSpace);
        }

        if (GameManager.instance.currentPlayer.currentGodCard.name == "Hephaestus")
        {
            Debug.Log("activating hep flame");
            PlayEffectOnce(HepFlame, selectedSpace.spacePosition);
        }
    }

    protected void PlayEffectOnce(ParticleSystem prefab, Vector3 position) {
        if (prefab == null) { return ; }

        ParticleSystem ps = Instantiate(prefab, position, Quaternion.identity) as ParticleSystem;
        GetComponent<particleDestroyer>().particleOn =ps;
        GetComponent<particleDestroyer>().destroyParticle();
    }

    /***********************************************************************************
    * Function Name: Start
    * Description: Used for initialization. 
    **********************************************************************************/
    /*
    void Start () {
        selectedSpace = null;
        spaceHighlight = GameObject.Find("TileSelector");
        spaceHighlight.SetActive(false);
        selectedSpacePosition = new Vector3(0.0f, 0.0f, 0.0f);
	}
    */


    /***********************************************************************************
    * Function Name: Update
    * Description: Called once per frame.
    **********************************************************************************/
    /*
    void Update () {
        // Highlight Selected Space///////////////////////////////////////////
        Ray ray = gameCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hitInfo;

        //Raycast will only hit spaces
        if (Physics.Raycast(ray, out hitInfo, 25.0f, LayerMask.GetMask("Tile")))
        {
            //If it hits a space, select it
            selectedSpace = hitInfo.collider.gameObject;
            selectedSpacePosition.x = selectedSpace.transform.position.x;
            selectedSpacePosition.z = selectedSpace.transform.position.z;
        } else
        {
            selectedSpace = null;
            spaceHighlight.SetActive(false);
        }

        //Highlight the selected space if applicable
        if(selectedSpace)
        {
            spaceHighlight.transform.position = selectedSpacePosition;
            spaceHighlight.SetActive(true);
        }
    }
    */

}
