using UnityEngine;
using UnityEngine.UIElements;

public class MenuLogic : MonoBehaviour
{

    [SerializeField] private UIDocument mainMenu;
    [SerializeField] private GameController gameController;

    private Button startBtn;

    // Start is called before the first frame update
    void Start()
    {
        var rootElement = mainMenu.rootVisualElement;

        startBtn = rootElement.Q<Button>("Start");
        
        startBtn.clicked += gameController.StartGame;
        startBtn.clicked += HideMenu;
        
    }

    private void HideMenu()
    {
        var rootElement = mainMenu.rootVisualElement;
        rootElement.style.display = DisplayStyle.None;
    }
    
}