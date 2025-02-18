using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace BehaviorTreeSpace
{
    public enum NodeState
    {
        running,
        success,
        failure
    }

    public class Node
    {
        protected NodeState state;

        protected Node parent;
        protected List<Node> children = new List<Node>();

        private Dictionary<string, object> sharedData = new Dictionary<string, object>();

        public Node()
        {
            parent = null;

        }
        public Node(List<Node> children)
        {
            foreach (Node child in children)
            {
                Attach(child);
            }
        }
        public void Attach(Node node)
        {
            node.parent = this;
            children.Add(node);
        }


        public virtual NodeState Evaluate() => NodeState.failure;

        public void SetData(string key, object value)
        {
            sharedData[key] = value;
        }

        public object GetData(string key)
        {
            object value = null;
            if (sharedData.TryGetValue(key, out value))
                return value;

            Node node = parent;
            while (node != null)
            {
                value = node.GetData(key);
                if(value != null) 
                    return value;
                node = node.parent;
            }
            return null;
        }    
        public bool ClearData(string key)
        {
            object value = null;
            if (sharedData.ContainsKey(key))
            {
                sharedData.Remove(key);
                return true;
            }
 
            Node node = parent;
            while (node != null)
            {
                bool cleared = node.ClearData(key);
                if(cleared) 
                    return true;
                node = node.parent;
            }
            return false;
        }
    }

}