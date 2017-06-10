using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "PluggableAI/Decisions/Look")]
public class LookDecision : Decision 
{
    public override bool Decide(StateController controller)
    {
        bool targetVisble = Look(controller);
        return targetVisble;
    }
    
    //巡逻检测
    private bool Look(StateController controller)
    {
        RaycastHit hit;

        Debug.DrawRay(controller.eyes.position, controller.eyes.forward.normalized * controller.enemyStats.lookRange, Color.green);

        //射出一球体的射线检测周围是否有Player
        if (Physics.SphereCast(controller.eyes.position,controller.enemyStats.lookSphereCastRadius,controller.eyes.forward,out hit,controller.enemyStats.lookRange) && hit.collider.CompareTag("Player"))
        {
            controller.chaseTarget = hit.transform;
            return true;
        }
        else
        {
            return false;
        }
    }
}
