using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//    Opponent’s Turn: If one of your
//Workers moved up on your last
//turn, opponent Workers cannot
//move up this turn.
public class Athena : GodCard
    {
    //member variables
   




 
    void Start()
        {
            name = "Athena";
        }




    public override void PlaceWorker(PlayerWorker worker, Space selectedSpace)
        {
        Debug.Log("Using Athena PlaceWorker");
        //for athena, we need to find out if they move a worker up
            base.PlaceWorker(worker, selectedSpace);                    //do the normal movement, this updates levelChangePerTurn

        GameManager.instance.IsAthenaActive(worker.owner);              //now we determine if athena is activated after movement

        }


    }