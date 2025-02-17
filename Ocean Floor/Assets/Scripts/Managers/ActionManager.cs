using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ActionManager : MonoBehaviour
{
    public static ActionManager instance;

    [SerializeField] private GameObject actionBar;
    [SerializeField] private TextMeshProUGUI actionsText;
    [SerializeField] private int actionsPerTurn;

    private ActionButton[] buttons;
    private BaseAbility currentAbility;
    private int actionsLeft;

    public BaseAbility CurrentAbility {  get { return currentAbility; } }
    public int ActionsLeft { get { return actionsLeft; } }
    private void Awake()
    {
        instance = this;
        buttons = FindObjectsOfType<ActionButton>();
        actionBar.SetActive(false);
    }

    public void StartTurn()
    {
        SetAction(actionsPerTurn);
    }

    public void SetAbilityValues(BaseAbility[] abilities) //update the ability UI
    {
        for (int i = 0; i < abilities.Length; i++)
        {
            foreach (var item in buttons)
            {
                item.SetValues(abilities[i]);

            }
        }
    }
    public bool CheckActionsLeft()
    {
        int actions = 0;
        ActionButton actionButton = GetSelectedAction();

        if (actionButton != null)
        {
            actions = actionButton.Cost;
        }

        if (actionsLeft < actions)
        {
            return false;
        }
        return true;
    }
    public void SpendActions() //spend action points
    {
        int actions = 0;
        ActionButton actionButton = GetSelectedAction();

        if (actionButton != null)
        {
            actions = actionButton.Cost;
            Debug.Log(actions);
        }
        SetAction(actionsLeft - actions);
       
    }

    private void SetAction(int amount) //update the action point display
    {
        actionsLeft = amount;
        actionsText.text = "Actions: " + actionsLeft;
    }

    public void SetCurrentAbility (BaseAbility ability)
    {
        currentAbility = ability;
    }

    public void DeSelectActions()
    {
        TileManager.instance.StopHightlight();
        foreach (var item in buttons)
        {
            item.DeSelect();   
        }
    }

    public void ReSelectAction()
    {
        ActionButton actionButton = GetSelectedAction();
        if (actionButton != null)
            actionButton.HighlightTiles();
    }

    public void ActivateActionBar(bool activate)
    {
        actionBar.SetActive(activate);
        if (!activate)
        {
            DeSelectActions();
        }
    }

    public ActionButton GetSelectedAction()
    {
        foreach (var item in buttons)
        {
            if (item.Selected)
            {
                return item;
            }
        }
        return null;
    }
}
