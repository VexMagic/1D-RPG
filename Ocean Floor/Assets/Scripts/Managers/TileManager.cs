using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.Progress;

public class TileManager : MonoBehaviour
{
    public static TileManager instance;

    [SerializeField] private GameObject Tile;
    [SerializeField] private SpriteRenderer renderer;
    [SerializeField] private int amount;

    private List<GameObject> tiles = new List<GameObject>();
    private List<GroundTile> tileData = new List<GroundTile>();

    public List<GroundTile> TileData { get { return tileData; } }

    private void Awake()
    {
        instance = this;
        SetBoard();
    }

    public float GetTilePos(int index)
    {
        return tiles[index].transform.position.x;
    }

    public void SelectTile(int index, BaseAbility ability)
    {
        if (!ActionManager.instance.SpendActions()) //check if there is enough action points to use the ability
            return;

        //activate selected ability
        ability.ActivateAbility(index, CharacterManager.instance.SelectedCharacter);

        StopHightlight();
    }

    public void StopHightlight()
    {
        HighlightTiles(new List<int>(), false);
    }

    public void HighlightTiles(List<int> indexes, bool AoE)
    {
        for (int i = 0; i < tiles.Count; i++)
        {
            tileData[i].Highlight(indexes.Contains(i), AoE);
        }
    }

    private void SetBoard()
    {
        DestroyTiles();
        float width = amount + ((amount + 1) * 0.5f);
        renderer.size = new Vector2(width, renderer.size.y);

        for (int i = 0; i < amount; i++)
        {
            CreateTile(i);
        }
    }

    private void CreateTile(int index)
    {
        GameObject tileObject = Instantiate(Tile, transform);

        //set tile position
        float indexOffset = index - ((float)(amount - 1) / 2);
        float xPos = indexOffset * 1.5f;
        tileObject.transform.localPosition = new Vector3(xPos, 0, 0);

        //add the new tile's script to a list for later use
        GroundTile tile = tileObject.GetComponent<GroundTile>();
        tile.SetIndex(index);

        tiles.Add(tileObject);
        tileData.Add(tile);
    }

    private void DestroyTiles()
    {
        foreach (GameObject tileObject in tiles)
        {
            Destroy(tileObject);
        }
        tiles.Clear(); 
        tileData.Clear();
    }
}
