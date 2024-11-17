using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotSleeping : RobotState
{
  public RobotSleeping(RobotStateController rsc) : base(rsc)
  {

  }

  public override void OnStateEnter()
  {
    rsc.scanner.GetComponent<Renderer>().material = rsc.blue;
    rsc.GetComponent<NavMeshAgent>().isStopped = true;
    rsc.pauseTimer = rsc.pauseDuration;
    Debug.Log("Entering Sleeping State. Pause Timer: " + rsc.pauseTimer);
  }

  public override void CheckTransitions() //change to idle state (timer for destination spot)
  {
    //sleeping
    if (rsc.pauseTimer <= 0f)
    {
      rsc.SetState(new RobotWorking(rsc));
      return;
    }

    //chasing
    rsc.scanner.transform.LookAt(rsc.player.transform.position);
    RaycastHit hit;
    if (Physics.Raycast(rsc.scanner.transform.position, rsc.scanner.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
    {
      if (hit.collider.gameObject.name == "Player")
      {
        rsc.SetState(new RobotChase(rsc));
        return;
      }
    }
  }

  public override void Act()
  {
    rsc.pauseTimer -= Time.deltaTime;
  }

  public override void OnStateExit()
  {
    Debug.Log("Exiting Sleeping State");
  }
}
