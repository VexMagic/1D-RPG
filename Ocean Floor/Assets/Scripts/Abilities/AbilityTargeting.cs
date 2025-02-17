using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;
using UnityEditor.Playables;

public enum TargetingType { MovementTargeting, MeleeTargeting, RangedTargeting, MinimumRangedTargeting, SelfTargeting };

public class AbilityTargeting 
{
    public bool targetingEmpty;
    public TargetingType targetingType;
    public bool omniDirectional;
    List<int> targetTileList = new List<int>();
    public int abilityRange;
    public int minAbilityRange;
    public bool isAoE;
    public void GetAvaialbleTargets(Character character)
    {
        targetTileList.Clear();
        switch (targetingType)
        {
            case TargetingType.MovementTargeting:
                MovementTargeting(character);
                break;
            case TargetingType.MeleeTargeting:
                MeleeTargeting(character);
                break;
            case TargetingType.RangedTargeting:
                RangedTargeting(character);
                break;
            case TargetingType.MinimumRangedTargeting:
                MinimumRangedTargeting(character);
                break;
            case TargetingType.SelfTargeting:
                SelfTargeting(character);
                break;
        }

        TileManager.instance.HighlightTiles(targetTileList, isAoE);
    }

    private void MovementTargeting(Character character)
    {
        targetTileList.Add(character.TilePos - abilityRange);
        targetTileList.Add(character.TilePos + abilityRange);
    }
    private void MeleeTargeting(Character character)
    {
        int tempTile = character.TilePos;
        if (omniDirectional)
        {
           
            Character tempCharacter1 = CharacterManager.instance.GetCharacter(tempTile - 1);
            if (tempCharacter1 != null)
            {
                targetTileList.Add(tempTile);
            }
            tempCharacter1 = CharacterManager.instance.GetCharacter(tempTile +1 );
            if (tempCharacter1 != null)
            {
                targetTileList.Add(tempTile);
            }
        }
        else
        {
            if (character.facingLeft)
                tempTile -= 1;
            else
                tempTile += 1;

            Character tempCharacter = CharacterManager.instance.GetCharacter(tempTile);
            if (tempCharacter != null)
            {
                targetTileList.Add(tempTile);
            }

        }
    }
    private void RangedTargeting(Character character)
    {
        for (int i = 1; i <= abilityRange; i++) //loop through all tiles in a direction until the max targets or max range is reached
        {
            int tempTile = character.TilePos;
            if (omniDirectional)
            {
                Character tempCharacter1 = CharacterManager.instance.GetCharacter(tempTile - i);
                if (tempCharacter1 != null)
                {
                    targetTileList.Add(tempTile);
                }
                tempCharacter1 = CharacterManager.instance.GetCharacter(tempTile + i);
                if (tempCharacter1 != null)
                {
                    targetTileList.Add(tempTile);
                }

            }
            else
            {
                if (character.facingLeft)
                    tempTile -= i;
                else
                    tempTile += i;

                Character tempCharacter = CharacterManager.instance.GetCharacter(tempTile);
                if (tempCharacter != null)
                {
                    targetTileList.Add(tempTile);
                }
            }
           
        }
    }
    private void MinimumRangedTargeting(Character character)
    {
        for (int i = minAbilityRange; i <= abilityRange; i++) //loop through all tiles in a direction until the max targets or max range is reached
        {
            int tempTile = character.TilePos;
            if (omniDirectional)
            {
                
                Character tempCharacter1 = CharacterManager.instance.GetCharacter(tempTile+i);
                if (tempCharacter1 != null)
                {
                    targetTileList.Add(tempTile);
                }
                tempCharacter1 = CharacterManager.instance.GetCharacter(tempTile-i);
                if (tempCharacter1 != null)
                {
                    targetTileList.Add(tempTile);
                }

            }
            else
            {
                if (character.facingLeft)
                    tempTile -= i;
                else
                    tempTile += i;

                Character tempCharacter = CharacterManager.instance.GetCharacter(tempTile);
                if (tempCharacter != null)
                {
                    targetTileList.Add(tempTile);
                }
            }

        }
    }
    private void SelfTargeting(Character character)
    {
        targetTileList.Add(character.TilePos);
    }
}
