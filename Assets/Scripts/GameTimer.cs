using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    private float timeElapsed = 0f;
    public Text timerTxt;

    private bool timerEnabled = false;

    public void StartTimer()
    {
        timerEnabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (timerEnabled)
        {
            timeElapsed += Time.deltaTime;
            UpdateTimerDisplay();
        }
    }

    public void StopTimer()
    {
        timerEnabled = false;
    }

    public void ResetTimer()
    {
        timeElapsed = 0f;
    }

    void UpdateTimerDisplay()
    {
        int minutes = Mathf.FloorToInt(timeElapsed / 60f);
        int seconds = Mathf.FloorToInt(timeElapsed % 60f);
        int ms = Mathf.FloorToInt((timeElapsed * 1000) % 100);

        timerTxt.text = string.Format("{0:00}:{1:00}:{2:00}", minutes, seconds, ms);
    }
}
