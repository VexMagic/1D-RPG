using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Strike", menuName = "Ability/Strike")]
public class StrikeAbility : BaseAbility
{
    [SerializeField] private bool aoE;
    [SerializeField] private List<int> targets = new List<int>();

    public bool AoE { get { return aoE; } }
    public List<int> Targets { get { return targets; } }
}
