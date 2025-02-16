using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AbilityType { Attack,Movement,Special,Heal};
[CreateAssetMenu(fileName = "New Ability", menuName = "Ability2")]

public class BaseAbility : ScriptableObject
{
     //Fields for all abilities
    [SerializeField] private string abilityName;    
    [SerializeField] private Sprite icon; 
    [SerializeField] private int cost; //how many action points the ability costs. 2 is standard

    public string Name { get { return abilityName; } }
    public Sprite Icon { get { return icon; } }
    public int Cost { get { return cost; } }

    [SerializeField] public AbilityType abilityType;
    //Unique Fields
    //Attack
    [HideInInspector] public int dmgAmount;
    [HideInInspector] public int dmgRange;
    //Movement
    [HideInInspector] public MovementType movementType;
    //Special
    //List of special effects
    //Heal
    [HideInInspector] public bool selfHeal;
    [HideInInspector] public int healAmount;
    [HideInInspector] public int healRange;
    //make sure that the values can only be read and not changed
    

    public void ActivateAbility(Character character)
    {
        switch (abilityType)
        {
            case AbilityType.Attack:
                break;
            case AbilityType.Movement:
                movementType.Move(movementType.movementRange, character.TilePos, character);
                break;
            case AbilityType.Special:
                break;
            case AbilityType.Heal:
                break;
        }
    }

    
    //some abilities might deal extra damage if a condition is met
    public virtual bool BonusDamage(int targetTile, int sourceTile)
    {
        return false;
    }
}
