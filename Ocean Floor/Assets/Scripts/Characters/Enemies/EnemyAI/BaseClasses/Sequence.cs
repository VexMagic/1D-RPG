using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreeSpace
{

    public class Sequence : Node
    {
        public Sequence() : base() { } 
        public Sequence(List<Node> children) : base(children) { }
        public override NodeState Evaluate()
        {
            bool anyChildIsRunning = false;

            //går igenom alla child nodes, om en nod är failure så returnerar även sequence failure och avbryter
            //om en nod är success så forsätter den till nästa nod
            //om en nod är running så forsätter den att köra noden
            //om all noderna är success så returnerar även sequence success
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    //om evaluate returnerar failure så returnerar även sequence failure och avbryter
                    case NodeState.failure:
                        state = NodeState.failure;
                        return state;
                        case NodeState.success:
                        continue;
                    //om evaluate returnerar running så sätts state till running och loopen fortsätter
                    case NodeState.running:
                        anyChildIsRunning = true;
                        continue;
                    //om något annat returneras så tar den det som success och forsätter
                    default:
                        state = NodeState.success;
                        return state;
                        
                }
            }
            if (anyChildIsRunning)
                state = NodeState.running;
            else
                state = NodeState.success;

            return state;
        }
    }

}