using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseAbility : ScriptableObject
{
    [SerializeField] private int damage;
    [SerializeField] private Sprite sprite;
    [SerializeField] private int cost;

    public int Damage { get { return damage; } }
    public Sprite Sprite { get { return sprite; } }
    public int Cost { get { return cost; } }

    public virtual bool BonusDamage(int targetTile, int sourceTile)
    {
        return false;
    }
}
