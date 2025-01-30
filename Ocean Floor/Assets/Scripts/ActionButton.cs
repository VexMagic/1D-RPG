using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField] private Outline outline;
    [SerializeField] private TextMeshProUGUI costText;
    [SerializeField] private Image image;
    [SerializeField] private int index;

    private int cost = 1;

    public int Index { get { return index; } }
    public int Cost { get { return cost; } }
    public bool Selected { get { return outline.enabled; } }

    private void Start()
    {
        costText.text = cost.ToString();
        DeSelect();
    }

    public void SetValues(BaseAbility ability)
    {
        cost = ability.Cost;
        image.sprite = ability.Sprite;
        costText.text = cost.ToString();
    }

    public void Select()
    {
        if (outline.enabled)
        {
            DeSelect();
            TileManager.instance.StopHightlight();
        }
        else
        {
            ActionManager.instance.DeSelectActions();
            outline.enabled = true;
            ActionManager.instance.SetActionIndex(index);
            HighlightTiles();
        }
    }

    public void DeSelect()
    {
        outline.enabled = false;
    }

    public void HighlightTiles()
    {
        int tilePos = CharacterManager.instance.SelectedCharacter.TilePos;

        if (index >= 0)
        {
            CharacterManager.instance.SelectedCharacter.AbilityTargeting(index);
        }
        else if (index == -1)
        {
            TileManager.instance.HighlightTiles(new List<int> { tilePos - 1, tilePos + 1 }, false);
        }
        else
        {
            TileManager.instance.HighlightTiles(new List<int> { tilePos }, false);
        }
    }
}
