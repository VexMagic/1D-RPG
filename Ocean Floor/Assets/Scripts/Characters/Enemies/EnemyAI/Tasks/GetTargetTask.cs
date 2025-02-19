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

            for (int i = 0; i < TileManager.instance.tiles.Count; i++)
            {
                if (character.TilePos + searchRadius <= TileManager.instance.tiles.Count)
                {

                    if (CharacterManager.instance.GetCharacter(character.TilePos + searchRadius) != null)
                    {
                        target = CharacterManager.instance.GetCharacter(character.TilePos + searchRadius);
                        targetFound = true;
                    }
                }
                if (character.TilePos - searchRadius >= 0)
                {
                    if (CharacterManager.instance.GetCharacter(character.TilePos - searchRadius) != null)
                    {
                        target = CharacterManager.instance.GetCharacter(character.TilePos - searchRadius);
                        targetFound = true;
                    }
                }
                if (targetFound)
                {
                    parent.SetData("target", target);
                    parent.SetData("targetPos", target.TilePos);
                    return NodeState.success;
                 }
                else
                {
                    searchRadius++;
                }
            }
            return NodeState.failure;
        }
    }
}

