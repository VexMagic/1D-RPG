using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : ScriptableObject
{
    [SerializeField] private int damage;
    [SerializeField] private Sprite icon; 
    [SerializeField] private int cost; //how many action points the ability costs. 2 is standard

    //make sure that the values can only be read and not changed
    public int Damage { get { return damage; } }
    public Sprite Icon { get { return icon; } }
    public int Cost { get { return cost; } }

    //some abilities might deal extra damage if a condition is met
    public virtual bool BonusDamage(int targetTile, int sourceTile)
    {
        return false;
    }
}
