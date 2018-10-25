using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L2_BuildingController : MonoBehaviour {

    public GameObject L2_TowerPrefab;
    public GameObject _L2_Tower;

    public const float L2_Building_Height = 1.15f; // Used for object placement on top

    // Update is called once per frame
    void Update () {

        // Build a L2 Tower
        // The following impements the logic to place a tower on the space
        // selected by first right-clicking a space on the board, and
        // subsequently pressing the number 2 on the keyboard.

        if (Input.GetKeyUp(KeyCode.Keypad2) || Input.GetKeyUp(KeyCode.Alpha2))
        {
            RaycastHit hit;
            Ray ray;
            Space selectedSpace;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 25.0f, LayerMask.GetMask("Tile")))
            {
                // Build L2 Tower on space
                selectedSpace = hit.collider.gameObject.GetComponent<Space>();
                _L2_Tower = Instantiate(L2_TowerPrefab) as GameObject;
                _L2_Tower.transform.position = selectedSpace.spacePosition;

                // Set building height for placement of workers or dome
                selectedSpace.SetSpaceFloorLevel(FloorLevel.second);

            }
        }
    }
}
