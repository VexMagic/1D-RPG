using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Throw", menuName = "Ability/Throw")]
public class ThrowAbility : BaseAbility
{
    [SerializeField] private int range = 1;
    [SerializeField] private int minRange = 1;
    [SerializeField] private bool canTargetEmpty; //useful for movement based abilities or AoE
    [SerializeField] private bool omniDirectional; //ignore characters direction when targeting
    [SerializeField] private bool aoE; //target everyone within the range

    //make sure that the values can only be read and not changed
    public int Range { get { return range; } }
    public int MinRange { get { return minRange; } }
    public bool CanTargetEmpty { get {  return canTargetEmpty; } } 
    public bool OmniDirectional { get {  return omniDirectional; } }
    public bool AoE { get {  return aoE; } }
}
