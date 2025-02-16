using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Turn Movement", menuName = "Movement Type/Turn")]
public class Turn : MovementType
{
    public override void Move(int index, int tilePos, Character character)
    {
        character.facingLeft = !character.facingLeft;

        if (character.facingLeft)
        {
            character.body.transform.eulerAngles = new Vector3(0, 180);
        }
        else
        {
            character.body.transform.eulerAngles = new Vector3(0, 0);
        }
    }
}
