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

            //loopen avbryts bara av en success eller running, detta betyder att den forstätter söka om den får en failure, eller forstätter köra noden om den är running
            //om en nod är success så returnerar även selector success, och kör bara igenom den noden
            //om alla noderna ger en failure så returnerar även selector failure, och går den vidare till nästa "syskon" nod
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    //om noden returnerar failure så forsätter den till nästa nod
                    case NodeState.failure:
                        continue;
                    //om noden returnerar success så returnerar även selector success och avslutar sökningen
                    case NodeState.success:
                        state = NodeState.success;
                        return state;
                    //running betyder att noden inte är klar och forsätter att köra
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