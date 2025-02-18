using UnityEngine;
using System.Collections.Generic;

public class MapVisualizer : MonoBehaviour
{
    public GameObject linePrefab; // Assign a UI Line prefab
    public Transform lineContainer; // Parent for lines
    public MapManager mapManager;

    void Start()
    {
        DrawConnections(mapManager.nodes);
    }

    public void DrawConnections(List<MapNode> nodes)
    {
        foreach (MapNode node in nodes)
        {
            foreach (MapNode connectedNode in node.connectedNodes)
            {
                GameObject line = Instantiate(linePrefab, lineContainer);
                LineRenderer lr = line.GetComponent<LineRenderer>();
                lr.positionCount = 2;
                lr.SetPosition(0, node.position);
                lr.SetPosition(1, connectedNode.position);
            }
        }
    }
}
