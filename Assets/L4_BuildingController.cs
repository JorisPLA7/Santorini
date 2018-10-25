using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L4_BuildingController : MonoBehaviour {

    public GameObject L4_Full_TowerPrefab;
    public GameObject _L4_Full_Tower;


    // Update is called once per frame
    void Update () {
		
        // Build a L4 Full Tower
        // The following impements the logic to place a tower on the space
        // selected by first right-clicking a space on the board, and
        // subsequently pressing the number 4 on the keyboard.

        if(Input.GetKeyUp(KeyCode.Alpha4) || Input.GetKeyUp(KeyCode.Keypad4)) {


            RaycastHit hit;
            Ray ray;
            Space selectedSpace;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if(Physics.Raycast(ray, out hit, 25.0f, LayerMask.GetMask("Tile")))
            {
                // Build a level floor tower on space
                selectedSpace = hit.collider.gameObject.GetComponent<Space>();
                _L4_Full_Tower = Instantiate(L4_Full_TowerPrefab) as GameObject;
                _L4_Full_Tower.transform.position = selectedSpace.spacePosition;

                // Set building height for placement of workers or dome
                selectedSpace.SetSpaceFloorLevel(FloorLevel.fullTower);

                // Set space blockedByDome member of Space
                selectedSpace.SetBlockedByDome(true);
            }

        }

       
	}
}
