using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;
using static UnityEngine.RuleTile.TilingRuleOutput;

[CreateAssetMenu(fileName = "New BasicMovement", menuName = "Movement Type/BasicMovement")]
public class BasicMovement: MovementType
{
    

    // Update is called once per frame
    public override void Move(int index, int tilePos, Character character)
    {
        Character tempCharacter = CharacterManager.instance.GetCharacter(character.TilePos + index);
        if (tempCharacter != null)
        {
            base.SetPosition(character.TilePos, character);
        }

        base.SetPosition(character.TilePos + index, character);
    }
   
}
