using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BehaviorTreeSpace
{
    public abstract class BehaviorTree : MonoBehaviour
    {
        //parent classen till behavior tree
        protected int targetIndex = 0;
        protected int targetPos = 0;
        private Node root = null;

        protected void Start()
        {
            //rooten är första noden i trädet
            root = CreateTree();
        }

        private void Update()
        {
            //update kör rootens evaluate, som då kör igenom trädet
            if(root != null)
            {
                root.Evaluate();
            }
        }
        //detta måste implementeras i subklassen eftersom det är baserat på vilka noder som ska användas
        protected abstract Node CreateTree();
    }
}