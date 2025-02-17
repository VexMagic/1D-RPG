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

            //g�r igenom alla child nodes, om en nod �r failure s� returnerar �ven sequence failure och avbryter
            //om en nod �r success s� fors�tter den till n�sta nod
            //om en nod �r running s� fors�tter den att k�ra noden
            //om all noderna �r success s� returnerar �ven sequence success
            foreach (Node node in children)
            {
                switch (node.Evaluate())
                {
                    //om evaluate returnerar failure s� returnerar �ven sequence failure och avbryter
                    case NodeState.failure:
                        state = NodeState.failure;
                        return state;
                        case NodeState.success:
                        continue;
                    //om evaluate returnerar running s� s�tts state till running och loopen forts�tter
                    case NodeState.running:
                        anyChildIsRunning = true;
                        continue;
                    //om n�got annat returneras s� tar den det som success och fors�tter
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