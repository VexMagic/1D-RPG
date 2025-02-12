using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Animations;

namespace BehaviorTreeSpace
{
    public class MoveTask : Node
    {
        int targetIndex;
        public MoveTask(int targetIndex) : base() 
        {
            this.targetIndex = targetIndex;
        }


        


        public override NodeState Evaluate()
        {
            return NodeState.success;
        }

    }
}