using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Shoot", menuName = "Ability/Shoot")]
public class ShootAbility : BaseAbility
{
    [SerializeField] private int range = 1;
    [SerializeField] private int maxTargets = 1; //normally 1 but some abilities might pierce through enemies

    //make sure that the values can only be read and not changed
    public int Range {  get { return range; } }
    public int MaxTargets {  get { return maxTargets; } }
}
