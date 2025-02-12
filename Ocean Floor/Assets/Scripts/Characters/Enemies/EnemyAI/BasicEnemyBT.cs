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
            //h�r �r en sequence, den kommer g�ra dens barn i ordning
            new Sequence(new List<Node>
            {
                
                //om basic enemy har f�r l�gt HP flyttar den sig och sen healar
                new MoveTask(targetIndex),

            }),
            
            new Sequence(new List<Node>
            {
                new MoveTask(targetIndex),
            }),
        });
        return root;
    }

}