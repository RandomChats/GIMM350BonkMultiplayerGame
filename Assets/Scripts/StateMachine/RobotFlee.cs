using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotFlee : RobotState
{
   public RobotFlee(RobotStateController rsc) : base(rsc)
   {
        rsc.scanner.GetComponent<Renderer>().material = rsc.yellow;
        rsc.GetComponent<NavMeshAgent>().isStopped = false;
   }

   public override void OnStateEnter()
   {
       
   }

   public override void CheckTransitions() // when low on health transition to flee state from attack state
   {
      
   }

   public override void Act()
   {
      Vector3 directionAwayFromPlayer = rsc.transform.position - rsc.player.transform.position;
      Vector3 fleeDestination = rsc.transform.position + directionAwayFromPlayer;

      rsc.GetComponent<NavMeshAgent>().SetDestination(fleeDestination);  
   }

   public override void OnStateExit()
   {

   }
}

