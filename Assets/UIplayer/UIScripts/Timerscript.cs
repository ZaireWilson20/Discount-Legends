using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timerscript : MonoBehaviour
{
    public float Timer = 90f;
    public Text Timertext;

    // Start is called before the first frame update
   
    // Update is called once per frame
    void Update()
    {
        if (Timer > 0)
        {
            Timer -= Time.deltaTime;

        }
        else
        {
            Timer = 0;

        }
        DisplayTime(Timer);



      
    }


    void DisplayTime(float TimeDisplay)
    {
        if (TimeDisplay < 0)
        {
            TimeDisplay = 0;

        }

        float minutes = Mathf.FloorToInt(TimeDisplay/60);
        float seconds = Mathf.FloorToInt(TimeDisplay % 60);

        Timertext.text = string.Format("{0:00}:{1:00}",minutes,seconds); 

    }
}
