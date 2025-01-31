using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Strike", menuName = "Ability/Strike")]
public class StrikeAbility : BaseAbility
{
    [SerializeField] private bool aoE;  //target everyone within the range
    [SerializeField] private List<int> targets = new List<int>(); //all tiles relative to the character that can be targeted

    //make sure that the values can only be read and not changed
    public bool AoE { get { return aoE; } }
    public List<int> Targets { get { return targets; } }
}
