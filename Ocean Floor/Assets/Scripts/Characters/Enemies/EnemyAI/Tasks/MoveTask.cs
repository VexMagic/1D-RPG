using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Animations;

namespace BehaviorTreeSpace
{
    public class MoveTask : Node
    {
        int targetIndex;
        Character character;
        public MoveTask(Character character, int targetIndex) : base() 
        {
            this.targetIndex = targetIndex;
            this.character = character;
        }


        


        public override NodeState Evaluate()
        {
            targetIndex = (int)parent.GetData("targetPos");
            
            if(character.TilePos != targetIndex + 1 && character.TilePos != targetIndex - 1)
            {
                ////move
                //if (character.TilePos < targetIndex)
                //{
                //    character.EnemyMove(character.TilePos + 1);
                //}
                //else
                //{
                //    character.EnemyMove(character.TilePos - 1);
                //}
            }
            else
            {
               return NodeState.failure;
            }
            Debug.Log("Moving to target");
            return NodeState.success;
        }

    }
}