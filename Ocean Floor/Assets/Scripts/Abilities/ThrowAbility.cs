using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Throw", menuName = "Ability/Throw")]
public class ThrowAbility : BaseAbility
{
    [SerializeField] private int range = 1;
    [SerializeField] private int minRange = 1;
    [SerializeField] private bool canTargetEmpty;
    [SerializeField] private bool omniDirectional;
    [SerializeField] private bool aoE;

    public int Range { get { return range; } }
    public int MinRange { get { return minRange; } }
    public bool CanTargetEmpty { get {  return canTargetEmpty; } }
    public bool OmniDirectional { get {  return omniDirectional; } }
    public bool AoE { get {  return aoE; } }
}
