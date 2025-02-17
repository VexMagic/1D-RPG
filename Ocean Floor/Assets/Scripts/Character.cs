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
    [SerializeField] public GameObject body;
    [SerializeField] private SpriteRenderer healthBar;

    [SerializeField] private int maxHealth;
    [SerializeField] private BaseAbility[] abilities;

    public bool facingLeft = false;
    private int currentHealth;
    private bool AoE;

    public int TilePos { get { return tilePos; } set { tilePos = TilePos; } } //the tile the character is standing on

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

    private void Reselect() //update all the highlighted tiles after an action is activated
    {
        ActionManager.instance.ReSelectAction();
    }
}
