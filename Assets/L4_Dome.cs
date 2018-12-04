using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L4_Dome : MonoBehaviour {
    public GameObject domeObject;
    private static L4_Dome dome;

    public static L4_Dome Instance()
    {
        if (!dome)
        {
            dome = FindObjectOfType(typeof(L4_Dome)) as L4_Dome;
            if (!dome)
            {
                Debug.LogError("No active Dome script");
            }
        }

        return dome;
    }

}
