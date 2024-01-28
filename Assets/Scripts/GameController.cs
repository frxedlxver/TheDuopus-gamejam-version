using UnityEngine;

public class GameController : MonoBehaviour
{
    
    [SerializeField] private GameObject duopus;
    [SerializeField] private GameObject timerUI;
    [SerializeField] private GameTimer timerManager;

    private void Start()
    {
        duopus.SetActive(false);
    }

    public void StartGame()
    {
        duopus.SetActive(true);
        timerUI.SetActive(true);
        timerManager.StartTimer();
    }
}
