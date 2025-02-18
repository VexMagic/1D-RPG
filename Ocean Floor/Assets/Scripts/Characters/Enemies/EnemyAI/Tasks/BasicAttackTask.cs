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
    public class BasicAttackTask: Node
    {
        Character target;
        Character character;
        int damage;
        BaseAbility ability;
        public BasicAttackTask(Character character, int damage, BaseAbility ability) : base() 
        {
            this.character = character;
            this.damage = damage;
            this.ability = ability;
        }

        public override NodeState Evaluate()
        {
            target = (Character)GetData("target");
            ActionManager.instance.SetCurrentAbility(ability);
            ability.ActivateAbility(target.TilePos, character);
            ActionManager.instance.SpendActions();
            return NodeState.success;
        }

    }
}