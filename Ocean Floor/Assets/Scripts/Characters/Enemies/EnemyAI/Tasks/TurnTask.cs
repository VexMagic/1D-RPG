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
        BaseAbility ability;
        public TurnTask(Character character, int targetIndex, BaseAbility ability) : base() 
        {
            this.targetIndex = targetIndex;
            this.character = character;
            this.ability = ability;
        }


        


        public override NodeState Evaluate()
        {
            targetIndex = (int)parent.GetData("targetPos");

            if (character.TilePos < targetIndex && character.facingLeft)
            {
                ability.ActivateAbility(2, character);
                ActionManager.instance.SetCurrentAbility(ability);
                ActionManager.instance.SpendActions();
            }
            else if (character.TilePos > targetIndex && !character.facingLeft)
            {
                ability.ActivateAbility(2, character);
                ActionManager.instance.SetCurrentAbility(ability);
                ActionManager.instance.SpendActions();
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