using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GodSelection : MonoBehaviour {
    public bool oneDone;
    public bool twoDone;
    public static GodSelection instance;
	// Use this for initialization
    void Awake(){
        instance = this;
            }
	IEnumerator Start () {
		//player one selects their god
        oneDone=false;
        twoDone=false;
       yield return StartCoroutine(PickGod());                                      //set up gods
            Debug.Log("outside PickGod");
      SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);         //load game scene
	}
	
    private IEnumerator PickGod()
    {
        while(!oneDone)
            { 
                yield return new WaitUntil(()=> Input.GetMouseButtonUp(0));
            }
                     
        
               while(!twoDone)
            { 
                yield return new WaitUntil(()=> Input.GetMouseButtonUp(0));
            }         
           
        

    }
}
