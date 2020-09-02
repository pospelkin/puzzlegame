using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    public static Timer instance;
    
    public Text timerCounter;

    public TimeSpan timePlaying;
    private bool timerGoing;

    public float elapsedTime;

   // public int unixTime = 0;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    void Start()
    {
        timerCounter.text = "00:00:00";
        timerGoing = false;
        
    }
       
    public void BeginTimer()
    {
        timerGoing = true;
        elapsedTime = 0;

        StartCoroutine(UpdateTimer());
    }
    public void NoPouseTimer()
    {
        timerGoing = true;
        StartCoroutine(UpdateTimer());
    }
    public void EndTimer()
    {
        timerGoing = false;
    }

    private IEnumerator UpdateTimer()
    {
        while(timerGoing)
        {
            elapsedTime += Time.deltaTime;
            timePlaying = TimeSpan.FromSeconds(elapsedTime);
            string timePlayingStr = timePlaying.ToString("mm':'ss'.'ff");
            timerCounter.text = timePlayingStr;

            yield return null;

            //unixTime = (int)(DateTime.UtcNow - new DateTime(1970, 1, 1)).TotalSeconds;
            //Debug.Log(unixTime);
        }
    }
   /*static double ConvertToUnixTimestamp(DateTime time)
    {
        DateTime origin = new DateTime(1970, 1, 1, 0, 0, 0, 0);
        TimeSpan timePlaying = time - origin;
        return Math.Floor(timePlaying.TotalSeconds);
    }*/

    /*void Update()
    {
        float t = Time.time - startTime;

        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;

    }*/
}
