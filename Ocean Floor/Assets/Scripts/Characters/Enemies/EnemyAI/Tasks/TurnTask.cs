using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.IO;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEditor;
using UnityEngine;
using UnityEngine.Animations;

namespace BehaviorTreeSpace
{
    public class TurnTask : Node
    {
        int targetIndex;
        Character character;
        public TurnTask(Character character, int targetIndex) : base() 
        {
            this.targetIndex = targetIndex;
            this.character = character;
        }


        


        public override NodeState Evaluate()
        {
            targetIndex = (int)parent.GetData("targetPos");

            if (character.TilePos < targetIndex && character.FacingLeft)
            {
                character.EnemyTurnAround();
            }
            else if (character.TilePos > targetIndex && !character.FacingLeft)
            {
                character.EnemyTurnAround();
            }
            else
            {
                return NodeState.failure;
            }
                Debug.Log("Turning to target");
            return NodeState.success;
        }

    }
}