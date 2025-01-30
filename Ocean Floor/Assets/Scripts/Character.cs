using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.GraphicsBuffer;

public class Character : MonoBehaviour
{
    [SerializeField] private int tilePos;
    [SerializeField] private GameObject selectedArrow;
    [SerializeField] private GameObject body;
    [SerializeField] private SpriteRenderer healthBar;

    [SerializeField] private int maxHealth;
    [SerializeField] private BaseAbility[] abilities;

    private bool facingLeft = false;
    private int currentHealth;
    private bool AoE;

    public int TilePos { get { return tilePos; } }

    private void Start()
    {
        currentHealth = maxHealth;
        SetHealthbar();

        CharacterManager.instance.AddCharacterToList(this);
        Deselect();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Math.Clamp(currentHealth, 0, maxHealth);
        SetHealthbar();
    }

    private void SetHealthbar()
    {
        float healthPercent = (float)currentHealth / (float)maxHealth;
        Debug.Log(healthPercent);

        healthBar.size = new Vector2(healthPercent, healthBar.size.y);
        healthBar.transform.localPosition = new Vector3((1 - healthPercent) / -2, 0);
    }

    public void SelectCharacter()
    {
        if (CharacterManager.instance.SelectedCharacter == this)
        {
            ActionManager.instance.ActivateActionBar(false);
            CharacterManager.instance.SelectCharacter(null);
            TileManager.instance.StopHightlight();
        }
        else
        {
            ActionManager.instance.SetAbilityValues(abilities);
            selectedArrow.SetActive(true);
            ActionManager.instance.ActivateActionBar(true);
            CharacterManager.instance.SelectCharacter(this);
            ActionManager.instance.ReSelectAction();
        }
    }

    public void Deselect()
    {
        selectedArrow.SetActive(false);
    }

    public void GetTileTarget(int index)
    {
        if (ActionManager.instance.ActionIndex >= 0)
        {
            ActivateAbility(index);
        }
        else if (ActionManager.instance.ActionIndex == -1)
        {
            Move(index);
        }
        else 
        {
            Turn();
        }
        Invoke(nameof(Reselect), 0.01f);
    }

    private void ActivateAbility(int index)
    {
        if (AoE)
        {
            foreach (var item in TileManager.instance.TileData)
            {
                if (item.IsHighlighted)
                {
                    Character tempCharacter = CharacterManager.instance.GetCharacter(item.Index);
                    if (tempCharacter != null)
                        tempCharacter.TakeDamage(abilities[ActionManager.instance.ActionIndex].Damage);
                }
            }
        }
        else
        {
            Character tempCharacter = CharacterManager.instance.GetCharacter(index);
            if (tempCharacter != null)
                tempCharacter.TakeDamage(abilities[ActionManager.instance.ActionIndex].Damage);
        }
    }

    public void AbilityTargeting(int index)
    {
        BaseAbility ability = abilities[index];
        List<int> tileList = new List<int>();

        if (ability is StrikeAbility)
        {
            foreach (var item in (ability as StrikeAbility).Targets)
            {
                if (facingLeft)
                    tileList.Add(tilePos - item);
                else
                    tileList.Add(tilePos + item);
            }
            AoE = (ability as StrikeAbility).AoE;
        }
        else if (ability is ShootAbility)
        {
            int targets = (ability as ShootAbility).MaxTargets;
            for (int i = 1; i <= (ability as ShootAbility).Range; i++)
            {
                int tempTile = tilePos;

                if (facingLeft)
                    tempTile -= i;
                else
                    tempTile += i;

                Character tempCharacter = CharacterManager.instance.GetCharacter(tempTile);
                if (tempCharacter != null)
                {
                    tileList.Add(tempTile);
                    targets--;
                    if (targets == 0)
                        break;
                }
            }
            AoE = true;
        }
        else if (ability is ThrowAbility)
        {
            for (int i = (ability as ThrowAbility).MinRange; i <= (ability as ThrowAbility).Range; i++)
            {
                int tempTile = tilePos;

                if ((ability as ThrowAbility).OmniDirectional)
                {
                    Character tempCharacter = CharacterManager.instance.GetCharacter(tempTile + i);
                    if (tempCharacter != null || (ability as ThrowAbility).CanTargetEmpty)
                    {
                        tileList.Add(tempTile + i);
                    }

                    tempCharacter = CharacterManager.instance.GetCharacter(tempTile - i);
                    if (tempCharacter != null || (ability as ThrowAbility).CanTargetEmpty)
                    {
                        tileList.Add(tempTile - i);
                    }
                }
                else
                {
                    if (facingLeft)
                        tempTile -= i;
                    else
                        tempTile += i;

                    Character tempCharacter = CharacterManager.instance.GetCharacter(tempTile);
                    if (tempCharacter != null || (ability as ThrowAbility).CanTargetEmpty)
                    {
                        tileList.Add(tempTile);
                    }
                }
            }
            AoE = (ability as ThrowAbility).AoE;
        }

        TileManager.instance.HighlightTiles(tileList, AoE);
    }

    private void Move(int index)
    {
        Character tempCharacter = CharacterManager.instance.GetCharacter(index);
        if (tempCharacter != null)
        {
            tempCharacter.SetPosition(tilePos);
        }

        SetPosition(index);
    }

    private void SetPosition(int index)
    {
        tilePos = index;
        transform.position = new Vector3(TileManager.instance.GetTilePos(index), transform.position.y);
    }

    private void Turn()
    {
        facingLeft = !facingLeft;

        if (facingLeft)
        {
            body.transform.eulerAngles = new Vector3(0, 180);
        }
        else
        {
            body.transform.eulerAngles = new Vector3(0, 0);
        }
    }

    private void Reselect()
    {
        ActionManager.instance.ReSelectAction();
    }
}
