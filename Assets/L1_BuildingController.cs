using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L1_BuildingController : MonoBehaviour {

    public GameObject L1_TowerPrefab;
    public GameObject _L1_Tower;

    public const float L1_Building_Height = .58f; // Used for object placement on top

    // Update is called once per frame
    void Update () {
        // Build a L1 Tower
        // The following impements the logic to place a tower on the space
        // selected by first right-clicking a space on the board, and
        // subsequently pressing the number 1 on the keyboard.
        if (Input.GetKeyUp(KeyCode.Keypad1) || Input.GetKeyUp(KeyCode.Alpha1)){
            RaycastHit hit;
            Ray ray;
            Space selectedSpace;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 25.0f, LayerMask.GetMask("Tile")))
            {
                // Build L1 Tower on space
                selectedSpace = hit.collider.gameObject.GetComponent<Space>();
                _L1_Tower = Instantiate(L1_TowerPrefab) as GameObject;
                _L1_Tower.transform.position = selectedSpace.spacePosition;
                // This additional trasform is to account for a scaling anomoly caused by Blender
                _L1_Tower.transform.position = new Vector3(selectedSpace.spacePosition.x, (float)0.3, selectedSpace.spacePosition.z);

                // Set building height for placement of workers or dome
                selectedSpace.SetSpaceFloorLevel(FloorLevel.first);
            }
        }
	}
}
