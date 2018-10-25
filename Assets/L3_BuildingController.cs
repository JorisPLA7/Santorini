using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3_BuildingController : MonoBehaviour {

    public GameObject L3_TowerPrefab;
    public GameObject _L3_Tower;

    public const float L3_Building_Height = 1.65f; // Used for object placement on top

    // Update is called once per frame
    void Update () {

        // Build a L3 Tower
        // The following impements the logic to place a tower on the space
        // selected by first right-clicking a space on the board, and
        // subsequently pressing the number 2 on the keyboard.

        if (Input.GetKeyUp(KeyCode.Keypad3) || Input.GetKeyUp(KeyCode.Alpha3))
        {
            RaycastHit hit;
            Ray ray;
            Space selectedSpace;

            ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 25.0f, LayerMask.GetMask("Tile")))
            {
                selectedSpace = hit.collider.gameObject.GetComponent<Space>();
                _L3_Tower = Instantiate(L3_TowerPrefab) as GameObject;
                _L3_Tower.transform.position = selectedSpace.spacePosition;

                // Set building height for placement of workers or dome
                selectedSpace.SetSpaceFloorLevel(FloorLevel.third);
            }
        }
    }
}
