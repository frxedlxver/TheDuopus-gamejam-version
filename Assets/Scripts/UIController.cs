using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public VisualTreeAsset uiVisualTreeAsset;
    private VisualElement uiRoot;

    private void Start()
    {
        Getting_Head gettingHeadScript = GetComponent<Getting_Head>();

        // Subscribe to the custom event
        gettingHeadScript.gameOverEvent.AddListener(GameOver);

        // Instantiate the UI hierarchy from the VisualTreeAsset
        if (uiVisualTreeAsset != null)
        {
            uiRoot = uiVisualTreeAsset.CloneTree();
            uiRoot.visible = false; // Initially hide the UI

            // Find the UIDocument in the scene
            UIDocument uiDocument = FindObjectOfType<UIDocument>();

            if (uiDocument != null)
            {
                VisualElement rootElement = uiDocument.rootVisualElement;

                // Add uiRoot to the hierarchy
                if (rootElement != null)
                {
                    Debug.Log("Adding ui root");
                    rootElement.Add(uiRoot);
                }
                else
                {
                    Debug.LogError("Root element not found in the UIDocument. Make sure to set a suitable parent in your UI hierarchy.");
                }
            }
            else
            {
                Debug.LogError("UIDocument not found in the scene.");
            }
        }
    }


    private void GameOver()
    {
        // Pause game
        Time.timeScale = 0f;

        // Make the leaderboard show up
        if (uiRoot != null)
        {
            uiRoot.visible = true;
        }
    }
}