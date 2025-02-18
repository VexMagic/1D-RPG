using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    private List<Character> allCharacters = new List<Character>();

    private Character selectedCharacter;
    private int initiativeOrder;

    public Character SelectedCharacter { get { return selectedCharacter; } }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        Invoke(nameof(Begin), 0.01f);
    }

    private void Begin()
    {
        allCharacters = allCharacters.OrderBy(x => Random.value).ToList(); //set the turn order for all characters
        EndTurn();
    }

    public void EndTurn() //select the next character in the turn order
    {
        if (selectedCharacter != null)
            selectedCharacter.Deselect();

        ActionManager.instance.StartTurn();
        ActionManager.instance.DeSelectActions();


        initiativeOrder++;
        if (initiativeOrder == allCharacters.Count)
            initiativeOrder = 0;

        allCharacters[initiativeOrder].SelectCharacter();

        if (selectedCharacter.isEnemy)
        {
            selectedCharacter.behaviorTree.Search();
            EndTurn();
        }

    }

    public void AddCharacterToList(Character character)
    {
        allCharacters.Add(character);
    }

    public void SelectCharacter(int tileIndex)
    {
        selectedCharacter = GetCharacter(tileIndex);
    }

    public void SelectCharacter(Character character)
    {
        selectedCharacter = character;
    }

    public Character GetCharacter(int tileIndex) //get a character on a specific tile
    {
        foreach (var item in allCharacters)
        {
            if (item.TilePos == tileIndex)
            {
                return item;
            }
        }
        return null;
    }
}
