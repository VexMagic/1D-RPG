using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTreeSpace;
public class BasicEnemyBT : BehaviorTree
{

    protected override Node CreateTree()
    {
        //h�r v�ljar jag noderna av behavior tr�det
        //f�rst �r rooten som �r en selector, den v�ljer mellan dens barn
        //detta betyder att bara en av dens barn kommer k�ras igenom helt
        Node root = new Selector(new List<Node>
        {
            //the sequence that moves the enemy to the target
            new Sequence(new List<Node>
            {
                new GetTargetTask(thisCharacter, 0),

                new TurnTask(thisCharacter, targetIndex),
            }),
            new Sequence(new List<Node>
            {
                new GetTargetTask(thisCharacter, 0),
                //om basic enemy har f�r l�gt HP flyttar den sig och sen healar
                new MoveTask(thisCharacter, targetIndex),

            }),
            //the sequence that turns the enemy to face the target
            //the sequence that attacks the target
            new Sequence(new List<Node>
            {
                new GetTargetTask(thisCharacter, 0),
                new BasicAttackTask(thisCharacter, 1),
            }),
        });
        return root;
    }

}