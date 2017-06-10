using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//包含要执行动作，下一个动作的决定
[CreateAssetMenu (menuName = "PluggableAI/State")]
public class State : ScriptableObject 
{
    public Action[] actions;                        //动作：巡逻动作、
    public Transition[] transitions;                //同决定，选择两种状态其中之一。
    public Color sceneGizmoColor = Color.gray;      //拿来渲染eyes的Gizmos颜色

    public void  UpdateState(StateController controller)
    {
        DoActions(controller);                      //执行动作
        CheckTransition(controller);                //检测转换状态
    }

    private void DoActions(StateController controller)
    {
        for (int i = 0; i < actions.Length; i++)
        {
            actions[i].Act(controller);
        }
    }

    private void CheckTransition(StateController controller)
    {
        //检查所有转换状态，并改变状态
        for (int i = 0; i < transitions.Length; i++)
        {
            bool decisionSucceeded = transitions[i].decision.Decide(controller);

            if (decisionSucceeded)
                controller.TransitionToState(transitions[i].trueState);
            else
                controller.TransitionToState(transitions[i].falseState);
        }
    }

}
