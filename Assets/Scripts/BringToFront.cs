using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BringToFront : MonoBehaviour {

	void OnEnable () {
        transform.SetAsLastSibling();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
