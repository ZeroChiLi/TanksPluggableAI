using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Complete;

public class StateController : MonoBehaviour
{

    public State currentState;                      //当前状态
    public EnemyStats enemyStats;                   //敌人状态
    public Transform eyes;                          //眼睛：拿来观察状态变化
    public State remainState;                       //保持当前状态


    [HideInInspector]
    public NavMeshAgent navMeshAgent;               //导航组件
    [HideInInspector]
    public Complete.TankShooting tankShooting;      //射击
    [HideInInspector]
    public List<Transform> wayPointList;            //所有巡逻点
    [HideInInspector]
    public int nextWayPotnt;                        //下一个巡逻点
    [HideInInspector]
    public Transform chaseTarget;                   //追踪目标
    [HideInInspector]
    public float stateTimeElapsed;                  //状态变化时间间隔

    private bool aiActive;                          //AI是否有效

    void Awake()
    {
        tankShooting = GetComponent<Complete.TankShooting>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    //设置巡逻点还有是否设置AI并且是否激活导航
    public void SetupAI(bool aiActivationFromTankManager, List<Transform> wayPointsFromTankManager)
    {
        wayPointList = wayPointsFromTankManager;
        aiActive = aiActivationFromTankManager;
        if (aiActive)
            navMeshAgent.enabled = true;
        else
            navMeshAgent.enabled = false;
    }

    private void Update()
    {
        if (!aiActive)
            return;
        currentState.UpdateState(this);     //更新状态
    }

    private void OnDrawGizmos()
    {
        if (currentState != null && eyes != null)
        {
            Gizmos.color = currentState.sceneGizmoColor;
            Gizmos.DrawWireSphere(eyes.position, enemyStats.lookSphereCastRadius);
        }
    }

    //转换到下一个状态
    public void TransitionToState(State nextState)
    {
        if(nextState != remainState)
        {
            currentState = nextState;
        }
    }

    //返回是否过了时间间隔
    public bool CheckIfCountDownElapsed(float duration)
    {
        stateTimeElapsed += Time.deltaTime;
        return (stateTimeElapsed >= duration);
    }

    private void OnExitState()
    {
        stateTimeElapsed = 0;
    }

}