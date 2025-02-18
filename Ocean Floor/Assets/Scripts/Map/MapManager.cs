using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    public GameObject nodeButtonPrefab;
    public Transform mapContainer;
    public GameObject playerIcon;
    public GameObject playerPrefab;
    public List<MapNode> nodes;

    private MapNode currentNode;

    void Start()
    {
        LoadProgress();
        GenerateMapUI();
        UpdateNodeInteractivity();

        if (currentNode != null)
        {
            Debug.Log($"Setting player position to: {currentNode.position}");

            if (playerIcon != null)
            {
                playerIcon.transform.position = currentNode.position;
            }
            else
            {
                Debug.LogError("playerIcon is NULL! Is it assigned in the Inspector?");
            }
        }
        else
        {
            Debug.LogError("No starting node found!");
        }
    }


    void GenerateMapUI()
    {
        foreach (MapNode node in nodes)
        {
            GameObject button = Instantiate(nodeButtonPrefab, mapContainer);
            button.transform.position = node.position;

            NodeButton nodeButton = button.GetComponent<NodeButton>();
            nodeButton.targetNode = node;
            nodeButton.mapManager = this;
            node.uiButton = button.GetComponent<Button>();

            // Assign OnClick action dynamically
            node.uiButton.onClick.AddListener(() => OnNodeClicked(node));

            nodeButton.UpdateAppearance();

            Debug.Log($"Created button for {node.type} at {node.position}, interactable: {node.uiButton.interactable}");
        }
    }

    // This function executes when a node button is clicked
    public void OnNodeClicked(MapNode node)
    {
        MoveToNode(node); // Move player

        // Execute different actions based on node type
        switch (node.type)
        {
            case NodeType.Encounter:
                LoadScene("SampleScene");
                break;
            case NodeType.Shop:
                LoadScene("ShopScene");
                break;
            case NodeType.Rest:
                Debug.Log("Player Resting...");
                break;
        }
    }


    public void MoveToNode(MapNode targetNode)
    {
        OnReachNode(targetNode);
        Debug.Log("KLICKAT PÅ KNAPPEN");
        //if (currentNode.connectedNodes.Contains(targetNode))
        //{
        //    StartCoroutine(MoveToNodeCoroutine(targetNode));
        //}
    }

    //private IEnumerator MoveToNodeCoroutine(MapNode targetNode)
    //{
    //    Vector3 startPos = playerIcon.transform.position;
    //    Vector3 endPos = targetNode.position;
    //    float duration = 1f;
    //    float elapsed = 0;
    //
    //    while (elapsed < duration)
    //    {
    //        playerIcon.transform.position = Vector3.Lerp(startPos, endPos, elapsed / duration);
    //        elapsed += Time.deltaTime;
    //        yield return null;
    //    }
    //
    //    playerIcon.transform.position = endPos;
    //    currentNode = targetNode;
    //    OnReachNode(targetNode);
    //    SaveProgress();
    //    UpdateNodeInteractivity();
    //}

    private void OnReachNode(MapNode node)
    {
        switch (node.type)
        {
            case NodeType.Encounter:
                LoadScene("SampleScene");
                break;
            case NodeType.Shop:
                LoadScene("ShopScene");
                break;
            case NodeType.Rest:
                LoadScene("RestScene");
                break;
        }
    }

    public void LoadScene(string sceneName)
    {
        Debug.Log($"Loading Scene: {sceneName}");
        SceneManager.LoadScene(sceneName);
    }

    private void SaveProgress()
    {
        PlayerPrefs.SetInt("LastNode", nodes.IndexOf(currentNode));
    }

    private void LoadProgress()
    {
        int lastNodeIndex = PlayerPrefs.GetInt("LastNode", 0);
        currentNode = nodes[lastNodeIndex];
    }

    private void UpdateNodeInteractivity()
    {
        foreach (MapNode node in nodes)
        {
            if (node.uiButton != null)
            {
                node.uiButton.interactable = true; // Allow clicking on all nodes
            }
        }
    }
}
