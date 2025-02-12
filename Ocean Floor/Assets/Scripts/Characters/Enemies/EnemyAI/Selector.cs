using BehaviorTreeSpace;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Metadata;

namespace BehaviorTreeSpace
{
    public class Selector : Node
    {
        public Selector() : base() { }
        public Selector(List<Node> children) : base(children) { }
        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            //loopen avbryts bara av en success eller running, detta betyder att den forst�tter s�ka om den f�r en failure, eller forst�tter k�ra noden om den �r running
            //om en nod �r success s� returnerar �ven selector success, och k�r bara igenom den noden
            //om alla noderna ger en failure s� returnerar �ven selector failure, och g�r den vidare till n�sta "syskon" nod
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    //om noden returnerar failure s� fors�tter den till n�sta nod
                    case NodeState.failure:
                        continue;
                    //om noden returnerar success s� returnerar �ven selector success och avslutar s�kningen
                    case NodeState.success:
                        state = NodeState.success;
                        return state;
                    //running betyder att noden inte �r klar och fors�tter att k�ra
                    case NodeState.running:
                        state = NodeState.running;
                        return state;
                    default:
                        continue;

                }
            }
            state = NodeState.failure;
            return state;
        }
    }

}