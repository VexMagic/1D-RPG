using BehaviorTreeSpace;
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

    
    [SerializeField] private bool isEnemy;
    [SerializeField] private BehaviorTree behaviorTree;

    private bool facingLeft = false;
    private int currentHealth;
    private bool AoE;
    public bool IsEnemy { get { return isEnemy; } } //the tile the character is standing on
    public bool FacingLeft { get { return facingLeft; } } //the tile the character is standing on

    public int TilePos { get { return tilePos; } } //the tile the character is standing on

    private void Start()
    {
        currentHealth = maxHealth;
        SetHealthbar();

        CharacterManager.instance.AddCharacterToList(this);
        Deselect();

        //if its an enemy get its behavior tree
        if (isEnemy)
        {
            behaviorTree = gameObject.GetComponent<BehaviorTreeSpace.BehaviorTree>();
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Math.Clamp(currentHealth, 0, maxHealth);
        SetHealthbar();

        if (currentHealth == 0)
        {
            //add death
        }
    }

    private void SetHealthbar() //updates the healthbar to show how much health compared to the max health the character has
    {
        float healthPercent = (float)currentHealth / (float)maxHealth;

        healthBar.size = new Vector2(healthPercent, healthBar.size.y);
        healthBar.transform.localPosition = new Vector3((1 - healthPercent) / -2, 0);
    }

    public void SelectCharacter() //this activates when a characters turn starts and set the UI to the correct abilities
    {
        ActionManager.instance.SetAbilityValues(abilities); 
        selectedArrow.SetActive(true);
        ActionManager.instance.ActivateActionBar(true);
        CharacterManager.instance.SelectCharacter(this);
        ActionManager.instance.ReSelectAction();
    }

    public void Deselect() //turn off the selected arrow above the character
    {
        selectedArrow.SetActive(false);
    }

    public void GetTileTarget(int index) //activate selected ability on the targeted tile
    {
        if (ActionManager.instance.ActionIndex >= 0) //unique ability
        {
            ActivateAbility(index);
        }
        else if (ActionManager.instance.ActionIndex == -1) //move ability
        {
            Move(index);
        }
        else //turn ability
        {
            Turn();
        }
        Invoke(nameof(Reselect), 0.01f);
    }

    private void ActivateAbility(int index) //activate unique ability on the targeted tile
    {
        if (AoE) //check if the ability targets all possible tiles
        {
            foreach (var item in TileManager.instance.TileData) //target all highlighted tiles
            {
                if (item.IsHighlighted)
                {
                    //check if there is a character on the tile before dealing damage
                    Character tempCharacter = CharacterManager.instance.GetCharacter(item.Index);
                    if (tempCharacter != null)
                        tempCharacter.TakeDamage(abilities[ActionManager.instance.ActionIndex].Damage);
                }
            }
        }
        else
        {
            //check if there is a character on the tile before dealing damage
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
            foreach (var item in (ability as StrikeAbility).Targets) //highlight all targetable tiles for a strike ability
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
            for (int i = 1; i <= (ability as ShootAbility).Range; i++) //loop through all tiles in a direction until the max targets or max range is reached
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

            //this makes sure that the pierce effect works
            AoE = true;
        }
        else if (ability is ThrowAbility)
        {
            for (int i = (ability as ThrowAbility).MinRange; i <= (ability as ThrowAbility).Range; i++) //loop through all tiles within range
            {
                int tempTile = tilePos;

                if ((ability as ThrowAbility).OmniDirectional) //target both infront and behind
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
                else //only target infront 
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

    private void Reselect() //update all the highlighted tiles after an action is activated
    {
        ActionManager.instance.ReSelectAction();
    }

    public void StartEnemyTurn() //activate the behavior tree for the enemy
    {
        int numberOfTurns = 4;

        for (int i = 0; i < numberOfTurns; i++)
        {
            Invoke(nameof(SearchTree), 0.01f);
        }

    }
    private void SearchTree()
    {
        behaviorTree.Search();
    }

    public void EnemyMove(int tilePos)
    {
        Move(tilePos);
    }
    public void EnemyTurnAround()
    {
        Turn();
    }

}
