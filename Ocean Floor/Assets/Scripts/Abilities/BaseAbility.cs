using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability", menuName = "Ability")]

public class BaseAbility : ScriptableObject
{
     //Fields for all abilities
    [SerializeField] private string abilityName;    
    [SerializeField] private Sprite icon; 
    [SerializeField] private int cost; //how many action points the ability costs. 2 is standard

    [SerializeField] private bool canTargetEmpty = true;

    [SerializeField] private int abilityRange;
    [SerializeField] private int abilityMinRange;
    [SerializeField] private TargetingType targetingType;
    [Tooltip("Can this ability be used in both direction or only facing forward")]
    [SerializeField] private bool omniDirectional;
    [SerializeField] private bool isAoE;
    private AbilityTargeting abilityTargeting;
    
    public string Name { get { return abilityName; } }
    public Sprite Icon { get { return icon; } }
    public int Cost { get { return cost; } }

    [SerializeField] private bool attackAbility;
    [SerializeField] private bool movementAbility;
    [SerializeField] private bool specialAbility;
    [SerializeField] private bool healAbility;
  
    //Unique Fields
    //Attack
    [SerializeField] private int dmgAmount;
    [SerializeField] private int accuracy;
    //Movement
    [SerializeField] private MovementType movementType;
    //Special
    //List of special effects
    //Heal
    [SerializeField] private int healAmount;
    //make sure that the values can only be read and not changed
    
    public void GetAbilityTargets(Character character)
    {
       
        abilityTargeting = new AbilityTargeting();
        abilityTargeting.targetingType = targetingType;
        abilityTargeting.omniDirectional = omniDirectional;
        abilityTargeting.abilityRange = abilityRange;
        abilityTargeting.minAbilityRange = abilityMinRange;
        abilityTargeting.isAoE = isAoE;
        abilityTargeting.GetAvaialbleTargets(character);
    }
    public void ActivateAbility(int index,Character character )
    {
        if (attackAbility)
        {
            //FOR AOE DAMAGE
            //foreach (var item in TileManager.instance.TileData) //target all highlighted tiles
            //{
            //    if (item.IsHighlighted)
            //    {
            //        //check if there is a character on the tile before dealing damage
            //        Character tempCharacter = CharacterManager.instance.GetCharacter(item.Index);
            //        if (tempCharacter != null)
            //        {

            //        }
            //    }
            //}

            DealDamage(index);
        }
        if (movementAbility)
        {
            movementType.Move(index, character.TilePos, character);
        }
        if (healAbility)
        { }
        if (specialAbility)
        { }

    }

    //some abilities might deal extra damage if a condition is met
    public virtual bool BonusDamage(int targetTile, int sourceTile)
    {
        return false;
    }

    public void DealDamage(int index)
    {
        //check if there is a character on the tile before dealing damage
        Character tempCharacter = CharacterManager.instance.GetCharacter(index);
        if (tempCharacter != null)
            tempCharacter.TakeDamage(ActionManager.instance.CurrentAbility.dmgAmount);
    }
}
