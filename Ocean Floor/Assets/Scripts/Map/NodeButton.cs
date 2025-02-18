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

        Button btn = GetComponent<Button>();
        if (btn != null)
        {
            btn.onClick.RemoveAllListeners(); // Remove old listeners
            btn.onClick.AddListener(OnClick); // Attach OnClick() to Unity button

            Debug.Log($"Button click event assigned for {targetNode?.type}");
        }
        else
        {
            Debug.LogError("No Button component found on " + gameObject.name);
        }
    }



    public void OnClick()
    {
        Debug.Log("KLICKAT");
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
