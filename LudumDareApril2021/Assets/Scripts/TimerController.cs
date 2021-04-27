using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{
    public static TimerController instance;

    public Text timeCounter;
    public float maxTime;
    public Color warningTime;

    private TimeSpan timePlaying;
    private bool timerGoing;
        
    private float elapsedTime;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        // timeCounter.text = "00:00";
        // timerGoing = false;
    }

    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0f;

        StartCoroutine(UpdateTimer());
    }

    public bool OutOfTime()
    {
        return (maxTime - elapsedTime <= 0);
    }

    public void EndTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while (timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(maxTime - elapsedTime);
            string timePlayingStr = timePlaying.ToString("mm':'ss");
            timeCounter.text = timePlayingStr;
            if(maxTime - elapsedTime <= maxTime / 5)
            {
                timeCounter.color =  warningTime;
            }

            if(maxTime - elapsedTime <= 0)
            {
                EndTimer();
            }

            yield return null;
        }
    }
}
