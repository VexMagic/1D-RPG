using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;


public class MovementType : ScriptableObject
{
    public int movementRange;

    public virtual void Move(int index, int tilePos, Character character)
    {

    }
    public virtual void SetPosition(int index, Character character)
    {
        character.TilePos = index;
        character.transform.position = new Vector3(TileManager.instance.GetTilePos(index), character.transform.position.y);
    }
}
