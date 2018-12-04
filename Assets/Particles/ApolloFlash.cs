using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ApolloFlash : MonoBehaviour {

    public CanvasGroup myCG;
    public bool flash;
    public Canvas ApolloCanvas;
    void Start() {
        flash = false;
        ApolloCanvas = GetComponent<Canvas>();
        }
    // Update is called once per frame
    void Update () {
        if (flash)
            {
            Debug.Log("flash ran");
            myCG.alpha = myCG.alpha - Time.deltaTime;
            if (myCG.alpha <= 0)
                {
                myCG.alpha = 0;
                flash = false;
                }
            }
        }

    public void FlashAttack() {
        flash = true;
        myCG.alpha = 1;
        }
}
