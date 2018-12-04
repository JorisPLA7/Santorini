using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Minotaur : GodCard {

    private ParticleSystem Blood;
    private MinotaurBlood BloodEffect;
    void Start()
    {
        name = "Minotaur";
    }

    public override void Activate()
        {
            BloodEffect = MinotaurBlood.Instance();
        }
    public override List<Space> GetAvailableMoves(PlayerWorker worker)
    {
        List<Space> availableMoves = new List<Space>();
        availableMoves.AddRange(worker.currentSpace.AdjSpaces);

        //Keep spaces with opposing workers
        availableMoves.RemoveAll(moves => moves.workerAtSpace && moves.workerAtSpace.owner == GameManager.instance.currentPlayer);

        availableMoves.RemoveAll(moves=> (moves.workerAtSpace !=null && !CanPushWorker(worker.currentSpace,moves)));
        //Remove all spaces that are too high or too low
        availableMoves.RemoveAll(moves => (int)moves.GetSpaceFloorLevel() - (int)worker.currentSpace.GetSpaceFloorLevel() > 1);

        //Remove all spaces that are blocked by domes
        availableMoves.RemoveAll(moves => moves.blockedByDome);

        return availableMoves;
    }

    bool CanPushWorker(Space from, Space to){
            if(from.north == to){
                Debug.Log("Coming from north");
                if(to.north == null|| to.north.workerAtSpace != null || to.north.blockedByDome){
                        return false;
                    }
                 else{
                    return true;
                    }
                }
          else  if(from.south == to){
                Debug.Log("Coming from south");

               if(to.south == null|| to.south.workerAtSpace != null || to.south.blockedByDome){
                        return false;
                    }
                 else{
                    return true;
                    }
                }
           else if(from.west == to){
                Debug.Log("Coming from west");
                if(to.west == null|| to.west.workerAtSpace != null || to.west.blockedByDome){
                    return false;
                    }
                 else{
                    return true;
                    }
                }
           else if(from.east == to){
                if(to.east == null|| to.east.workerAtSpace != null || to.east.blockedByDome){
                    return false;
                    }
                 else{
                    return true;
                    }
                }
           else if(from.northwest == to){
                Debug.Log("Coming from northwest");
                if(to.northwest == null|| to.northwest.workerAtSpace != null || to.northwest.blockedByDome){
                    return false;
                }
                else{
                    return true;
                }
            }
           else if(from.northeast == to){
                Debug.Log("Coming from northeast");
                if(to.northeast == null|| to.northeast.workerAtSpace != null  || to.northeast.blockedByDome){
                    return false;
                }
                else{
                    return true;
                }
            }
           else if(from.southwest == to){
                Debug.Log("Coming from southwest");
                if(to.southwest == null|| to.southwest.workerAtSpace != null  || to.southwest.blockedByDome){
                    return false;
                }
                else{
                    return true;
                }
            }
           else if(from.southeast == to){
                Debug.Log("Coming from southeast");
                if(to.southeast == null|| to.southeast.workerAtSpace != null  || to.southeast.blockedByDome){
                    return false;
                }
                else{
                    return true;
                }
            }
           return true;
                }

          Space NewOpponentSpace(Space from, Space to){
            if(from.north == to){
                    return to.north;
                }
           else if(from.south == to){
                Debug.Log("Coming from south");
                return to.south;

                }
           else if(from.west == to){
                Debug.Log("Coming from west");
                return to.west;
                }
          else  if(from.east == to){
                return to.east;
                }
          else  if(from.northwest == to){
                return to.northwest;
                }
            
          else if(from.northeast == to){
                return to.northeast;
            }
          else  if(from.southwest == to){
                return to.southwest;
            }
          else{
                return to.southeast;
            }
            
                }          

            

    public override void PlaceWorker(PlayerWorker worker, Space space)
    {
        Debug.Log("Using Minotaur Place Worker");
        //If not at game start and there is an opposing worker at the space to move to
        if (worker.currentSpace && space.workerAtSpace)
        {
            worker.enabled=false;
            worker.currentSpace.workerAtSpace=null;
            GameManager.instance.MinotaurCharge.Play();
            BloodEffect.PlayEffectOnce(NewOpponentSpace(worker.currentSpace, space).spacePosition);
            base.PlaceWorker(space.workerAtSpace,NewOpponentSpace(worker.currentSpace,space));
           
            //Force the opposing worker to the space the current worker is occupying
            //worker.enabled = false;
            //base.PlaceWorker(space.workerAtSpace, worker.currentSpace);
            //Move the worker, don't use base function because current space's worker is not set to null because opposing worker was forced there
            space.workerAtSpace = worker;
            worker.currentSpace = space;
            // Calls helper method in the gameUI class to place worker
           
            GameManager.instance.gameUI.PlaceWorker(worker, space);
            
            worker.enabled = true;
            
        }
        else //Use default move if not
        {
            base.PlaceWorker(worker, space);
        }
    }
    




    }
