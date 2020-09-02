using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewTime : MonoBehaviour
{
    public Text timeLabel;


    void Update()
    {
        timeLabel.text = Timer.instance.timerCounter.text;


    }
}
