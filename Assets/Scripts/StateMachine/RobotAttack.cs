using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class RobotAttack : RobotState
{
   private float attackCooldown = 3f;
   private float lastAttackTime;

   public RobotAttack(RobotStateController rsc) : base(rsc)
   {
       
   }

   public override void OnStateEnter()
   {
      rsc.scanner.GetComponent<Renderer>().material = rsc.red;
      rsc.GetComponent<NavMeshAgent>().isStopped = false;
      lastAttackTime = Time.time - attackCooldown;
   }

   public override void CheckTransitions() // transition from chase to attack state after a certain distance
   {
      float distanceToPlayer = Vector3.Distance(rsc.transform.position, rsc.player.transform.position);
        
        if (rsc.health <= rsc.fleeHealth)
        {
            rsc.SetState(new RobotFlee(rsc));
        }

        else if (distanceToPlayer > rsc.attackRange && distanceToPlayer <= rsc.detectionRange)
        {
            rsc.SetState(new RobotChase(rsc));
        }
        
        else if (distanceToPlayer > rsc.detectionRange)
        {
            rsc.SetState(new RobotWorking(rsc));
        }
   }

   public override void Act()
   {
      if (Time.time >= lastAttackTime + attackCooldown)
      {
         Collider[] hitColliders = Physics.OverlapSphere(rsc.transform.position, rsc.attackRange);
         foreach (var hitCollider in hitColliders)
         {
            if (hitCollider.gameObject.tag == "Player")
            {
               PlayerHealth playerHealth = hitCollider.GetComponent<PlayerHealth>();
               if (playerHealth != null && !playerHealth.isDead)
               {
                  playerHealth.TakeDamage(rsc.attackDamage);
                  lastAttackTime = Time.time;
               }
            }
         } 
      }
   }

   public override void OnStateExit()
   {

   }
}

