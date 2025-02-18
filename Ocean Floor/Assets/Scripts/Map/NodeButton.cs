using UnityEngine;
using UnityEngine.UI;

public class NodeButton : MonoBehaviour
{
    public MapManager mapManager;
    public MapNode targetNode;
    public Image icon; // Assign in the Unity Inspector

    public Sprite encounterSprite;
    public Sprite shopSprite;
    public Sprite restSprite;

    void Start()
    {
        UpdateAppearance();
    }

    public void OnClick()
    {
        mapManager.MoveToNode(targetNode);
    }

    public void UpdateAppearance()
    {
        if (targetNode == null || icon == null) return;

        switch (targetNode.type)
        {
            case NodeType.Encounter:
                icon.sprite = encounterSprite;
                icon.color = Color.red; // Optional: Different colors
                break;
            case NodeType.Shop:
                icon.sprite = shopSprite;
                icon.color = Color.blue;
                break;
            case NodeType.Rest:
                icon.sprite = restSprite;
                icon.color = Color.green;
                break;
        }
    }
}
