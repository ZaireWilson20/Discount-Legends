using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
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

        if (Timer == 0)
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex + 1);
        }


    }


    void DisplayTime(float TimeDisplay)
    {
        if (TimeDisplay < 0)
        {
            TimeDisplay = 0;

        }

        float minutes = Mathf.FloorToInt(TimeDisplay / 60);
        float seconds = Mathf.FloorToInt(TimeDisplay % 60);

        Timertext.text = string.Format("{0:00}:{1:00}", minutes, seconds);

    }
}
