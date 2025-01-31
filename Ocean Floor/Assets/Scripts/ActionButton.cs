using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    [SerializeField] private Outline outline; //enable when this ability is selected
    [SerializeField] private TextMeshProUGUI costText; //display the action point cost of an ability
    [SerializeField] private Image image; //this is set to the abilities icon
    [SerializeField] private int index; //determins which of a characters abilities it refers to

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
        image.sprite = ability.Icon;
        costText.text = cost.ToString();
    }

    public void Select() //highlight the selected ability while turning off the highlight for all other abilities
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

    public void HighlightTiles() //indicate which tiles can be targeted by the selected ability
    {
        int tilePos = CharacterManager.instance.SelectedCharacter.TilePos;

        if (index >= 0) //unique ability targeting
        {
            CharacterManager.instance.SelectedCharacter.AbilityTargeting(index);
        }
        else if (index == -1) //Move targeting
        {
            TileManager.instance.HighlightTiles(new List<int> { tilePos - 1, tilePos + 1 }, false);
        }
        else //Turn targeting
        {
            TileManager.instance.HighlightTiles(new List<int> { tilePos }, false);
        }
    }
}
