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
        public BasicAttackTask(Character character, int damage) : base() 
        {
            this.character = character;
            this.damage = damage;
        }

        public override NodeState Evaluate()
        {
            target = (Character)GetData("target");
            target.TakeDamage(damage);
            return NodeState.success;
        }

    }
}