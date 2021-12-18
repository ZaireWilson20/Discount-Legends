using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
public class Timerscript : MonoBehaviour
{
    public float Timer = 10f;
    public TextMeshProUGUI Timertext;
    public UnityEngine.Events.UnityEvent _RoundEnd; 

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
             _RoundEnd.Invoke();
            StartCoroutine(SendChallonge()); 

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

      IEnumerator SendChallonge() {
            yield return new WaitForSeconds(25f) ;
          
        
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.buildIndex + 1);
       
        
    }
}
