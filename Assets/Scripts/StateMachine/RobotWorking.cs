using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotWorking : RobotState
{
   public RobotWorking(RobotStateController rsc) : base(rsc)
   {

   }

   public override void OnStateEnter()
   {
        rsc.scanner.GetComponent<Renderer>().material = rsc.green;
        rsc.GetComponent<NavMeshAgent>().isStopped = false;
        MoveToNextDestination();
   }

   public override void CheckTransitions()
   {
        //chasing
        float distanceToPlayer = Vector3.Distance(rsc.transform.position, rsc.player.transform.position);
        if (distanceToPlayer <= rsc.detectionRange)
        {
            rsc.scanner.transform.LookAt(rsc.player.transform.position);
            RaycastHit hit;
            if (Physics.Raycast(rsc.scanner.transform.position, rsc.scanner.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if (hit.collider.gameObject.tag == "Player")
                {
                    rsc.SetState(new RobotChase(rsc));
                    return;
                }
            }
        }

            //sleeping
        if (!rsc.GetComponent<NavMeshAgent>().pathPending && rsc.GetComponent<NavMeshAgent>().remainingDistance <= rsc.GetComponent<NavMeshAgent>().stoppingDistance)
        {
            if (!rsc.GetComponent<NavMeshAgent>().hasPath || rsc.GetComponent<NavMeshAgent>().velocity.sqrMagnitude == 0f)
            {
                rsc.SetState(new RobotSleeping(rsc));
                return;
            }   
        }
   }

   public override void Act()
   {
        if (!rsc.GetComponent<NavMeshAgent>().pathPending && rsc.GetComponent<NavMeshAgent>().remainingDistance <= rsc.GetComponent<NavMeshAgent>().stoppingDistance)
        {
            if (!rsc.GetComponent<NavMeshAgent>().hasPath || rsc.GetComponent<NavMeshAgent>().velocity.sqrMagnitude == 0f)
            {
                rsc.SetState(new RobotSleeping(rsc));
                return;
            }
        }
        else
        {
            rsc.GetComponent<NavMeshAgent>().destination = rsc.currentDestination.position;
        }
   }

   public override void OnStateExit()
   {

   }

   private void MoveToNextDestination()
   {
       rsc.currentDestination = rsc.RandomDestination();
       if (rsc.currentDestination != null)
       {
           rsc.GetComponent<NavMeshAgent>().SetDestination(rsc.currentDestination.position);
       }
   }
}
