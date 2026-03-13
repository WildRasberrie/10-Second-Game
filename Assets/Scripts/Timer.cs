using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;
public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float time = 10f;
    public static bool timeOut =false;

    void Update()
    {
        if (time <= 0) { 
            timeOut = true;
            time = 0;
        }
        if (!timeOut)
        {
            time -= Time.deltaTime;
            //set timer to one decimal place 
            timerText.text = ("" + (int)time);
        }
    }
    
}
