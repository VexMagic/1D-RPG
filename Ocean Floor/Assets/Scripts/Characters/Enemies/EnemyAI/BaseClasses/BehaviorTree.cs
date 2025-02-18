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
        protected Character thisCharacter;

        protected void Start()
        {
            //h�mtar character komponenten fr�n gameobjectet
            thisCharacter = GetComponent<Character>();
            //rooten �r f�rsta noden i tr�det
            root = CreateTree();
        }

        public void Search()
        {
            //update k�r rootens evaluate, som d� k�r igenom tr�det
            if(root != null)
            {
                for(int i = 0; i < ActionManager.instance.ActionsLeft; i++)
                {
                    root.Evaluate();
                }
                
            }
        }
        //detta m�ste implementeras i subklassen eftersom det �r baserat p� vilka noder som ska anv�ndas
        protected abstract Node CreateTree();
    }
}