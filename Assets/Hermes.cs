using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class Hermes : GodCard
    {
        public List<Space> AdjToWorkerSpace;
        
        void Start()
        {
            name = "Hermes";

        }

        public override List<Space> GetAvailableMoves(PlayerWorker worker)
            {
            List<Space> finalOutput;
            Space starting = worker.currentSpace;           //starting spot of worker
            AdjToWorkerSpace = base.GetAvailableMoves(worker);
            List<Space> Legal = new List<Space>();          //Legal set of avaiable moves for hermes
            FloodFill(starting,Legal);
            Legal.RemoveAll(temp=>temp.workerAtSpace!=null);                //remove workers
            Legal.RemoveAll(temp=>temp.blockedByDome);

            finalOutput= AdjToWorkerSpace.Union(Legal).ToList();  
            return finalOutput;
            }

        void FloodFill(Space starting, List<Space>Legal)
            {
                Queue<Space> Q = new Queue<Space>();
                FloorLevel targetFloor=starting.currentFloorLevel;
                Legal.Add(starting);

                Q.Enqueue(starting);

                while (Q.Count > 0) {
                    Space n = Q.Dequeue();                              //remove first element from Q
                    Space candidate;
                    if (n.west!=null&& n.west.workerAtSpace==null) {                                   //we keep moving west if possible and not blocked
                         candidate = n.west;
                        if (candidate.currentFloorLevel == targetFloor && !Legal.Contains(candidate) ) {
                            Legal.Add(candidate);
                            Q.Enqueue(candidate);
                        }
                    }
                    if (n.east!=null&&  n.east.workerAtSpace==null) {                               //if east not null
                         candidate = n.east;
                        if (candidate.currentFloorLevel == targetFloor && !Legal.Contains(candidate)) {
                            Legal.Add(candidate);
                            Q.Enqueue(candidate);
                            } 
                    }
                    if (n.north!=null &&  n.north.workerAtSpace==null) {
                         candidate= n.north;
                         if(candidate.currentFloorLevel==targetFloor && !Legal.Contains(candidate)){
                              Legal.Add(candidate);
                              Q.Enqueue(candidate);
                            }
                        }
                    if(n.south!=null &&  n.south.workerAtSpace==null){
                        candidate = n.south;
                        if(candidate.currentFloorLevel==targetFloor && !Legal.Contains(candidate)){
                                Legal.Add(candidate);
                                Q.Enqueue(candidate);
                            }
                        }
                    if(n.northwest!=null &&  n.northwest.workerAtSpace==null){
                         candidate = n.northwest;
                        if(candidate.currentFloorLevel==targetFloor && !Legal.Contains(candidate)){
                            Legal.Add(candidate);
                            Q.Enqueue(candidate);
                            }
                        }
                    if(n.northeast!=null &&  n.northeast.workerAtSpace==null){
                        candidate = n.northeast;
                        if(candidate.currentFloorLevel==targetFloor && !Legal.Contains(candidate)){
                            Legal.Add(candidate);
                            Q.Enqueue(candidate);
                            }
                        }
                    if(n.southwest!=null &&  n.southwest.workerAtSpace==null){
                        candidate = n.southwest;
                        if(candidate.currentFloorLevel==targetFloor && !Legal.Contains(candidate)){
                            Legal.Add(candidate);
                            Q.Enqueue(candidate);
                            }
                        }
                    if(n.southeast!=null &&  n.southeast.workerAtSpace==null){
                        candidate = n.southeast;
                        if(candidate.currentFloorLevel==targetFloor && !Legal.Contains(candidate)){
                            Legal.Add(candidate);
                            Q.Enqueue(candidate);
                            }
                        }
                }
            }


    }