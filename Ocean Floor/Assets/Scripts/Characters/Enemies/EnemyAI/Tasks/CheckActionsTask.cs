using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

namespace BehaviorTreeSpace
{
    public class CheckActionsTask: Node
    {
        BaseAbility ability;
        public CheckActionsTask(BaseAbility ability) : base() 
        {
            this.ability = ability;
        }
        public override NodeState Evaluate()
        {
            if(ActionManager.instance.CheckActionsLeft(ability))
            {
                return NodeState.success;
            }
            else
            {
                return NodeState.failure;
            }
        }

    }
}