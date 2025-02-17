using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTreeSpace;
public class BasicEnemyBT : BehaviorTree
{

    protected override Node CreateTree()
    {
        //här väljar jag noderna av behavior trädet
        //först är rooten som är en selector, den väljer mellan dens barn
        //detta betyder att bara en av dens barn kommer köras igenom helt
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
                //om basic enemy har för lågt HP flyttar den sig och sen healar
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