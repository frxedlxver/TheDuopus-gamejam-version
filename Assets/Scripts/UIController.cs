using UnityEngine;
using UnityEngine.UIElements;

public class UIController : MonoBehaviour
{
    public UIDocument uiDocument;

    private void Start()
    {
        Getting_Head gettingHeadScript = GetComponent<Getting_Head>();

        // Subscribe to the custom event
        gettingHeadScript.gameOverEvent.AddListener(GameOver);
    }

    private void GameOver()
    {
        // Pause game
        Time.timeScale = 0f;

        // Make the leaderboard show up
        if (uiDocument != null)
        {
            VisualElement uiRoot = uiDocument.rootVisualElement;
            uiRoot.visible = true;
        }
    }
}