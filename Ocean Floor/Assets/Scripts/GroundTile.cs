using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    [SerializeField] private SpriteRenderer renderer;

    int index;
    bool isHighlighted;

    public int Index { get { return index; } }
    public bool IsHighlighted { get { return isHighlighted; } }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    public void Highlight(bool highlight, bool AoE)
    {
        isHighlighted = highlight;

        if (highlight)
        {
            if (AoE)
                renderer.color = Color.yellow;
            else
                renderer.color = Color.green;
        }
        else
            renderer.color = Color.white;
    }

    private void OnMouseDown()
    {
        if (isHighlighted)
        {
            TileManager.instance.SelectTile(index, ActionManager.instance.CurrentAbility);
        }
        else
        {
            //Character tempCharacter = CharacterManager.instance.GetCharacter(index);
            //if (tempCharacter != null)
            //    tempCharacter.SelectCharacter();
        }
    }
}
