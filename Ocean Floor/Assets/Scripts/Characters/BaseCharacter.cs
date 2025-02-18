using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    //Basic stats
    [SerializeField] private int health;
    [SerializeField] private int speed; //determines turn order
    [SerializeField] private int actions; //action points per turn
    [SerializeField] private float protection; //damage reduction (0f - 1f = 0% - 100%)
    [SerializeField] private float crit; //critical hit chance (0f - 1f = 0% - 100%)
    [SerializeField] private float dodge; //chance to take no damage (0f - 1f = 0% - 100%)
}


