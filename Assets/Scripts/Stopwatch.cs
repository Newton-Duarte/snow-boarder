using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Stopwatch : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    bool isTimerRunning = false;
    float time;

    // Update is called once per frame
    void Update()
    {
        if (isTimerRunning)
        {
            time += Time.deltaTime;
            UpdateTimerText();
        }
    }

    public void StartTimer()
    {
        time = 0;
        isTimerRunning = true;
    }

    public void StopTimer()
    {
        isTimerRunning = false;
    }

    void UpdateTimerText()
    {
        float minutes = Mathf.FloorToInt(time / 60);
        float seconds = Mathf.FloorToInt(time % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public string GetTime()
    {
        return timerText.text;
    }
}
