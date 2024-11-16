using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotChase : RobotState
{
     public RobotChase(RobotStateController rsc) : base(rsc)
     {

     }

     public override void OnStateEnter()
     {
          rsc.scanner.GetComponent<Renderer>().material = rsc.orange;
          rsc.GetComponent<NavMeshAgent>().isStopped = false;
          Debug.Log("Chase state");
     }

     public override void CheckTransitions() // transition from working/attack to chase state
     {
          float distanceToPlayer = Vector3.Distance(rsc.transform.position, rsc.player.transform.position);
          PlayerHealth playerHealth = rsc.player.GetComponent<PlayerHealth>();
          if (playerHealth != null && playerHealth.isDead)
          {
               rsc.SetState(new RobotWorking(rsc));
          }

          if (distanceToPlayer < rsc.attackRange)
          {
               rsc.SetState(new RobotAttack(rsc));
          }

          else if (distanceToPlayer > rsc.detectionRange)
          {
               rsc.SetState(new RobotWorking(rsc));
          }

     }

     public override void Act()
     {
          if (rsc.GetComponent<NavMeshAgent>() != null && rsc.player != null) 
          {
               rsc.GetComponent<NavMeshAgent>().SetDestination(rsc.player.transform.position);
          }
     }

     public override void OnStateExit()
     {

     }
}

