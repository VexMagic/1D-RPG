using UnityEngine;

namespace BehaviorTreeSpace
{
    
public class GetTargetTask : Node
    {
        Character target;
        int targetPos;
        int targetIndex;
        Character character;
        public GetTargetTask(Character character, int targetIndex) : base()
        {
            this.targetIndex = targetIndex;
            this.character = character;
        }

        public override NodeState Evaluate()
        {
            //search for target left and right of the enemy, and keep searching until it finds someone
            int searchRadius = 1;
            bool targetFound = false;

            while (!targetFound)
            {
                if(CharacterManager.instance.GetCharacter(character.TilePos + searchRadius) != null)
                {
                    target = CharacterManager.instance.GetCharacter(character.TilePos + searchRadius);
                    targetFound = true;
                }
                else if (CharacterManager.instance.GetCharacter(character.TilePos - searchRadius) != null)
                {
                    target = CharacterManager.instance.GetCharacter(character.TilePos - searchRadius);
                    targetFound = true;
                }
                else
                {
                    searchRadius++;
                }
            }
            Debug.Log("Target found at index: " + target.TilePos);
            parent.SetData("target", target);
            parent.SetData("targetPos", target.TilePos);
            return NodeState.success;
        }
    }
}
