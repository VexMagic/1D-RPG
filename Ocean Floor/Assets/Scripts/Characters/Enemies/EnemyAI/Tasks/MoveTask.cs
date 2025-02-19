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
        BaseAbility ability;
        public MoveTask(Character character, int targetIndex, BaseAbility ability) : base() 
        {
            this.targetIndex = targetIndex;
            this.character = character;
            this.ability = ability;
        }
        public override NodeState Evaluate()
        {
            targetIndex = (int)parent.GetData("targetPos");
            
            if(character.TilePos != targetIndex + 1 && character.TilePos != targetIndex - 1)
            {
                //move
                if (character.TilePos < targetIndex)
                {
                    ability.ActivateAbility(character.TilePos + 1, character);
                    ActionManager.instance.SetCurrentAbility(ability);
                    ActionManager.instance.SpendActions();
                }
                else
                {
                    ability.ActivateAbility(character.TilePos - 1, character);
                    ActionManager.instance.SetCurrentAbility(ability);
                    ActionManager.instance.SpendActions();

                }
            }
            else
            {
               return NodeState.failure;
            }

            Debug.Log("Moving to target");

            ActionManager.instance.SpendActions(ability);
            return NodeState.success;
        }

    }
}