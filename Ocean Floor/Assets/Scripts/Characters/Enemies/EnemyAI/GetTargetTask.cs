using UnityEngine;

namespace BehaviorTreeSpace
{
    
public class GetTargetTask : Node
    {
        Character target;
        int targetPos;
        int targetIndex;

        public GetTargetTask(int targetIndex) : base()
        {
            this.targetIndex = targetIndex;
        }

        public override NodeState Evaluate()
        {
            //search for target left and right of the enemy, and keep searching until it finds someone
            int searchRadius = 1;
            bool targetFound = false;

            while (!targetFound)
            {
                // Check left
                target = CharacterManager.instance.GetCharacter(targetIndex - searchRadius);
                if (targetPos != -1)
                {
                    targetFound = true;
                    targetIndex -= searchRadius;
                    break;
                }

                // Check right
                target = CharacterManager.instance.GetCharacter(targetIndex + searchRadius);
                if (targetPos != -1)
                {
                    targetFound = true;
                    targetIndex += searchRadius;
                    break;
                }

                searchRadius++;
            }

            parent.SetData("target", targetIndex);
            parent.SetData("targetPos", targetPos);
            return NodeState.success;
        }
    }
}
