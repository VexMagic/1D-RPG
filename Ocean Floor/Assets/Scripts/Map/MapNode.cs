using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MapNode
{
    public Vector2 position; // Position on the map
    public NodeType type; // Encounter, Shop, Rest, etc.
    public List<MapNode> connectedNodes; // Connected nodes
    [HideInInspector] public Button uiButton; // UI button reference
}

public enum NodeType
{
    Encounter,
    Shop,
    Rest
}
